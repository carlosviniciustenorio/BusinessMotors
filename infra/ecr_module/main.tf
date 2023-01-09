resource "aws_ecr_repository" "ecr_repo" {
  name = var.erc_repo_name
}

output "ecr_module_repository_url" {
  value = aws_ecr_repository.ecr_repo.repository_url
}