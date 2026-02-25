# P7CreateRestApi

A robust, secure RESTful API built with **.NET 10** and **ASP.NET Core**. This project manages financial and trading data entities, featuring complete CRUD operations, token-based authentication (JWT), and role-based authorization using ASP.NET Core Identity.

## Requirements

- [**.NET 10.0 SDK**](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- **SQL Server**
- **Git**
- An **IDE** of your choice, such as:
    - **Visual Studio**
    - **JetBrains Rider**
    - **Visual Studio Code**

## Getting Started

Follow these steps to get a local copy up and running.

### 1. Clone the Repository and Switch to the Dev Branch

Open your terminal, clone the project, and then switch to the `dev` branch.

```
git clone https://github.com/Floverflow11/Findexium.git
cd Findexium
git checkout dev
```

### 2. Configuration

Ensure you have configured your database connection string and JWT settings in `appsettings.json`.

### 3. Apply Migrations

`dotnet ef database update`.

### 4. Run the Application

`dotnet run`.

## Authentication Flow

Most endpoints in this API require authentication. To access them, follow these steps:

- **Create an User**
  - Send a POST request to `/User` with a `RegisterDto` (UserName, Password, FullName).
  - Newly registered users are automatically assigned the Admin role in the current configuration.
- **Login**
  - Send a POST request to `/Login` with a `LoginDto` (UserName, Password). The API will return a JWT token.
- **Authorize**
  - Include the JWT token in the authorization header of subsequent requests.
  - Authorization: Bearer <your_jwt_token>.