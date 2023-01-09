variable "aws_region" {
  type        = string
  description = "The AWS region to deploy to"
  default     = "us-east-1"
}

variable "aws_profile" {
  type        = string
  description = "The AWS profile to use to execute the commands"
  default     = "default"
}

variable "environment" {
  type        = string
  description = "The environment to deploy to"
  default     = "development"
}

variable "project" {
  type        = string
  description = "Project Tag"
  default     = "development"
}

variable "owner" {
  type        = string
  description = "Owner Tag"
  default     = "AD"
}

variable "managed_by" {
  type        = string
  description = "CT Technology Tag"
  default     = "CT Technology"
}

//ECR
variable "erc_repo_name" {
  type = string
  description = "Name of ecr"
}

//Cluster
variable "ecs_cluster" {
  type = string
  description = "Cluster name"
}