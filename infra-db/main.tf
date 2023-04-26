terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 4.0"
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

resource "aws_db_instance" "database" {
  allocated_storage    = 10
  storage_type         = "gp2"
  engine               = "mysql"
  engine_version       = "5.7"
  instance_class       = "db.t2.micro"
  name                 = "cmanager"
  username             = "cmanagerapplication"
  password             = "C@rlos2626"
  parameter_group_name = "default.mysql5.7"
}