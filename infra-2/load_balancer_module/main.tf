resource "aws_alb" "application_load_balancer" {
  name               = "lb-tf-cmanager"
  load_balancer_type = "application"
  subnets = [ 
    var.default_subnet_a_id,
    var.default_subnet_b_id,
    var.default_subnet_c_id
  ]
  security_groups = ["${aws_security_group.load_balancer_security_group.id}"]
}
 
resource "aws_security_group" "load_balancer_security_group" {
  ingress {
    from_port   = 80
    to_port     = 80
    cidr_blocks = ["0.0.0.0/0"]
    protocol    = "tcp"
  }

  egress {
    from_port   = 0
    to_port     = 0
    cidr_blocks = ["0.0.0.0/0"]
    protocol    = "-1"
  }
}

resource "aws_lb_target_group" "target_group" {
  name        = "target-group-cmanager"
  port        = 80
  protocol    = "HTTP"
  target_type = "ip"
  vpc_id      = var.default_vpc_id
  health_check {
    matcher = "200,301,302"
    path = "/health"
  }
}

resource "aws_lb_listener" "listener" {
  load_balancer_arn = "${aws_alb.application_load_balancer.arn}"
  port              = "80"
  protocol          = "HTTP"
  default_action {
    type             = "forward"
    target_group_arn = "${aws_lb_target_group.target_group.arn}"
  }
}

output "aws_lb_target_group" {
  value = aws_lb_target_group.target_group
}

output "load_balancer_security_group" {
  value = aws_security_group.load_balancer_security_group
}