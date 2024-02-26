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

resource webStatusDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'web-status'
    labels: {
      app: 'web-status'
    }
  }
  spec: {
    replicas: 3
    selector: {
      matchLabels: {
        app: 'web-status'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'web-status'
        }
      }
      spec: {

        containers: [
          {
            name: 'web-status'
            image: '${containerRegistry}/web-status:${containerTag}'
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
    name: 'web-status'
  }
  spec: {
    selector: {
      app: 'web-status'
    }
    ports: [
      {
        port: 7050
        targetPort: '7050'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
