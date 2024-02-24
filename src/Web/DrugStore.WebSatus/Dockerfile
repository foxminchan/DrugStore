FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7050

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/Web/DrugStore.WebSatus/DrugStore.WebSatus.csproj", "src/Web/DrugStore.WebSatus/"]
COPY ["src/Api/DrugStore.Infrastructure/DrugStore.Infrastructure.csproj", "src/Api/DrugStore.Infrastructure/"]
COPY ["src/Api/DrugStore.Domain/DrugStore.Domain.csproj", "src/Api/DrugStore.Domain/"]
RUN dotnet restore "./src/Web/DrugStore.WebSatus/DrugStore.WebSatus.csproj"
COPY . .
WORKDIR "/src/src/Web/DrugStore.WebSatus"
RUN dotnet build "./DrugStore.WebSatus.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DrugStore.WebSatus.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DrugStore.WebSatus.dll"]