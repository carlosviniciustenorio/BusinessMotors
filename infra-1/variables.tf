variable "erc_repo_name" {
  type = string
  description = "Name of ecr"
}

variable "aws_region" {
  type        = string
  description = "The AWS region to deploy to"
  default     = "us-east-1"
}