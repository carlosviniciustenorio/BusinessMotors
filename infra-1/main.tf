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

resource "aws_ecr_repository" "ecr_repo" {
  name = var.erc_repo_name
}

provider "aws" {
  region = var.aws_region
}