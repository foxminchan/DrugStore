@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

provider 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource minioDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'minio'
    labels: {
      app: 'minio'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'minio'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'minio'
        }
      }
      spec: {
        containers: [
          {
            name: 'minio'
            image: 'minio/minio:latest'
            ports: [
              {
                containerPort: 9001
              }
            ]
            env: [
              {
                name: 'MINIO_ACCESS_KEY'
                value: 'admin'
              }
              {
                name: 'MINIO_SECRET_KEY'
                value: 'drugstore'
              }
            ]
            volumeMounts: [
              {
                name: 'minio-storage'
                mountPath: '/data'
              }
            ]
          }
        ]
        volumes: [
          {
            name: 'minio-storage'
            emptyDir: {}
          }
        ]
      }
    }
  }
}

resource minioService 'core/Service@v1' = {
  metadata: {
    name: 'minio'
  }
  spec: {
    selector: {
      app: 'minio'
    }
    ports: [
      {
        port: 9001
        targetPort: '9001'
      }
    ]
    type: 'ClusterIP'
  }
}
