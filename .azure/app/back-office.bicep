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

resource backOfficeDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'back-office'
    labels: {
      app: 'back-office'
    }
  }
  spec: {
    replicas: 3
    selector: {
      matchLabels: {
        app: 'back-office'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'back-office'
        }
      }
      spec: {

        containers: [
          {
            name: 'back-office'
            image: '${containerRegistry}/drug-store-backoffice:${containerTag}'
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
    name: 'back-office'
  }
  spec: {
    selector: {
      app: 'back-office'
    }
    ports: [
      {
        port: 7040
        targetPort: '7040'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
