terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "5.1.0"
    }
  }

  backend "s3" {
    bucket  = "incomes-infra-state"
    key     = "lambda-processreceived-state"
    region  = "us-east-1"
    profile = "terraform"
  }
}

provider "aws" {
  region  = "us-east-1"
  profile = "terraform"
}
