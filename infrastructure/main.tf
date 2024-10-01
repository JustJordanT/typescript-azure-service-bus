module "azure_service_bus" {
  source = "./modules/azure/service_bus"
  providers = {
    azurerm = azurerm
  }
}