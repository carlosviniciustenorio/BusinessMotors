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

data "aws_ecr_repository" "ecr" {
  name = var.erc_repo_name
}

module "vpc" {
  source = "./vpc_module"
}

module "load_balancer" {
  source = "./load_balancer_module"
  default_vpc_id = module.vpc.default_vpc_id
  default_subnet_a_id = module.vpc.default_subnet_a.id
  default_subnet_b_id = module.vpc.default_subnet_b.id
  default_subnet_c_id = module.vpc.default_subnet_c.id
}

module "ecs" {
  source = "./ecs_module"
  ecs_cluster = var.ecs_cluster
  ecr_repository_url = "${data.aws_ecr_repository.ecr.repository_url}:latest"
  target_group_arn = module.load_balancer.aws_lb_target_group.arn
  default_subnet_a_id = module.vpc.default_subnet_a.id
  default_subnet_b_id = module.vpc.default_subnet_b.id
  default_subnet_c_id = module.vpc.default_subnet_c.id
  load_balancer_security_group_id = module.load_balancer.load_balancer_security_group.id
} 