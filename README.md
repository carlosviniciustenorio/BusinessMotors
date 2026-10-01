<h1> 
  BusinessMotors
</h1>

## 📌 Overview
It's a project with many technologies.
Has a WebAPI to sale motors using the best principles of software development.

## 💻 Technologies
These are all the technologies and patterns used to develop this application
- Framework: .NET 8
- Cloud Provider: AWS - Services(ECR, ECS, VPC, ALB, TG, RDS, DynamoDB, S3)
- IaC: Terraform
- Container: Docker, Docker Compose
- Orchestration: Kubernetes
- Packaging and deployment: Helm
- GitOps continuous delivery: Argo CD
- Observability: Grafana, Sentry
- Metrics: Prometheus
- Cache: Redis
- ORM: Entity Framework
- CI/CD: GitHub Actions
- Observability: Prometheus, Grafana, ElasticSearch, Kibana, Sentry

## High level diagram

``` mermaid
    flowchart LR
        subgraph Frontend
            User
            BusinessMotorsFront
        end
        subgraph Public Network
            LoadBalancer
        end
        subgraph AWS VPC
            BusinessMotorsAPI
            RDS
        end

        subgraph AWS Network
            S3
        end

        User--Cloudfront/s3 --> BusinessMotorsFront
        BusinessMotorsFront--tcp/80 --> LoadBalancer
        LoadBalancer--tcp/8080 --> BusinessMotorsAPI --> S3
        BusinessMotorsAPI --> RDS--tcp/3036
        RDS --> BusinessMotorsAPI
        BusinessMotorsFront --> S3
        S3 --> BusinessMotorsFront
```

## Visão do cluster Kubernetes

A estrutura abaixo representa a arquitetura aplicada no namespace `development` com base nos manifestos em `kubernetes/manifests`, considerando um cluster com dois nós workers.

```mermaid
flowchart TB
    User --> Ingress

    subgraph Cluster[Cluster Kubernetes]
        subgraph Node1[Node 1]
            subgraph NamespaceDev1[Namespace: development]
                Ingress[Ingress development-ingress]

                subgraph API1[BusinessMotors API]
                    ServiceAPI[Service: businessmotorsapi]
                    HPA[HorizontalPodAutoscaler]
                    DeploymentAPI[Deployment: businessmotorsapi\n3 replicas]
                    PodAPI1[Pod API 1]
                    PodAPI2[Pod API 2]
                end

                subgraph Adminer1[Adminer]
                    ServiceAdminer[Service: adminer]
                    DeploymentAdminer[Deployment: adminer]
                end

                subgraph Mysql1[MySQL]
                    ServiceMySQL[Service: mysql]
                    StatefulSetMySQL[StatefulSet: mysql\n1 replica]
                    PVC[PVC: mysql-pvc]
                end

                Secret[Secret: mysql-secret]
                ConfigMap[ConfigMap: api-config]
            end
        end

        subgraph Node2[Node 2]
            subgraph NamespaceDev2[Namespace: development]
                PodAPI3[Pod API 3]
            end
        end
    end

    Ingress --> ServiceAPI
    Ingress --> ServiceAdminer

    HPA --> DeploymentAPI
    DeploymentAPI --> PodAPI1
    DeploymentAPI --> PodAPI2
    DeploymentAPI --> PodAPI3

    ServiceAPI --> PodAPI1
    ServiceAPI --> PodAPI2
    ServiceAPI --> PodAPI3

    PodAPI1 -->|envFrom configMap| ConfigMap
    PodAPI2 -->|envFrom configMap| ConfigMap
    PodAPI3 -->|envFrom configMap| ConfigMap

    PodAPI1 -->|connection string| Secret
    PodAPI2 -->|connection string| Secret
    PodAPI3 -->|connection string| Secret

    PodAPI1 --> ServiceMySQL
    PodAPI2 --> ServiceMySQL
    PodAPI3 --> ServiceMySQL

    ServiceMySQL --> StatefulSetMySQL
    StatefulSetMySQL --> PVC

    ServiceAdminer --> DeploymentAdminer
```

## Deploy Kubernetes com Helm e Argo CD

O repositório mantém duas formas de declarar os recursos Kubernetes:

- **Manifests diretos:** arquivos em `src/BusinessMotors/kubernetes/manifests/`, aplicados com `kubectl`.
- **Helm:** chart em `src/BusinessMotors/kubernetes/helm/businessmotors/`. O `values.yaml` contém a configuração base e `values-development.yaml` define os valores usados no Kind.
- **Argo CD:** a `Application` em `src/BusinessMotors/kubernetes/argocd/applications/businessmotors-development.yaml` acompanha a branch `main`, renderiza o chart Helm e sincroniza os recursos no namespace `development`.

### Fluxo GitOps

```mermaid
flowchart LR
    developer[Desenvolvedor] -->|commit e push| repository[Repositorio BusinessMotors\nbranch main]

    subgraph cluster[Cluster Kubernetes]
        subgraph argocdNamespace[Namespace argocd]
            application[Application\nbusinessmotors-development]
            controller[Argo CD Application Controller]
        end

        subgraph developmentNamespace[Namespace development]
            resources[Recursos Kubernetes\nAPI, MySQL, Adminer, Ingress e HPA]
        end
    end

    repository -->|chart e values| controller
    application --> controller
    controller -->|renderiza| chart[Helm chart\nbusinessmotors]
    chart -->|estado desejado| resources
    resources -->|estado observado| controller
    controller -. selfHeal e prune .-> resources
```

O Argo CD compara o estado declarado no Git com o estado do cluster. A sincronização automática aplica mudanças; `selfHeal` corrige alterações feitas fora do Git e `prune` remove recursos que foram retirados da configuração. A opção `CreateNamespace=true` cria o namespace `development` quando necessário.

### Instalação manual com Helm

Execute a partir da raiz do repositório:

```bash
helm lint src/BusinessMotors/kubernetes/helm/businessmotors \
  -f src/BusinessMotors/kubernetes/helm/businessmotors/values-development.yaml

helm upgrade --install businessmotors \
  src/BusinessMotors/kubernetes/helm/businessmotors \
  --namespace development --create-namespace \
  --values src/BusinessMotors/kubernetes/helm/businessmotors/values-development.yaml
```

### Ativação pelo Argo CD

Com o Argo CD instalado no cluster e acesso ao repositório configurado, registre a aplicação:

```bash
kubectl apply -f src/BusinessMotors/kubernetes/argocd/applications/businessmotors-development.yaml
argocd app get businessmotors-development
```

Revise `repoURL`, `targetRevision` e `path` no manifesto da `Application` ao usar outro repositório, branch ou localização do chart.

> Use apenas um método de instalação por namespace. Os manifests diretos e o chart gerenciam recursos com nomes equivalentes; aplicá-los ao mesmo tempo pode causar disputa de ownership e divergência de estado. Os valores de senha incluídos são exemplos locais: em ambientes compartilhados ou de produção, forneça um Secret externo e não armazene credenciais no Git.

O chart configura os recursos `Ingress` e HPA, mas não instala o NGINX Ingress Controller nem o Metrics Server. Esses componentes precisam estar disponíveis no cluster para que o acesso pelo Ingress e o autoscaling funcionem.