# Development safety boundary

The portal foundation may read `JobFlowDemo`. It must not change it.

## Database

| Name | Value |
|---|---|
| Server | `localhost\SQLEXPRESS` |
| Database | `JobFlowDemo` |
| Connection | `ConnectionStrings:JobFlowReadOnly` |
| Intent | `ApplicationIntent=ReadOnly` |

`JobFlowDemo` is the restored development copy. The hosted JobFlow RemoteApp on `JOBFLOW-RD01.AD.JOBFLOW.CO.UK` is a different system. Do not repoint it, and do not trace against it.

The portal's own database, when one exists, uses `ConnectionStrings:PortalDatabase`. Startup rejects a portal connection that names the same server and database as the JobFlow connection. No migration runs against `JobFlowDemo`.

## Writes

The hosted JobFlow system is still out of bounds. The standalone `JobFlowDemo` database is disposable, so the proof of concept may create marked rows there. That permission is not a production write path.

`IJobFlowJobWriter` is the only job-create port. The default implementation is `DisabledJobFlowJobWriter`. It throws `JobFlowWriteNotConfiguredException` and has no database connection.

`SimulatedJobFlowJobWriter` is registered only when both of these are true:

- `ASPNETCORE_ENVIRONMENT` is `Development`
- `JobFlowWrites:Mode` is `Simulated`

That writer keeps its reference in memory. The reference looks like `SIM-20261015-000001`. `JobWriteResult.IsSimulated` is true, and `DisplayText` says the job was not created in JobFlow. The same text is written to the log. It does not open a SQL connection.

`ExperimentalDirectSqlJobFlowJobWriter` is registered only for Development, `JobFlowWrites:Mode=ExperimentalDirectSql`, `AllowDevelopmentDatabaseMutation=true`, and a connection whose database name is exactly `JobFlowDemo`. It inserts `orders`, `selectsection`, and an `ORDER` `userlog` row. It does not update or delete existing JobFlow rows. See `docs/poc-database-write-plan.md`.

A unit test reads every `.cs` file under `src/JobFlowOOH.JobFlow`, ignores comments, and fails if it finds `UPDATE`, `DELETE`, `MERGE`, `TRUNCATE`, `ALTER`, `DROP`, `CREATE TABLE`, `EXEC`, `EXECUTE`, or `SELECT INTO`. `INSERT` is allowed only under `Writing/Experimental`. The test also fails if those files name the password or national-insurance columns. Comments are the only exclusion, and they are removed before the scan.

`SelectOnlySql` rejects anything that is not one `SELECT`, including a second statement or `SELECT INTO`, before Dapper runs it.

## Reads

Readers select reference data that the earlier investigation proved:

- `dbo.client`
- `dbo.houselist`
- `dbo.employee` with `dbo.operative`
- `dbo.ooheng`
- `dbo.category`
- `dbo.section` and `dbo.subsection`

They do not filter to "OOH clients" or an "OOH subsection". Those rules are not in this database. `ooheng` is read as it stands, which on this backup is empty.

## What this foundation does not do

- No job screen
- No row in `orders`, `selectsection`, `userlog`, `status`, `jobplan`, or `allocation`
- No schema change
- No hosted JobFlow connection
