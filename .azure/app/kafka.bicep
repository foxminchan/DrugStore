@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

provider 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource KafkaDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'kafka'
    labels: {
      app: 'kafka'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'kafka'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'kafka'
        }
      }
      spec: {
        containers: [
          {
            name: 'kafka'
            image: 'confluentinc/cp-kafka:7.6.0'
            ports: [
              {
                containerPort: 9092
              }
              {
                containerPort: 29092
              }
            ]
            env: [
              {
                name: 'CLUSTER_ID'
                value: 'MkU3OEVBNTcwNTJENDM2Qk'
              }
              {
                name: 'KAFKA_NODE_ID'
                value: '1'
              }
              {
                name: 'KAFKA_LISTENER_SECURITY_PROTOCOL_MAP'
                value: 'CONTROLLER:PLAINTEXT,PLAINTEXT:PLAINTEXT,PLAINTEXT_HOST:PLAINTEXT'
              }
              {
                name: 'KAFKA_ADVERTISED_LISTENERS'
                value: 'PLAINTEXT://kafka:29092,PLAINTEXT_HOST://localhost:9092'
              }
              {
                name: 'KAFKA_LISTENERS'
                value: 'PLAINTEXT://kafka:29092,CONTROLLER://kafka:29093,PLAINTEXT_HOST://0.0.0.0:9092'
              }
              {
                name: 'KAFKA_PROCESS_ROLES'
                value: 'broker,controller'
              }
              {
                name: 'KAFKA_CONTROLLER_QUORUM_VOTERS'
                value: '1@kafka:29093'
              }
              {
                name: 'KAFKA_INTER_BROKER_LISTENER_NAME'
                value: 'PLAINTEXT'
              }
              {
                name: 'KAFKA_CONTROLLER_LISTENER_NAMES'
                value: 'CONTROLLER'
              }
              {
                name: 'KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR'
                value: '1'
              }
            ]
          }
        ]
      }
    }
  }
}

resource KafkaService 'core/Service@v1' = {
  metadata: {
    name: 'kafka'
  }
  spec: {
    selector: {
      app: 'kafka'
    }
    ports: [
      {
        port: 9092
      }
      {
        port: 29092
      }
    ]
    type: 'ClusterIP'
  }
}
