data "aws_caller_identity" "current" {}

data "aws_region" "current" {}

data "aws_ssm_parameter" "ssm_lambda_execution_role_arn" {
  name = "/Incomes/Common/Lambda/LambdaExecutionRoleReadSQSArn"
}

data "aws_ssm_parameter" "ssm_queue_attach_arn" {
  name = "/Incomes/Common/SQS/QueueIncomesReceivedArn"
}

data "aws_ssm_parameter" "code_s3_bucket_name" {
  name = "/Incomes/LambdaProcessReceived/Code/S3BucketName"
}

data "aws_ssm_parameter" "code_s3_bucket_key" {
  name = "/Incomes/LambdaProcessReceived/Code/S3BucketKey"
}