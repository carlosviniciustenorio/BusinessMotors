variable "ecs_cluster" {
  type = string
  description = "Cluster name"
}

variable "ecr_repository_url" {
  type = string
  description = "URL ecs repository"
}

variable "target_group_arn" {
  type = string
  description = "ARN Target Group"
}

variable "default_subnet_a_id" {
  type = string
  description = "Region VPC subnet a"
}

variable "default_subnet_b_id" {
  type = string
  description = "Region VPC subnet b"
}

variable "default_subnet_c_id" {
  type = string
  description = "Region VPC subnet c"
}

variable "load_balancer_security_group_id" {
  type = string
  description = "Identity Load Balancer SG"
}

variable "environment" {
  type = string
  description = "Environment name"
}