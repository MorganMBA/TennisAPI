# TennisAPI
API REST développée en .NET 8 qui expose les statistiques des joueurs de tennis (IMC moyen, médiane de taille, ratio de victoires par pays)


🚀 Démo en ligne

L’API est actuellement déployée sur AWS EC2 et accessible à l’adresse suivante :

👉 Swagger UI :
🔗 http://ec2-3-79-168-143.eu-central-1.compute.amazonaws.com/swagger/index.html


Architecture du projet
TennisAPI/
│
├── Tennis.API/        → Couche API (Controllers, Swagger, Configuration)
├── Tennis.Domain/     → Couche Métier (Services, Interfaces)
├── Tennis.Core/       → Entités et DTOs
├── Tennis.Infra/      → Accès DynamoDB
└── Tennis.Tests/      → Tests unitaires

⚙️ Prérequis

Avant de lancer le projet, assure-toi d’avoir :

🧩 .NET 8 SDK
🐳 Docker
💾 (Optionnel) AWS CLI configuré pour tester le déploiement :
aws configure

🧰 Installation et exécution locale

1️⃣ Cloner le dépôt

git clone https://github.com/MorganMBA/TennisAPI.git
cd TennisAPI


2️⃣ Restaurer les dépendances

dotnet restore

3️⃣ Étapes pour exécuter le projet en local

Lancer DynamoDB Local avec Docker
docker run -d --name dynamodb-local \
  -p 8000:8000 \
  amazon/dynamodb-local

Créer la table Players dans DynamoDB Local

Avec AWS CLI localement :

aws dynamodb create-table \
  --table-name Players \
  --attribute-definitions AttributeName=Id,AttributeType=N \
  --key-schema AttributeName=Id,KeyType=HASH \
  --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5 \
  --endpoint-url http://localhost:8000 \
  --region eu-central-1

Configurer la connexion à DynamoDB Local

Vérifie ton appsettings.Development.json :
{
  "AWS": {
    "Region": "eu-central-1",
    "DynamoDB": {
      "TableName": "Players",
      "ServiceURL": "http://localhost:8000",
      "UseLocal": true
    }
  }
}

Program.cs utilise la configuration locale :

builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    return new AmazonDynamoDBClient(
        new Amazon.Runtime.BasicAWSCredentials("fakeKey", "fakeSecret"),
        new AmazonDynamoDBConfig
        {
            ServiceURL = "http://localhost:8000",
            AuthenticationRegion = "eu-central-1"
        });
});

Puis lance directement le conteneur :

docker run -d -p 8080:80 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  --name tennis-api \
  520128198500.dkr.ecr.eu-central-1.amazonaws.com/tennis-api:latest
  

🧪 Exécution des tests unitaires
dotnet test
