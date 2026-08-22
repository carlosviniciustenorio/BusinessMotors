# Guia de teste do HPA do BusinessMotors

Este guia foi preparado para testar o autoscaling horizontal do deployment `businessmotorsapi` no namespace `development`.

## Objetivo

Validar que o HPA escala automaticamente os pods quando a aplicação recebe carga real de requisições e a média de CPU/memória ultrapassa os limites configurados.

---

## 1) Verifique se o HPA e o deployment estão aplicados

```bash
kubectl get hpa -n development
kubectl describe hpa businessmotorsapi-hpa -n development
kubectl get deploy -n development
kubectl get pods -n development -l app=businessmotorsapi
```

Você deve confirmar:

- o HPA `businessmotorsapi-hpa` existe
- o deployment `businessmotorsapi` existe
- há 3 pods iniciais em execução

---

## 2) Verifique se o Metrics Server está funcionando

O HPA depende de métricas de CPU e memória para escalar.

```bash
kubectl get deployment metrics-server -n kube-system
kubectl get pods -n kube-system
kubectl top pods -n development
```

Se o comando `kubectl top` não funcionar, o Metrics Server provavelmente não está instalado ou não está pronto.

Se necessário, instale o Metrics Server:

```bash
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml
```

Se o cluster estiver em ambiente local, pode ser necessário ajustar o `args` do Metrics Server com `--kubelet-insecure-tls` e `--kubelet-preferred-address-types=InternalIP`.

---

## 3) Entenda a regra de escala do HPA

O HPA configurado usa:

- CPU target: 70%
- memory target: 70%
- minReplicas: 3
- maxReplicas: 10

No deployment, o request está configurado em:

- CPU: `256m`
- Memory: `512Mi`

Isso significa que o HPA considerará escala quando a média de uso por pod ficar acima de:

- CPU: aproximadamente `179m`
- Memory: aproximadamente `358Mi`

---

## 4) Acesso à aplicação para testar carga

Para testar o endpoint da API localmente, exponha o serviço via port-forward:

```bash
kubectl port-forward svc/businessmotorsapi 8000:80 -n development
```

Depois, teste a URL:

```bash
curl "http://localhost:8000/api/anuncios?take=10&skip=0"
```

Se o endpoint depender de banco ou de algum serviço externo, certifique-se de que tudo está funcionando antes de aumentar a carga.

---

## 5) Script k6 recomendado para simular carga

Crie ou ajuste um arquivo `getAds-hpa.js` com o conteúdo abaixo:

```javascript
import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '1m', target: 20 },
    { duration: '3m', target: 80 },
    { duration: '4m', target: 150 },
    { duration: '1m', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'],
    http_req_failed: ['rate<0.05'],
  },
};

export default function () {
  const res = http.get('http://localhost:8000/api/anuncios?take=10&skip=0');

  check(res, {
    'status is 200': (r) => r.status === 200,
    'response time < 500ms': (r) => r.timings.duration < 500,
  });

  sleep(0.5);
}
```

Observações:

- `vus` e `iterations` não são a mesma coisa
- o correto para carga em sequência é usar `stages`
- um script muito simples com 1 usuário por 10 segundos quase nunca gera escalonamento real

---

## 6) Executar o teste de carga

No diretório do script, rode:

```bash
k6 run getAds-hpa.js
```

Se for usar o script com outro nome, substitua o nome do arquivo no comando.

---

## 7) Monitorar o HPA enquanto o teste está rodando

Em terminais separados, execute:

```bash
kubectl get hpa -n development -w
```

```bash
kubectl get deploy businessmotorsapi -n development -w
```

```bash
kubectl get pods -n development -l app=businessmotorsapi -w
```

```bash
kubectl top pods -n development
```

O que você deve observar:

- as métricas de CPU/memória aumentando
- o HPA alterando `DESIRED` e `CURRENT`
- o deployment criando novas pods
- os novos pods ficando `Running`

---

## 8) Validar se o HPA escalou

Após alguns minutos de carga, confirme:

```bash
kubectl describe hpa businessmotorsapi-hpa -n development
```

Procure eventos como:

- `SuccessfulRescale`
- `New size: 4; reason: ...`
- `pods metric ... above target`

Também verifique:

```bash
kubectl get pods -n development -l app=businessmotorsapi
```

Se a carga foi suficiente, você deve ver mais réplicas além de 3.

---

## 9) Testar o scale down

Quando a carga diminuir, pare o teste e aguarde o cooldown:

```bash
kubectl get hpa -n development -w
kubectl get deploy businessmotorsapi -n development -w
```

O HPA reduz as réplicas gradualmente devido ao `scaleDown` configurado:

- `stabilizationWindowSeconds: 60`
- `policies: 25% every 60s`

A redução não é instantânea, então aguarde alguns minutos antes de concluir que não houve escalar para baixo.

---

## 10) Troubleshooting

### HPA não escala

Verifique:

```bash
kubectl describe hpa businessmotorsapi-hpa -n development
kubectl top pods -n development
kubectl get events -n development --sort-by=.metadata.creationTimestamp
```

Possíveis causas:

- Metrics Server não funcionando
- a aplicação não está consumindo CPU/memória
- a carga está muito baixa para ultrapassar o threshold
- os requests estão muito rápidos e o endpoint acaba não gerando custo real

### Erro de 500/429/422 no endpoint

Se o endpoint responder com erro, isso pode ser devido a:

- banco indisponível
- serviço dependente caindo
- timeout
- excesso de carga no banco

Valide primeiro a saúde da API:

```bash
kubectl logs deploy/businessmotorsapi -n development --tail=100
```

---

## 11) Checklist final antes de concluir o teste

- [ ] Metrics Server funcionando
- [ ] HPA presente no namespace `development`
- [ ] deployment `businessmotorsapi` em execução
- [ ] carga aplicada com k6 por alguns minutos
- [ ] HPA mostrando scale-up
- [ ] pods novos sendo criados
- [ ] scale-down observado após a carga cessar

---

## 12) Comandos resumidos

```bash
kubectl get hpa -n development -w
kubectl get deploy businessmotorsapi -n development -w
kubectl get pods -n development -l app=businessmotorsapi -w
kubectl top pods -n development
kubectl describe hpa businessmotorsapi-hpa -n development
kubectl logs deploy/businessmotorsapi -n development --tail=100
k6 run getAds-hpa.js
```

---

## Dica importante

Para um teste real de autoscaling, o mais importante não é apenas “mandar algumas requisições”, e sim gerar pressão contínua por tempo suficiente para que a média de CPU ou memória ultrapasse os limites do HPA.

Se quiser, o próximo passo é gerar uma versão do script `k6` para o ambiente real do cluster, com endpoint via `Ingress`, `Service` ou `LoadBalancer`, além de um comando de execução completo em uma linha.
