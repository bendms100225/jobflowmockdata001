# Local development

Requires the .NET 8 SDK and the restored `JobFlowDemo` database on `localhost\SQLEXPRESS`.

From the repository root:

```powershell
dotnet build
dotnet test
```

`appsettings.json` leaves both connection strings empty and sets `JobFlowWrites:Mode` to `Disabled`. `appsettings.Development.json` points `JobFlowReadOnly` at the local demo database with Windows authentication and `ApplicationIntent=ReadOnly`. That string has no password. A SQL login, if you use one later, goes in user-secrets and should be a reader:

```powershell
dotnet user-secrets --project src/JobFlowOOH.Web set "ConnectionStrings:JobFlowReadOnly" "Server=localhost\SQLEXPRESS;Database=JobFlowDemo;User ID=...;Password=...;TrustServerCertificate=True;ApplicationIntent=ReadOnly"
dotnet user-secrets --project src/JobFlowOOH.Web set "AzureAd:TenantId" "<tenant-id>"
dotnet user-secrets --project src/JobFlowOOH.Web set "AzureAd:ClientId" "<client-id>"
dotnet user-secrets --project src/JobFlowOOH.Web set "AzureAd:ClientSecret" "<client-secret>"
```

Do not put `PortalDatabase` on `JobFlowDemo`. The host refuses that pair. Development settings point `PortalDatabase` at `JobFlowOohPortal` on the same SQL Server instance. The portal initializer creates that database and its submission table. It does not migrate `JobFlowDemo`.

`appsettings.Development.json` enables `ExperimentalDirectSql` for this disposable database. Before starting the app, set the two login values from existing `dbo.loginuser.username` rows. Do not invent a login, and do not commit the values:

```powershell
dotnet user-secrets --project src/JobFlowOOH.Web set "JobFlowWrites:DevelopmentUsername" "<existing loginuser.username>"
dotnet user-secrets --project src/JobFlowOOH.Web set "JobFlowWrites:DevelopmentCallTakenBy" "<existing loginuser.username>"
```

Run with `ASPNETCORE_ENVIRONMENT=Development`. The proof-of-concept screens are `/jobs/new`, `/jobs/created/{jobNumber}`, and `/jobs/recent`. `GET /health` remains. Entra middleware is registered only when `AzureAd:ClientId` is set. A simulated mode is still available by setting `JobFlowWrites:Mode` to `Simulated`; outside Development that mode registers `DisabledJobFlowJobWriter`. Experimental mode outside Development refuses to start.

Integration tests read the development settings and user-secrets. The write tests insert `POC-OOH-TEST-` rows and do not delete them. Take the backup in `docs/poc-database-restore.md` before running them. Restoring that backup is the preferred way to reset `JobFlowDemo`.
