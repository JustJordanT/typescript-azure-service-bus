output "servicebus_queue_name" {
  value = module.azure_service_bus.servicebus_queue_name
  sensitive = true
}

output "servicebus_connection_string" {
  value = module.azure_service_bus.servicebus_connection_string
  sensitive = true
}