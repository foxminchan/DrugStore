@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

provider 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource KafkaConnectDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'kafka-connect'
    labels: {
      app: 'kafka-connect'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'kafka-connect'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'kafka-connect'
        }
      }
      spec: {
        containers: [
          {
            name: 'kafka-connect'
            image: 'confluentinc/cp-kafka-connect:7.6.0'
            command: [
              'bash'
              '-c'
              'echo "Installing Connector Plugins" && confluent-hub install --no-prompt debezium/debezium-connector-postgresql:2.2.1 && confluent connect plugin installjcustenborder/kafka-connect-redis:latest && echo "Starting Kafka Connect" && /etc/confluent/docker/run'
            ]
            ports: [
              {
                containerPort: 8083
              }
            ]
            env: [
              {
                name: 'CONNECT_BOOTSTRAP_SERVERS'
                value: 'kafka:29092'
              }
              {
                name: 'CONNECT_GROUP_ID'
                value: 'categories-connect'
              }
              {
                name: 'CONNECT_CONFIG_STORAGE_TOPIC'
                value: '_categories-connect-configs'
              }
              {
                name: 'CONNECT_OFFSET_STORAGE_TOPIC'
                value: '_categories-connect-offsets'
              }
              {
                name: 'CONNECT_STATUS_STORAGE_TOPIC'
                value: '_categories-connect-status'
              }
              {
                name: 'CONNECT_KEY_CONVERTER'
                value: 'org.apache.kafka.connect.json.JsonConverter'
              }
              {
                name: 'CONNECT_VALUE_CONVERTER'
                value: 'org.apache.kafka.connect.json.JsonConverter'
              }
              {
                name: 'CONNECT_INTERNAL_KEY_CONVERTER'
                value: 'org.apache.kafka.connect.json.JsonConverter'
              }
              {
                name: 'CONNECT_INTERNAL_VALUE_CONVERTER'
                value: 'org.apache.kafka.connect.json.JsonConverter'
              }
              {
                name: 'CONNECT_PLUGIN_PATH'
                value: '/usr/share/java,/usr/share/confluent-hub-components'
              }
              {
                name: 'CONNECT_REST_ADVERTISED_HOST_NAME'
                value: 'kafka-connect'
              }
              {
                name: 'CONNECT_REPLICATION_FACTOR'
                value: '1'
              }
              {
                name: 'CONNECT_CONFIG_STORAGE_REPLICATION_FACTOR'
                value: '1'
              }
              {
                name: 'CONNECT_OFFSET_STORAGE_REPLICATION_FACTOR'
                value: '1'
              }
              {
                name: 'CONNECT_STATUS_STORAGE_REPLICATION_FACTOR'
                value: '1'
              }
            ]
          }
        ]
      }
    }
  }
}

resource KafkaConnectService 'core/Service@v1' = {
  metadata: {
    name: 'kafka-connect'
  }
  spec: {
    selector: {
      app: 'kafka-connect'
    }
    ports: [
      {
        protocol: 'TCP'
        port: 8083
        targetPort: '8083'
      }
    ]
    type: 'ClusterIP'
  }
}
