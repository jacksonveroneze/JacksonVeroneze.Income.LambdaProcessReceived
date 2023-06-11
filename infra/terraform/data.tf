data "aws_ssm_parameter" "ssm_lambda_execution_role_arn" {
  name = "/Incomes/Common/Lambda/LambdaExecutionRoleReadSQSArn"
}

data "aws_ssm_parameter" "ssm_queue_incomesreceived_arn" {
  name = "/Incomes/Common/SQS/QueueIncomesReceivedArn"
}
