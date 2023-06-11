resource "aws_cloudwatch_log_group" "lambda_process_received" {
  name              = "/aws/lambda/lambda_process_received"
  retention_in_days = 1
}

resource "aws_s3_object" "lambda_zip" {
  bucket = "incomes-lambdas-zip"
  key    = "JacksonVeroneze.Income.LambdaProcessReceived.zip"
  source = "../../src/main/JacksonVeroneze.Income.LambdaProcessReceived/bin/Release/net6.0/JacksonVeroneze.Income.LambdaProcessReceived.zip"
}

resource "aws_lambda_function" "lambda_process_received" {
  function_name                  = "LambdaProcessReceived"
  role                           = data.aws_ssm_parameter.ssm_lambda_execution_role_arn.value
  architectures                  = ["x86_64"]
  description                    = "Lambda - ProcessReceived"
  handler                        = "JacksonVeroneze.Income.LambdaProcessReceived::JacksonVeroneze.Income.LambdaProcessReceived.Function::FunctionHandler"
  runtime                        = "dotnet6"
  publish                        = true
  timeout                        = 15
  memory_size                    = 128
  reserved_concurrent_executions = -1
  s3_bucket                      = "incomes-lambdas-zip"
  s3_key                         = aws_s3_object.lambda_zip.key
  s3_object_version              = aws_s3_object.lambda_zip.version_id
  environment {
    variables = {
      env = "dev"
    }
  }
  tracing_config {
    mode = "Active"
  }
  tags = var.tags
}

resource "aws_lambda_event_source_mapping" "aws_lambda_event" {
  enabled          = true
  event_source_arn = data.aws_ssm_parameter.ssm_queue_incomesreceived_arn.value
  function_name    = aws_lambda_function.lambda_process_received.arn
}
