@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

provider kubernetes with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource postgresDeployment 'apps/StatefulSet@v1' = {
  metadata: {
    name: 'postgres'
    labels: {
      app: 'postgres'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'postgres'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'postgres'
        }
      }
      spec: {
        containers: [
          {
            name: 'postgres'
            image: 'postgres:16.2'
            imagePullPolicy: 'Always'
            ports: [
              {
                containerPort: 5432
              }
            ]
            env: [
              {
                name: 'POSTGRES_DB'
                value: 'postgres'
              }
              {
                name: 'POSTGRES_USER'
                value: 'postgres'
              }
              {
                name: 'POSTGRES_PASSWORD'
                value: 'postgres'
              }
            ]
          }
        ]
      }
    }
    volumeClaimTemplates: [
      {
        metadata: {
          name: 'postgres-data'
        }
        spec: {
          accessModes: [ 'ReadWriteOnce' ]
          resources: {
            requests: {
              storage: '1Gi'
            }
          }
        }
      }
    ]
    serviceName: 'postgres'
  }
}

resource postgresService 'core/Service@v1' = {
  metadata: {
    name: 'postgres'
    labels: {
      app: 'postgres'
    }
  }
  spec: {
    selector: {
      app: 'postgres'
    }
    ports: [
      {
        port: 5432
        targetPort: '5432'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
