variable "aws_region" {
  type        = string
  description = "The AWS region to deploy to"
  default     = "us-east-1"
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