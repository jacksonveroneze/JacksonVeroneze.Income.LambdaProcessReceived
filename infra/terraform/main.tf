resource "aws_s3_object" "lambda_zip" {
  bucket = data.aws_ssm_parameter.code_s3_bucket_name.value
  key    = data.aws_ssm_parameter.code_s3_bucket_key.value
  source = var.code_path
}

module "lambda" {
  source                = "git@github.com:jacksonveroneze/JacksonVeroneze.Terraform.Modules.git//src/lambda?ref=development"
  function_name         = var.function_name
  description           = var.description
  runtime               = var.runtime
  handler               = var.handler
  memory_size           = var.memory_size
  timeout               = var.timeout
  execution_role_arn    = data.aws_ssm_parameter.ssm_lambda_execution_role_arn.value
  s3_bucket             = data.aws_ssm_parameter.code_s3_bucket_name.value
  s3_key                = data.aws_ssm_parameter.code_s3_bucket_key.value
  environment_variables = var.environment_variables
  enable_xray           = var.enable_xray
  event_source_enabled  = true
  event_source_arn      = data.aws_ssm_parameter.ssm_queue_attach_arn.value
  custom_tags           = var.tags
  depends_on            = [aws_s3_object.lambda_zip]
}
