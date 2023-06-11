################################################################################
# Common
################################################################################

variable "tags" {
  type = map(string)
  default = {
    environment = "dev"
    feature     = "incomes"
    terraform   = "true"
  }
}
