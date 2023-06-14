################################################################################
# Common - Tags
################################################################################

variable "tags" {
  type    = map(string)
  default = {
    environment = "dev"
    feature     = "incomes"
  }
}

################################################################################
# Lambda - Common
################################################################################

variable "function_name" {
  type    = string
  default = "lambda_process_received"
}

variable "description" {
  type    = string
  default = "Process - Received Incomes"
}

variable "runtime" {
  type    = string
  default = "dotnet6"
}

variable "handler" {
  type    = string
  default = "JacksonVeroneze.Income.LambdaProcessReceived::JacksonVeroneze.Income.LambdaProcessReceived.Function::FunctionHandler"
}

variable "memory_size" {
  type    = number
  default = 128
}

variable "timeout" {
  type    = number
  default = 15
}

variable "enable_xray" {
  type    = bool
  default = true
}

variable "environment_variables" {
  type    = map(string)
  default = {
    "parameter1" = "value"
    "parameter2" = "value"
  }
}

variable "code_path" {
  type    = string
  default = "../../src/main/JacksonVeroneze.Income.LambdaProcessReceived/code.zip"
}