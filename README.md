# Drug Store

[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=foxminchan_DrugStore&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=foxminchan_DrugStore)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=foxminchan_DrugStore&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=foxminchan_DrugStore)

## Description

<p align="justify">
A sample e-commerce application that enables users to buy and oversee pharmaceuticals and other health-related items. The application will be constructed with a clean architecture and domain-driven design, utilizing .NET 8.0, and runs on Azure AKS 🛞.
</p>

<img src="./assets/DrugStore.png" alt="Drug Store" width="100%">

## Prerequisites

- [K6](https://k6.io/docs/getting-started/installation/)
- [pnpm](https://pnpm.io/installation)
- [Docker](https://docs.docker.com/get-docker/)
- [Node.js](https://nodejs.org/en/download/)
- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

> [!NOTE]
> If you want to deploy the application to Azure, you need to install the [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) and have an [Azure Subscription](https://azure.microsoft.com/en-us/free/).

## Installation

### Local setup

1. Clone the repository

```bash
git clone https://github.com/foxminchan/DrugStore
```

3. Restore the packages and install the dependencies

```bash
cd DrugStore
dotnet restore ./DrugStore.sln
dotnet tool restore
pnpm install
```

4. Set up infrastructure

```bash
docker-compose -f ./docker-compose.yml -f ./docker-compose.o11y.yaml up -d
```

## Usage

### Running the application

From the root directory, run the following command:

```bash
tye run
```

> [!WARNING]
> Ensure that the Docker Desktop is running.

### Testing the application

For load testing, run the following command:

```bash
k6 run ./k6/performance.js
```

For unit testing, run the following command:

```bash
dotnet test ./DrugStore.sln
```

For test the tests, run the following command:

```bash
dotnet stryker
```

## Deployment

To deploy the application to **Azure**, follow the steps below:

1. Login and select the subscription

```bash
az login
az account set --subscription "subscription_id"
```

2. Deploy the infrastructure

```bash
az deployment sub create --location eastus --template-file ./azure/main.bicep
```

3. Login to the Azure Container Registry

```bash
export PASSWORD=YOUR_PASSWORD
echo $PASSWORD | docker login "acr_name".azurecr.io -u "username" --password-stdin
```

4. Build and push the images to the Azure Container Registry

```bash
docker compose build
docker compose push "acr_name".azurecr.io/drug-store-api:latest
docker compose push "acr_name".azurecr.io/drug-store-storefront:latest
docker compose push "acr_name".azurecr.io/drug-store-backoffice:latest
docker compose push "acr_name".azurecr.io/drug-store-webstatus:latest
docker compose push "acr_name".azurecr.io/drug-store-identityserver:latest
```

5. Get the Azure Kubernetes Service credentials

```bash
az aks get-credentials --resource-group "resource_group" --name "aks_name"
```

6. Deploy the application to the Azure Kubernetes Service

```bash
az deployment group create --resource-group "resource_group" --template-file ./azure/app.bicep
```

7. Open the application

```bash
kubectl get ingress
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
