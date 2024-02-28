@description('Azure region to deploy resources into. Defaults to location of target resource group')
param location string = resourceGroup().location

@description('Name of the container registry. Defaults to unique hashed ID prefixed with "drugstore"')
param registryName string = 'drugstore-${uniqueString(resourceGroup().id)}'

@description('SKU of the container registry. Defaults to "Basic"')
param sku string = 'Standard'

resource acr 'Microsoft.ContainerRegistry/registries@2023-08-01-preview' = {
  name: registryName
  location: location
  sku: {
    name: sku
  }
  properties: {
    anonymousPullEnabled: true
    adminUserEnabled: false
  }
}

output registryName string = acr.name
