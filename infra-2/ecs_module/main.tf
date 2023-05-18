resource "aws_ecs_cluster" "ecs_fargate_cluster_ct" {
  name = var.ecs_cluster
}

resource "aws_iam_role" "ecsTaskExecutionRole" {
  name               = "ecsTaskExecutionRole"
  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [
      {
        Effect = "Allow"
        Principal = {
          Service = "ecs-tasks.amazonaws.com"
        }
        Action = "sts:AssumeRole"
      }
    ]
  })
}

resource "aws_iam_policy" "policies" {
  name        = "example-rds-fargate-policy"
  description = "Policy to allow RDS Fargate to access Secrets Manager"

  policy = jsonencode({
    Version   = "2012-10-17"
    Statement = [
      {
        Sid     = "Stmt1684445489794"
        Action  = "secretsmanager:*",
        Effect  = "Allow"
        Resource = "*"
      },
      {
        Sid     = "AllowECRAccess"
        Effect  = "Allow"
        Action  = [
          "ecr:GetAuthorizationToken",
          "ecr:BatchCheckLayerAvailability",
          "ecr:GetDownloadUrlForLayer",
          "ecr:GetRepositoryPolicy",
          "ecr:DescribeRepositories",
          "ecr:ListImages",
          "ecr:DescribeImages",
          "ecr:BatchGetImage"
        ]
        Resource = "*"
      }
    ]
  })
}

resource "aws_iam_role_policy_attachment" "ecsTaskExecutionRole_policy" {
  role       = aws_iam_role.ecsTaskExecutionRole.name
  policy_arn = aws_iam_policy.policies.arn
}

resource "aws_ecs_task_definition" "cmanager_task" {
  family                   = "cmanager-task" # Naming our first task
  container_definitions    = <<DEFINITION
  [
    {
      "name": "cmanager-task",
      "image": "${var.ecr_repository_url}",
      "essential": true,
      "portMappings": [
        {
          "containerPort": 80,
          "hostPort": 80
        }
      ],
      "environment": [
        {
          "name": "ASPNETCORE_ENVIRONMENT",
          "value": "${var.environment}"
        }
      ],
      "memory": 512,
      "cpu": 256
    }
  ]
  DEFINITION
  requires_compatibilities = ["FARGATE"]
  network_mode             = "awsvpc"   
  memory                   = 512        
  cpu                      = 256        
  execution_role_arn       = "${aws_iam_role.ecsTaskExecutionRole.arn}"
}

resource "aws_ecs_service" "cmanager_ecs_service" {
  name            = "${aws_ecs_task_definition.cmanager_task.family}"                        
  cluster         = "${aws_ecs_cluster.ecs_fargate_cluster_ct.id}"             
  task_definition = "${aws_ecs_task_definition.cmanager_task.arn}" 
  launch_type     = "FARGATE"
  desired_count   = 1

  load_balancer {
    target_group_arn = var.target_group_arn
    container_name   = "${aws_ecs_task_definition.cmanager_task.family}"
    container_port   = 80
  }

  network_configuration {
    subnets          = [var.default_subnet_a_id,
                        var.default_subnet_b_id,
                        var.default_subnet_c_id]
    assign_public_ip = true
    security_groups  = ["${aws_security_group.service_security_group.id}"]
  }
}

resource "aws_security_group" "service_security_group" {
  ingress {
    from_port = 0
    to_port   = 0
    protocol  = "-1"
    security_groups = [var.load_balancer_security_group_id]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}