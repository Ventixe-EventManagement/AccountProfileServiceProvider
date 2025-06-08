# AccountProfileServiceProvider

**AccountProfileServiceProvider** is a backend microservice responsible for managing user profile data within the Ventixe platform. It is part of a distributed microservices architecture built using **ASP.NET Core** and is integrated with **Azure services** for authentication, secret management, and scalability.

## Overview

This service exposes a RESTful API for creating, retrieving, and verifying user profile data. It uses **JWT authentication** to secure all endpoints and integrates with **Azure Key Vault** for secure secret management. Profiles are associated with authenticated users via unique user ID claims from the token.

## Features

- Create or update user profiles (authenticated users only)  
- Fetch the authenticated user's profile  
- Check if a user profile exists  
- Full JWT validation including **issuer**, **audience**, and **signing key**  
- Secrets and connection strings retrieved securely from **Azure Key Vault**

## Technology Stack

- **ASP.NET Core 9**  
- **Entity Framework Core**  
- **SQL Server (Azure)**  
- **Azure App Service**  
- **Azure Key Vault**  
- **Swagger / OpenAPI**  
- **JWT-based Authentication**

## Azure Key Vault Integration

Secrets and sensitive configuration values are **not stored in plaintext**. The service uses **Azure Key Vault** together with **system-assigned managed identity** to securely fetch secrets at runtime using `DefaultAzureCredential` from the `Azure.Identity` package.

### Secrets used

| Key                          | Purpose                                  |
|-----------------------------|------------------------------------------|
| `ConnectionStrings--SqlConnection` | SQL Server connection string             |
| `Jwt--Issuer`               | Expected JWT token issuer                |
| `Jwt--Audience`             | Expected JWT token audience              |
| `Jwt--Secret`               | Symmetric key for signing JWT tokens     |

## Security

- All profile endpoints require a valid **JWT access token**  
- Authorization is enforced based on the **user ID claim** in the token  
- Tokens are issued by a separate authentication service (**AuthServiceProvider**)  
- User identity is extracted via the `ClaimTypes.NameIdentifier` claim  
- Only the user themself can access or modify their profile
