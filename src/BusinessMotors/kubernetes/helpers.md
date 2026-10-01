# Comandos facilitadores - passo a passo

Este guia mostra como subir toda a infraestrutura no Kind. Existem duas formas de expor a API e o Adminer:

1. **Com Ingress (recomendado):** um unico endpoint `http://localhost` com caminhos `/businessmotorsapi` e `/adminer`.
2. **Sem Ingress (modo teste):** acesso direto pelos `NodePorts` `http://localhost:30080` e `http://localhost:30081`.

O modo **Ingress** exige a instalacao do NGINX Ingress Controller, que depende de acesso a imagens do `registry.k8s.io`. Se voce estiver em rede corporativa com Artifactory, siga a secao de configuracao de registry mirror.

---

## Antes de comecar

Se voce ja criou o Secret no cluster sem valores ou com valores errados, reaplique o manifesto:

```bash
kubectl apply -f manifests/01-secrets.yaml
kubectl rollout restart statefulset/mysql -n development
kubectl rollout restart deployment/businessmotorsapi -n development
```

Se quiser recriar o Secret do zero:

```bash
kubectl delete secret mysql-secret -n development
kubectl apply -f manifests/01-secrets.yaml

### 00. Garantir que o Ingress Controller esteja no node com portas mapeadas (Kind)

Em clusters Kind este repositório cria mapeamentos de portas (`extraPortMappings`) apenas no node `control-plane`. Se o `ingress-nginx` for agendado em um `worker`, `localhost:80` pode não alcançar o controller, causando falhas de conexão pelo Ingress.

Para forçar o `ingress-nginx` a rodar no `control-plane`, aplique o patch incluído neste repositório:

```bash
kubectl apply -f manifests/00-ingress-node-selector.yaml
```

Isso adiciona um `nodeSelector` a `ingress-nginx-controller` direcionando-o para o node com o label `ingress-ready=true`.
```
 
### 001. Reescrita de caminho para a API (Ingress)

O `Ingress` deste repositório remove o prefixo `/businessmotorsapi` antes de encaminhar para o serviço da API. Sem essa reescrita, o backend ASP.NET pode receber caminhos como `/businessmotorsapi/swagger/index.html` e retornar 404, porque a aplicação espera `/swagger/index.html`.

O manifesto `manifests/11-ingress.yaml` já inclui as anotações do NGINX necessárias:

```yaml
nginx.ingress.kubernetes.io/use-regex: "true"
nginx.ingress.kubernetes.io/rewrite-target: /$2
```

Se preferir não reescrever no Ingress, configure a aplicação ASP.NET para usar `PathBase` com `/businessmotorsapi`.

---

## Passo 1: Criar o cluster Kind

O comando abaixo cria o cluster com 1 control-plane e 2 workers, ja configurando portas e registry mirror.

```bash
kind create cluster --config kind-config.yaml
```

Se voce ja tiver um cluster com o mesmo nome e quiser recriar:

```bash
kind delete cluster --name development
kind create cluster --config kind-config.yaml
```

---

## Passo 2: Carregar suas imagens locais no Kind

```bash
kind load docker-image businessmotorsapi:latest --name development
kind load docker-image adminer:latest --name development
kind load docker-image mysql:latest --name development
```

```bash
docker save --platform linux/amd64 -o mysql-amd64.tar mysql:latest
kind load image-archive mysql-amd64.tar --name development
```

```bash
docker save --platform linux/amd64 -o adminer-amd64.tar adminer:latest
kind load image-archive adminer-amd64.tar --name development
---
```

## Passo 3 (opcional): Instalar o NGINX Ingress Controller

Este passo e necessario apenas se voce for usar o Ingress. Se quiser testar primeiro pelos NodePorts, va para o **Passo 5**.

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/kind/deploy.yaml
```

Aguarde o controller ficar pronto:

```bash
kubectl wait --namespace ingress-nginx \
 --for=condition=ready pod \
 --selector=app.kubernetes.io/component=controller \
 --timeout=180s
```

Confira se todos os pods estao Running:

```bash
kubectl get pods -n ingress-nginx
```

### Se os pods do ingress-nginx ficarem em `ImagePullBackOff`

Significa que o cluster nao conseguiu baixar as imagens. Em redes corporativas, configure o `kind-config.yaml` para usar o Artifactory como registry mirror.

Se precisar, descubra as tags exatas das imagens:

```bash
kubectl describe pod -n ingress-nginx ingress-nginx-controller-XXXX
kubectl describe pod -n ingress-nginx ingress-nginx-admission-create-XXXX
```

E carregue as imagens manualmente (se ja tiver no Docker local):

```bash
kind load docker-image registry.k8s.io/ingress-nginx/controller:v1.11.2 --name development
kind load docker-image registry.k8s.io/ingress-nginx/kube-webhook-certgen:v1.4.3 --name development
```

> Substitua `v1.11.2` e `v1.4.3` pelas tags que aparecerem no `kubectl describe`.

---

## Passo 4 (opcional): Instalar o Metrics Server

O Metrics Server e obrigatorio para o HPA funcionar.

```bash
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml
```

No Kind, pode ser necessario desativar a validacao de certificado do kubelet:

```bash
kubectl patch deployment metrics-server -n kube-system --type='json' -p='[{"op": "add", "path": "/spec/template/spec/containers/0/args/-", "value": "--kubelet-insecure-tls"}]'

kubectl wait --namespace kube-system \
 --for=condition=ready pod \
 --selector=k8s-app=metrics-server \
 --timeout=90s
```

---

## Passo 5: Aplicar os manifests

Aplicar todos de uma vez e a maneira mais rapida:

```bash
kubectl apply -f manifests/
```

Se voce preferir aplicar um a um (para entender cada recurso), use:

```bash
# Namespace
kubectl apply -f manifests/00-namespace.yaml

# Secrets (senhas do MySQL e connection string da API)
kubectl apply -f manifests/01-secrets.yaml

# ConfigMaps (variaveis da API e do Adminer)
kubectl apply -f manifests/02-configmap.yaml

# PVC do MySQL
kubectl apply -f manifests/03-mysql-pvc.yaml

# StatefulSet do MySQL
kubectl apply -f manifests/04-mysql-statefulset.yaml

# Service Headless do MySQL
kubectl apply -f manifests/05-mysql-service.yaml

# Deployment da API
kubectl apply -f manifests/06-api-deployment.yaml

# Service NodePort da API
kubectl apply -f manifests/07-api-service.yaml

# Deployment do Adminer
kubectl apply -f manifests/08-adminer-deployment.yaml

# Service NodePort do Adminer
kubectl apply -f manifests/09-adminer-service.yaml

# Ingress (somente se instalou o NGINX no Passo 3)
kubectl apply -f manifests/10-ingress.yaml

# HPA da API (somente se instalou o Metrics Server no Passo 4)
kubectl apply -f manifests/11-api-hpa.yaml
```

---

## Passo 6: Aguardar os pods

```bash
kubectl wait --namespace development \
 --for=condition=ready pod \
 --all \
 --timeout=180s
```

Se quiser acompanhar em tempo real:

```bash
kubectl get pods -n development --watch
```

---

## Passo 7: Verificar o ambiente

```bash
# Pods
kubectl get pods -n development -o wide

# Services
kubectl get svc -n development

# Ingress (se instalou)
kubectl get ingress -n development

# HPA (se instalou)
kubectl get hpa -n development
```

---

🚀
👏
👍



## Passo 8: Acessar os servicos

### Opcao A: Via Ingress (se tudo funcionou)

- API: http://localhost/businessmotorsapi
- Adminer: http://localhost/adminer

### Opcao B: Via NodePort (se o Ingress nao funcionar)

- API: http://localhost:30080
- Adminer: http://localhost:30081

### Conectar o Adminer ao MySQL

No Adminer, preencha:

- **Sistema:** MySQL
- **Servidor:** `mysql.development.svc.cluster.local`
- **Usuario:** `businessmotors`
- **Senha:** `secret`
- **Banco de dados:** `businessmotors`

---

## Passo 9: Testar o HPA

Gere carga na API e acompanhe o scale:

Terminal 1:

```bash
kubectl run -n development --rm -i load-test --image=williamyeh/wrk --restart=Never -- \
 -t2 -c10 -d60s http://businessmotorsapi.development.svc.cluster.local/businessmotorsapi
```

Terminal 2:

```bash
kubectl get hpa -n development --watch
kubectl get pods -n development --watch
```

---

## Passo 10: Logs e debug

```bash
# Logs da API
kubectl logs -n development -l app=businessmotorsapi --tail=50

# Logs do MySQL
kubectl logs -n development -l app=mysql --tail=50

# Entrar no MySQL
kubectl exec -n development -it mysql-0 -- mysql -ubusinessmotors -psecret businessmotors

# Descrever um pod com erro
kubectl describe pod -n development <nome-do-pod>
```

---

## Passo 11: Destruir o ambiente

```bash
kind delete cluster --name development
```

---

## Problemas comuns

### Erro `failed calling webhook validate.nginx.ingress.kubernetes.io`

O webhook do ingress-nginx ainda nao esta pronto. Aguarde e reaplique:

```bash
kubectl wait --namespace ingress-nginx --for=condition=ready pod --selector=app.kubernetes.io/component=controller --timeout=180s
kubectl apply -f manifests/10-ingress.yaml
```

Se persistir, em ambiente de laboratorio apenas:

```bash
kubectl delete validatingwebhookconfiguration ingress-nginx-admission
kubectl apply -f manifests/10-ingress.yaml
```

### Pods do ingress-nginx em `ImagePullBackOff`

O Kind nao consegue acessar o `registry.k8s.io`. Opcoes:

1. Configure o Artifactory como registry mirror em `kind-config.yaml` e recrie o cluster.
2. Carregue as imagens manualmente:

```bash
kind load docker-image registry.k8s.io/ingress-nginx/controller:v1.11.2 --name development
kind load docker-image registry.k8s.io/ingress-nginx/kube-webhook-certgen:v1.4.3 --name development
```

> Descubra as tags exatas com `kubectl describe pod -n ingress-nginx`.

### Nao consegue acessar via `localhost`

Verifique se os mapeamentos de portas do `kind-config.yaml` foram aplicados:

```bash
docker ps
```

Voce deve ver algo como:

```text
0.0.0.0:80->80/tcp
0.0.0.0:443->443/tcp
```

Se nao aparecer, destrua e recrie o cluster.

### API retorna 502 Bad Gateway pelo Ingress

Provavelmente os pods da API ainda nao estao prontos ou o path esta errado. Verifique:

```bash
kubectl get pods -n development
kubectl logs -n development -l app=businessmotorsapi --tail=50
```

Se o probe `/health` nao existir na sua API, ajuste o path em `manifests/06-api-deployment.yaml`.

---

## Helm e Argo CD

Os manifests Kubernetes existentes em `manifests/` continuam disponiveis para aplicacao direta. O chart Helm correspondente fica em `helm/businessmotors/`, e o `Application` do Argo CD fica em `argocd/applications/`.

### Validar e instalar com Helm

```bash
helm lint helm/businessmotors -f helm/businessmotors/values-development.yaml
helm template businessmotors helm/businessmotors -n development -f helm/businessmotors/values-development.yaml
helm upgrade --install businessmotors helm/businessmotors \
	--namespace development --create-namespace \
	--values helm/businessmotors/values-development.yaml
```

### Sincronizar com Argo CD

Instale/configure o Argo CD no cluster e aplique o `Application`:

```bash
kubectl apply -f argocd/applications/businessmotors-development.yaml
```

O `Application` acompanha a branch `main`, cria o namespace `development` e sincroniza automaticamente o chart. Para outro repositorio ou branch, ajuste `repoURL` e `targetRevision` no manifesto da aplicacao.

### Segredos

Os valores de `values.yaml` sao apenas para desenvolvimento local. Para clusters compartilhados ou producao, nao grave senhas no Git: crie o Secret `mysql-secret` no namespace de destino com as chaves `root-password`, `database`, `user`, `password` e `connection-string`, depois configure `mysql.auth.createSecret: false` e `mysql.auth.existingSecret: mysql-secret` nos values usados pelo Argo CD.
