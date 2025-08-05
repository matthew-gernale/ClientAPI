# ClientAPI

**ClientAPI** is a simple and secure ASP.NET Core Web API backend that manages client data and authentication for administrative systems. It supports core CRUD operations for clients and provides authentication with JWT and refresh tokens.

---

## ✅ Features

### 👤 Authentication (`/api/auth`)
- `POST /login` — Login and receive JWT + refresh token (stored in cookie)
- `POST /refresh-token` — Refresh access token using stored refresh token
- `POST /logout` — Clear refresh token and log out

### 🧾 Client Management (`/api/client`)
- `POST /add` — Create a new client
- `GET /paginated` — Retrieve clients in a paginated format
- `GET /details/{clientId}` — Get detailed information about a client
- `PUT /details` — Update client details
- `DELETE /underage` — Delete all clients under the legal age

> All client routes are **restricted to ADMIN role** and protected with JWT-based authorization.

---

## ⚙️ Tech Stack

- **ASP.NET Core Web API** (.NET 8)
- **Entity Framework Core** for ORM
- **LINQ** for querying
- **RESTful API** design
- **SOLID Principles**
- **Repository Pattern** for clean separation of concerns

---

## 📦 Project Structure
ClientAPI/
│
├── Controllers/ # API endpoints (ClientController, AuthController)
│
├── Data/
│ ├── DTOs/ # Data transfer objects
│ ├── Models/ # Entity models
│ ├── Enums/ # Enum types
│ ├── Response/ # Response models
│ └── DataContext.cs # EF Core DbContext setup
│
├── Repositories/ # Interfaces and implementations for data access
│
├── Utils/
│ ├── AuthUtil.cs # JWT generation and validation helpers
│ ├── FinanceHelper.cs # Utilities for financial calculations
│ ├── ModelMapper.cs # Mapping between entities and DTOs
│ ├── ResponseHelper.cs # Response formatting and status helpers
│ ├── TimeUtils.cs # Time conversion utilities
│ └── UserUtils.cs # Extracting UserId and Role from HttpContext
│
├── Program.cs # Main entry point with DI, middleware, etc.
└── appsettings.json # App configuration file

---

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or modify `DbContext` for your database)
- Postman or Swagger for API testing

### 🛠️ Setup Instructions

1. **Clone the repository**
```bash
git clone https://github.com/your-username/ClientAPI.git
cd ClientAPI

2. **Configure your database and JWT in appsettings.json**
{
  "ConnectionStrings": {
    "DefaultConnection": "your-database-connection-string"
  },
}

3. **Apply Migrations**
dotnet ef database update

4. **Run the project**
dotnet run

5. **Test API using Postman**
🔐 Step-by-Step Authorization with JWT in Postman
a. Login and Get Access Token

Method: POST
URL: https://localhost:{port}/api/auth/login

Body (JSON):
{
  "Email": "admin@gmail.com",
  "Password": "Admin2025"
}

Response (JSON):
{
  "AccessToken": "eyJhbGciOiJIUzI1NiIs...",
  "RefreshToken": "...",
  ...
}

b. Use Access Token for Secured APIs
Copy the accessToken from the login response.
In Postman, go to the Authorization tab:

Type: Bearer Token
Token: paste the accessToken

Make authorized requests like:
GET https://localhost:{port}/api/client/paginated
POST https://localhost:{port}/api/client/add

etc.
---

👤 Author
Matthew Gernale
GitHub
