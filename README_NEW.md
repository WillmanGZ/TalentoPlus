# TalentoPlus

Employee management system with web application and REST API.

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 10.0
- **Database**: PostgreSQL
- **Authentication**: ASP.NET Identity
- **API**: REST with Swagger/OpenAPI
- **Containerization**: Docker & Docker Compose

## 📁 Project Structure

- **RRHH.WEB** - Web application with admin dashboard
- **TalentoPlus.API** - REST API
- **docker-compose.yaml** - Container orchestration

## 🚀 Running the Application

### Prerequisites

- .NET 10.0 SDK
- Docker & Docker Compose
- Visual Studio 2022 or VS Code

### Option 1: Docker Compose (Recommended)

```bash
docker-compose up -d
```

- Web App: `http://localhost:5041`
- API: `http://localhost:5152`
- Database: PostgreSQL on port 5433

### Option 2: Local Development

**Install dependencies:**
```bash
cd RRHH.WEB
dotnet restore
cd ../TalentoPlus.API
dotnet restore
```

**Apply migrations:**
```bash
dotnet ef database update
```

**Run Web Application:**
```bash
cd RRHH.WEB
dotnet run
```
Access at: `http://localhost:5041`

**Run API:**
```bash
cd TalentoPlus.API
dotnet run
```
Access at: `http://localhost:5152` (with Swagger UI)

### Option 3: Visual Studio

1. Open `TalentoPlus.sln`
2. Set multiple startup projects (RRHH.WEB + TalentoPlus.API)
3. Press `F5`

## ✨ Features

### Web Application
- Employee management (CRUD)
- Admin dashboard
- Excel import functionality
- Authentication with ASP.NET Identity

### API
- RESTful endpoints for employee data
- Swagger documentation
- Identity-based authentication

## 🗄️ Database

PostgreSQL with Entity Framework Core migrations

**Default Connection (Docker):**
- Host: localhost:5433
- Database: talento_plus_db
- User: postgres
- Password: Qwe.123*

## 📚 Employee Model

- Position
- Salary
- Hire Date
- Education Level
- Professional Profile
- Department
- Active Status

## 🐛 Troubleshooting

**Port conflicts:**
```bash
# Modify port mappings in docker-compose.yaml
# Or kill process: lsof -i :5041
```

**Database issues:**
```bash
# Check container logs
docker-compose logs postgres-db
```

---

**Version**: 1.0.0