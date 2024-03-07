@description('Name of the container registry. Defaults to unique hashed ID prefixed with "drugstore"')
param registryName string = 'drugstore-${uniqueString(resourceGroup().id)}'

@description('Name of the AKS cluster. Defaults to a unique hash prefixed with "drugstore"')
param clusterName string = 'drugstore'

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-08-01-preview' existing = {
  name: registryName
}

resource aksCluster 'Microsoft.ContainerService/managedClusters@2023-07-02-preview' existing = {
  name: clusterName
}

module postgres 'app/postgres.bicep' = {
  name: 'postgres'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module redis 'app/redis.bicep' = {
  name: 'redis'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module kafka 'app/kafka.bicep' = {
  name: 'kafka'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module kafkaConnect 'app/kafka-connect.bicep' = {
  name: 'kafkaConnect'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module minio 'app/minio.bicep' = {
  name: 'minio'
  params: {
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module api 'app/api.bicep' = {
  name: 'api'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module storeFront 'app/store-front.bicep' = {
  name: 'storeFront'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module backOffice 'app/back-office.bicep' = {
  name: 'backOffice'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module identityServer 'app/identity-server.bicep' = {
  name: 'identityServer'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module ingress 'app/ingress.bicep' = {
  name: 'ingress'
  params: {
    HTTPApplicationRoutingZoneName: aksCluster.properties.addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}
