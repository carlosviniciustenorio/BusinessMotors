resource "aws_default_vpc" "default_vpc" {
}

resource "aws_default_subnet" "default_subnet_a" {
  availability_zone = "us-east-1a"
}

resource "aws_default_subnet" "default_subnet_b" {
  availability_zone = "us-east-1b"
}

resource "aws_default_subnet" "default_subnet_c" {
  availability_zone = "us-east-1c"
}

output "default_vpc_id" {
  value = aws_default_vpc.default_vpc.id
}

output "default_subnet_a" {
  value = aws_default_subnet.default_subnet_a
}

output "default_subnet_b" {
  value = aws_default_subnet.default_subnet_b
}

output "default_subnet_c" {
  value = aws_default_subnet.default_subnet_c
}