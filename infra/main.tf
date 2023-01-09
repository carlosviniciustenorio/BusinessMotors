terraform {
  required_version = "1.3.3"

  required_providers {
    aws = {
        source = "hashicorp/aws"
        version   = "4.36.1"
    }
  }

  backend "s3" {}
}

provider "aws" {
  region = var.aws_region

  default_tags {
    tags = {
        Project = var.project
        Owner = var.owner
        ManagedBy = var.managed_by
    }
  }
}

module "ecr" {
  source = "./ecr_module"
  erc_repo_name = var.erc_repo_name
}

module "ecs" {
  source = "./ecs_module"
  ecs_cluster = var.ecs_cluster
  ecr_repository_url = module.ecr.ecr_module_repository_url
}

module "vpc" {
  source = "./vpc_module"
}

module "load_balancer_module" {
  source = "./load_balancer_module"
  default_vpc_id = module.vpc.default_vpc_id
  default_subnet_a_id = module.vpc.default_subnet_a.id
  default_subnet_b_id = module.vpc.default_subnet_b.id
  default_subnet_c_id = module.vpc.default_subnet_c.id
}