@description('Azure region to deploy resources into. Defaults to location of target resource group')
param location string = resourceGroup().location

@description('Name of the AKS cluster. Defaults to "drugstore"')
param clusterName string = 'drugstore'

@description('The size of the Virtual Machine.')
param vmSize string = 'standard_d2s_v3'

@description('The number of nodes for the cluster.')
@minValue(1)
@maxValue(50)
param agentCount int = 3

resource aksCluster 'Microsoft.ContainerService/managedClusters@2023-11-01' = {
  name: clusterName
  location: location
  properties: {
    kubernetesVersion: '1.29.2'
    agentPoolProfiles: [
      {
        name: 'agentpool'
        osDiskSizeGB: 0
        count: agentCount
        vmSize: vmSize
        osType: 'Linux'
        mode: 'System'
      }
    ]
    dnsPrefix: '${clusterName}-dns'
    enableRBAC: true
    addonProfiles: {
      httpApplicationRouting: {
        enabled: true
      }
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

output kubeConfig string = aksCluster.name
