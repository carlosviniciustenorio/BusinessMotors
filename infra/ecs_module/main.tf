resource "aws_ecs_cluster" "ecs_fargate_cluster_ct" {
  name = var.ecs_cluster
}

resource "aws_ecs_task_definition" "cmanager_task" {
  family                   = "cmanager-task" # Naming our first task
  container_definitions    = <<DEFINITION
  [
    {
      "name": "cmanager_task",
      "image": "${var.ecr_repository_url}",
      "essential": true,
      "portMappings": [
        {
          "containerPort": 5000,
          "hostPort": 5000
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

resource "aws_iam_role" "ecsTaskExecutionRole" {
  name               = "ecsTaskExecutionRole"
  assume_role_policy = "${data.aws_iam_policy_document.assume_role_policy.json}"
}

data "aws_iam_policy_document" "assume_role_policy" {
  statement {
    actions = ["sts:AssumeRole"]

    principals {
      type        = "Service"
      identifiers = ["ecs-tasks.amazonaws.com"]
    }
  }
}

resource "aws_iam_role_policy_attachment" "ecsTaskExecutionRole_policy" {
  role       = "${aws_iam_role.ecsTaskExecutionRole.name}"
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}

resource "aws_ecs_service" "cmanager_ecs_service" {
  name            = "cmanager-service"                             
  cluster         = "${aws_ecs_cluster.ecs_fargate_cluster_ct.id}"             
  task_definition = "${aws_ecs_task_definition.cmanager_task.arn}" 
  launch_type     = "FARGATE"
  desired_count   = 1
}