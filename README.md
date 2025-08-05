# ClientAPI

**ClientAPI** is a simple and secure ASP.NET Core Web API backend that manages client data and authentication for administrative systems. It supports core CRUD operations for clients and provides authentication with JWT and refresh tokens.

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

> **Note:** All client routes are **restricted to ADMIN role** and protected with JWT-based authorization.

## ⚙️ Tech Stack

- **ASP.NET Core Web API** (.NET 8)
- **Entity Framework Core** for ORM
- **LINQ** for querying
- **RESTful API** design
- **SOLID Principles**
- **Repository Pattern** for clean separation of concerns

## 📦 Project Structure

```
ClientAPI/
│
├── Controllers/
│   ├── AuthController.cs
│   └── ClientController.cs
│
├── Data/
│   ├── DTOs/                # Data transfer objects
│   ├── Models/              # Entity models
│   ├── Enums/               # Enum definitions
│   ├── Response/            # Response model wrappers
│   └── DataContext.cs       # EF Core database context
│
├── Repositories/            # Interfaces & implementations for data access
│
├── Utils/
│   ├── AuthUtil.cs          # JWT handling
│   ├── FinanceHelper.cs     # Finance-related utilities
│   ├── ModelMapper.cs       # DTO-to-model mappings
│   ├── ResponseHelper.cs    # Standardized API responses
│   ├── TimeUtils.cs         # Time formatting helpers
│   └── UserUtils.cs         # Get user info from HttpContext
│
├── Program.cs               # App entry point
└── appsettings.json         # Configuration file
```

## 🚀 Getting Started

### 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or modify `DbContext` for your database)
- Postman or Swagger for API testing

### 🛠️ Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/matthew-gernale/ClientAPI.git
   cd ClientAPI
   ```

2. **Configure your database and JWT in `appsettings.json`**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "your-database-connection-string"
     },
   }
   ```

3. **Apply Migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the project**
   ```bash
   dotnet run
   ```

5. **Test API using Postman or visit Swagger UI**
   - API: `https://localhost:7045/api`
   - Swagger: `https://localhost:7045/swagger`

## 🔐 Authentication Guide

### Step-by-Step Authorization with JWT in Postman

#### a. Login and Get Access Token

- **Method:** `POST`
- **URL:** `https://localhost:7045/api/auth/login`
- **Body (JSON):**
  ```json
  {
    "Email": "admin@gmail.com",
    "Password": "Admin2025"
  }
  ```
- **Response (JSON):**
  ```json
  {
    "AccessToken": "eyJhbGciOiJIUzI1NiIs...",
    "RefreshToken": "..."
  }
  ```

#### b. Use Access Token for Secured APIs

1. Copy the `accessToken` from the login response
2. In Postman, go to the **Authorization** tab:
   - **Type:** Bearer Token
   - **Token:** paste the `accessToken`
3. Make authorized requests:
   - `GET https://localhost:7045/api/client/paginated`
   - `POST https://localhost:7045/api/client/add`
   - `PUT https://localhost:7045/api/client/details`
   - `DELETE https://localhost:7045/api/client/underage`

## 📋 API Endpoints

### Authentication Endpoints
| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/login` | User login | No |
| POST | `/api/auth/refresh-token` | Refresh access token | No |
| POST | `/api/auth/logout` | User logout | Yes |

### Client Management Endpoints
| Method | Endpoint | Description | Auth Required | Role |
|--------|----------|-------------|---------------|------|
| POST | `/api/client/add` | Create new client | Yes | Admin |
| GET | `/api/client/paginated` | Get paginated clients | Yes | Admin |
| GET | `/api/client/details/{id}` | Get client details | Yes | Admin |
| PUT | `/api/client/details` | Update client | Yes | Admin |
| DELETE | `/api/client/underage` | Delete underage clients | Yes | Admin |

## 🔧 Configuration

### Database Configuration
The application uses Entity Framework Core with SQL Server by default. Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ClientApiDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Sample Client Data
```json
{
  "FirstName": "John",
  "LastName": "Doe",
  "Email": "john.doe@example.com",
  "Phone": "+1234567890",
  "DateOfBirth": "1990-01-01",
  "Address": "123 Main St, City, State 12345"
}
```


## 👤 Author

**Matthew Gernale**
- GitHub: [@matthew-gernale](https://github.com/matthew-gernale)
- Email: matthewgernale26@gmail.com

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📞 Support

If you have any questions or need help, please open an issue on GitHub or contact the author.
