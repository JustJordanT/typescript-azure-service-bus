locals {
  resource_group_name = "azure-servicebus-rg"
  servicebus_namespace_name = "azure-servicebus-ns"
}

resource "azurerm_resource_group" "servicebus_rg" {
  name     = local.resource_group_name
  location = "Central US"
}

resource "azurerm_servicebus_namespace" "servicebus_ns" {
  name                = local.servicebus_namespace_name
  location            = azurerm_resource_group.servicebus_rg.location
  resource_group_name = azurerm_resource_group.servicebus_rg.name
  sku                 = "Basic"

  tags = {
    source = "terraform"
  }
}

resource "azurerm_servicebus_queue" "servicebus_queue" {
  name                = "servicebus-master-queue"
  namespace_id        = azurerm_servicebus_namespace.servicebus_ns.id
  partitioning_enabled = false
}