# TennisAPI
API REST développée en .NET 8 qui expose les statistiques des joueurs de tennis (IMC moyen, médiane de taille, ratio de victoires par pays)


🚀 Démo en ligne

L’API est actuellement déployée sur AWS EC2 et accessible à l’adresse suivante :

👉 Swagger UI :
🔗 http://ec2-63-178-193-58.eu-central-1.compute.amazonaws.com/swagger/index.html


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


3️⃣ Lancer l’API localement

cd Tennis.API
dotnet run

🧪 Exécution des tests unitaires
dotnet test
