# Consultez https://aka.ms/customizecontainer pour savoir comment personnaliser votre conteneur de débogage et comment Visual Studio utilise ce Dockerfile pour générer vos images afin d’accélérer le débogage.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80


# Cette phase est utilisée pour générer le projet de service
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tennis.API/Tennis.API.csproj", "Tennis.API/"]
COPY ["Tennis.Domain/Tennis.Domain.csproj", "Tennis.Domain/"]
COPY ["Tennis.Core/Tennis.Core.csproj", "Tennis.Core/"]
COPY ["Tennis.Infra/Tennis.Infra.csproj", "Tennis.Infra/"]
COPY ["Tennis.Tests/Tennis.Tests.csproj", "Tennis.Tests/"]
RUN dotnet restore "./Tennis.API/Tennis.API.csproj"
RUN dotnet restore "./Tennis.Tests/Tennis.Tests.csproj"
COPY . .
WORKDIR "/src/Tennis.Tests"
RUN dotnet test --no-restore -c $BUILD_CONFIGURATION
WORKDIR "/src/Tennis.API"
RUN dotnet build "./Tennis.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tennis.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Cette phase est utilisée en production ou lors de l’exécution à partir de VS en mode normal (par défaut quand la configuration de débogage n’est pas utilisée)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tennis.API.dll"]