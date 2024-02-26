targetScope = 'subscription'

@minLength(3)
@maxLength(11)
param resourceGroupName string = 'drugstore'

param location string = deployment().location

resource newRg 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: resourceGroupName
  location: location
}

module infra 'infra.bicep' = {
  scope: newRg
  name: 'infra'
  params: {
    location: location
  }
}
