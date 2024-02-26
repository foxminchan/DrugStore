@description('The location of the resources to deploy.')
param location string = resourceGroup().location

@description('Which mode to deploy the infrastructure. Defaults to cloud, which deploys everything. The mode dev only deploys the resources needed for local development.')

module registry 'infra/acr.bicep' = {
  name: 'registry'
  params: {
    location: location
  }
}

module app 'infra/aks.bicep' = {
  name: 'aks'
  params: {
    location: location
  }
}
