FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Web/DrugStore.WebSatus/DrugStore.WebSatus.csproj", "src/Web/DrugStore.WebSatus/"]
RUN dotnet restore "./src/Web/DrugStore.WebSatus/DrugStore.WebSatus.csproj"
COPY . .
WORKDIR "/src/src/Web/DrugStore.WebSatus"
RUN dotnet build "./DrugStore.WebSatus.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DrugStore.WebSatus.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["./DrugStore.WebSatus"]