# UserRepositoryService

This archive contains the full ASP.NET Core Web API project (C#) configured to use **MSSQL** by default.

## Quick app start
1. Update `src/UserRepositoryService/appsettings.json` connection string with your SQL Server credentials.
2. Run the SQL script `sql/create_tables.sql` against your DB (or let EF Core create the DB if you prefer migrations).
3. Restore and run the project:
   navigate to: src/UserRepositoryService
```bash
dotnet restore
dotnet run 
```
Server will start on localhost (http://localhost:5000/)

4. Run tests:
  navigate to tests/UserRepositoryService.Tests
  run:
```bash
dotnet test
```

## Usage and examples

You can use this program with executing curl commands or using Postman software. I have also prepared python scripts which are located in python_requests_examples. 

1. Examples of using program with use of curl commands: 

2. Running python scripts. (**You will need to have installed python framework on your PX**)
 ```bash
python .\add_user.py
python .\delete_user.py
python .\edit_user.py
python .\get_user.py
python .\validate_password.py
```
You will get response printed in terminal for both methods.

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
