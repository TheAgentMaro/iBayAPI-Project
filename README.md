# Projet iBay

Le projet iBay est une plateforme de commerce électronique avec une API RESTful qui prend en charge diverses fonctionnalités liées aux utilisateurs, aux produits et au panier d'achat.

## Table des matières
- [Installation](#installation)
- [Configuration](#configuration)
- [Endpoints de l'API](#endpoints-de-lapi)
- [Documentation de l'API](#documentation-de-lapi)
- [Exemples de requêtes](#exemples-de-requetes)
- [Authentification](#authentification)
- [Licence](#licence)

## Installation

1. Clonez le dépôt :

    ```bash
    git clone https://github.com/TheAgentMaro/iBayAPI-Project.git
    ```

2. Accédez au répertoire du projet :

    ```bash
    cd iBayAPI-Project
    ```

3. Installez les dépendances :

    ```bash
    dotnet restore
    ```

4. Appliquez les migrations pour créer la base de données :

    ```bash
    Add-Migration (Migration name)
    Update-Database
    ```

5. Lancez l'application :

    ```bash
    dotnet run
    ```

## Configuration

Avant de lancer l'application, assurez-vous de configurer les paramètres nécessaires dans le fichier `appsettings.json`, tels que les chaînes de connexion à la base de données et les clés secrètes pour l'authentification.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "VotreChaineDeConnexion"
  },
  "Jwt": {
    "Issuer": "VotreEmetteur",
    "Audience": "VotreAudience",
    "SecretKey": "VotreCleSecrete",
    "ExpireDays": 30
  }
}
```

## Endpoints de l'API

L'API offre les fonctionnalités suivantes :

    - Gestion des utilisateurs (CRUD)
    - Gestion des produits (CRUD)
    - Gestion du panier d'achat (ajout, suppression, paiement)

## Documentation de l'API (Swagger)

```bash
https://localhost:44378/swagger/index.html
```

## Exemples de requêtes

- Créer un nouvel utilisateur

```json
POST /api/User/register
{
    "username": "UtilisateurTest",
    "Email": "test@example.com",
    "Password": "MotDePasse123",
    "Role": "Seller"
}
```

- Se Connecter :

```json
POST /api/User/login
{
    "Email": "test@example.com",
    "Password": "MotDePasse123",
}
```

- Créer un produit : 

```json
POST /api/Product
{
  "name": "Ordinateur",
  "image": "image.png",
  "price": 950,
  "available": true,
  "addedTime": "2024-02-06T19:47:02.072Z",
  "sellerId": "1",
  "seller": {
    "username": "UtilisateurTest",
    "email": "test@example.com",
    "role": "Seller"
  }
}
```
- Ajouter un article au panier :

```json
POST /api/Cart
{
  "userId": "1",
  "userName": "sami",
  "user": {
    "email": "sami@example.com",
    "username": "sami",
    "role": "User"
  },
  "product": {
    "name": "Ordinateur",
    "image": "image.png",
    "price": 950,
    "available": true,
    "addedTime": "2024-02-06T19:47:02.072Z",
    "sellerId": "1",
    "seller": {
      "email": "selleruser@example.com",
      "username": "selleruser",
      "role": "Seller"
    }
  },
  "quantity": 2,
  "price": 1900,
  "isPaid": false
}
```


- Paiement / Checkout Process

```json
POST /api/Cart/Checkout
```

## Authentification

L'API utilise JSON Web Tokens (JWT) pour l'authentification. Assurez-vous d'inclure le jeton dans l'en-tête de vos requêtes sécurisées.

Exemple :

```bash
GET /api/User/1
Authorization: Bearer TokenJWT
```

## Licence

Ce projet est sous licence MIT.