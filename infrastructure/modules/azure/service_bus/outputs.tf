output "servicebus_queue_name" {
  value       = azurerm_servicebus_queue.servicebus_queue.name
  sensitive   = true
  description = "description"
  depends_on  = []
}

// connection string for service bus
output "servicebus_connection_string" {
  value       = azurerm_servicebus_namespace.servicebus_ns.default_primary_connection_string
  sensitive   = true
  description = "description"
  depends_on  = []
}