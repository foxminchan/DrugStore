@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

@description('Address of the container registry where container resides')
param containerRegistry string

@description('Tag of container to use')
param containerTag string = 'latest'

provider 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource apiDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'api'
    labels: {
      app: 'api'
    }
  }
  spec: {
    replicas: 3
    selector: {
      matchLabels: {
        app: 'api'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'api'
        }
      }
      spec: {

        containers: [
          {
            name: 'api'
            image: '${containerRegistry}/drug-store-api:${containerTag}'
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

resource apiService 'core/Service@v1' = {
  metadata: {
    name: 'api'
  }
  spec: {
    selector: {
      app: 'api'
    }
    ports: [
      {
        port: 7070
        targetPort: '7070'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
