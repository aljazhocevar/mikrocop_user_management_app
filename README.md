# UserRepositoryService

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
