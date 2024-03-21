@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

@description('Address of the container registry where container resides')
param containerRegistry string

@description('Tag of container to use')
param containerTag string = 'latest'

provider kubernetes with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource storeFrontDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'store-front'
    labels: {
      app: 'store-front'
    }
  }
  spec: {
    replicas: 3
    selector: {
      matchLabels: {
        app: 'store-front'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'store-front'
        }
      }
      spec: {

        containers: [
          {
            name: 'store-front'
            image: '${containerRegistry}/drug-store-storefront:${containerTag}'
            imagePullPolicy: 'Always'
            env: [
              {
                name: 'ASPNETCORE_ENVIRONMENT'
                value: 'Development'
              }
            ]
          }
        ]
      }
    }
  }
}

resource backOfficeService 'core/Service@v1' = {
  metadata: {
    name: 'store-front'
  }
  spec: {
    selector: {
      app: 'store-front'
    }
    ports: [
      {
        port: 7060
        targetPort: '7060'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
