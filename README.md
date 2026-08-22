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

A estrutura abaixo representa a arquitetura aplicada no namespace `development` com base nos manifestos em `kubernetes/manifests`.

```mermaid
flowchart TB
    User --> Ingress

    subgraph KubernetesCluster[Cluster Kubernetes]
        subgraph NamespaceDev[Namespace: development]
            Ingress[Ingress development-ingress]

            subgraph API[BusinessMotors API]
                ServiceAPI[Service: businessmotorsapi]
                HPA[HorizontalPodAutoscaler]
                DeploymentAPI[Deployment: businessmotorsapi\n3 replicas]
                PodAPI1[Pod API 1]
                PodAPI2[Pod API 2]
                PodAPI3[Pod API 3]
            end

            subgraph Adminer[Adminer]
                ServiceAdminer[Service: adminer]
                DeploymentAdminer[Deployment: adminer]
            end

            subgraph MySQL[MySQL]
                ServiceMySQL[Service: mysql]
                StatefulSetMySQL[StatefulSet: mysql\n1 replica]
                PVC[PVC: mysql-pvc]
            end

            Secret[Secret: mysql-secret]
            ConfigMap[ConfigMap: api-config]
        end
    end

    Ingress --> ServiceAPI
    Ingress --> ServiceAdminer

    ServiceAPI --> DeploymentAPI
    HPA --> DeploymentAPI
    DeploymentAPI --> PodAPI1
    DeploymentAPI --> PodAPI2
    DeploymentAPI --> PodAPI3

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