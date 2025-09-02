# UserRepositoryService

<<<<<<< HEAD
This archive contains the full ASP.NET Core Web API project (C#) configured to use **MSSQL** by default.

## Quick start (MSSQL)
1. Update `src/UserRepositoryService/appsettings.json` connection string with your SQL Server credentials.
2. Run the SQL script `sql/create_tables.sql` against your DB (or let EF Core create the DB if you prefer migrations).
3. Restore and run the project:
```bash
dotnet restore
dotnet run --project src/UserRepositoryService
```
4. Run tests:
```bash
dotnet test
```

## Notes
- API key is required in header `X-Api-Key` for every request.
- Logs are written into `logs/log-<date>.txt` using Serilog rolling files.
=======
**Description:**  
ASP.NET Core Web API for managing users and their passwords. Supports create, read, update, delete operations, password validation, and API key authentication. Logs every request to daily log files.

---

## Features

- Add new user  
- Update existing user  
- Delete user  
- Retrieve user data  
- Validate user password  
- API key authentication  
- Request logging to daily log files  
- Swagger / OpenAPI documentation

---

## Requirements

- Windows 10/11  
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- MSSQL Server 
- PowerShell or CMD for commands

---
>>>>>>> f1ac6a697f92ee8e89fa574399b62cd469700bfb
