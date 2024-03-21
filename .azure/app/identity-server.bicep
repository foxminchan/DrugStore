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

resource identityServerDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'identity-server'
    labels: {
      app: 'identity-server'
    }
  }
  spec: {
    replicas: 3
    selector: {
      matchLabels: {
        app: 'identity-server'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'identity-server'
        }
      }
      spec: {

        containers: [
          {
            name: 'identity-server'
            image: '${containerRegistry}/drug-store-identityserver:${containerTag}'
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
    name: 'identity-server'
  }
  spec: {
    selector: {
      app: 'identity-server'
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
