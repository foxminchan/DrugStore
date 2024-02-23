# Drug Store

## Description

<p align="justify">
An application that allows users to search for drugs and get information about them. The application also allows users to search for drugs based on their location and get a list of drug stores that have the drugs in stock.
</p>

## Prerequisites

- [K6](https://k6.io/docs/getting-started/installation/)
- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/)

## Installation

1. Clone the repository

```bash
git clone https://github.com/foxminchan/DrugStore
```

3. Restore the packages

```bash
cd DrugStore && dotnet restore ./DrugStore.sln
```

4. Set up infrastructure

```bash
docker-compose -f ./docker-compose.yml up -d
```

### Cloudinary Configuration

<p align="justify">
Navigate to the <a href="https://cloudinary.com/">Cloudinary</a> website and create an account. After creating an account, navigate to the dashboard and copy the cloud name, API key, and API secret. Create a file named <code>appsettings.Development.json</code> in the <code>DrugStore.Presentation</code> project and add the following code:
</p>

```json
{
  "Cloudinary": {
    "CloudName": "cloud_name",
    "ApiKey": "api_key",
    "ApiSecret": "api_secret"
  }
}
```

## Usage

### Running the application

```bash
dotnet watch -p ./src/Api/DrugStore.Presentation/ run -lp https
dotnet watch -p ./src/Web/DrugStore.StoreFront/ run -lp https
dotnet watch -p ./src/Web/DrugStore.WebStatus/ run -lp https
```

### Running the tests

For load testing, run the following command:

```bash
k6 run ./k6/performance.js
```

For unit testing, run the following command:

```bash
dotnet test ./DrugStore.sln
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
