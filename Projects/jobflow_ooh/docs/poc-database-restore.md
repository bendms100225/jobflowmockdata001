# POC database restore

Backup taken before any experimental write to `JobFlowDemo`.

| Item | Value |
|---|---|
| Server | `localhost\SQLEXPRESS` |
| Database | `JobFlowDemo` |
| Backup file | `C:\Users\Public\JobFlowDemo\poc-backup\JobFlowDemo_pre_poc_20261015.bak` |
| Size | 716,873,216 bytes |
| Taken | 15 October 2026, 13:56 local |
| Verification | `RESTORE VERIFYONLY` reported `The backup set on file 1 is valid.` |

`BACKUP DATABASE` processed 87,481 pages. The backup is outside the application source tree.

## Baseline counts

Counted immediately before the backup, and before any proof-of-concept insert:

| Table | Rows |
|---|---|
| `dbo.orders` | 15792 |
| `dbo.selectsection` | 15828 |
| `dbo.userlog` | 197711 |
| `dbo.status` | 14731 |
| `dbo.jobplan` | 31200 |
| `dbo.allocation` | 26870 |

`status`, `jobplan`, and `allocation` must stay at these counts. The POC does not write them. `orders`, `selectsection`, and `userlog` increase by one row for each committed POC job. Rolled-back attempts do not keep a row, but they can consume an identity value.

## Restore

Restoring this backup is the preferred way to remove every POC row. Stop the portal first. This command replaces the current `JobFlowDemo` database:

```sql
USE master;
ALTER DATABASE [JobFlowDemo] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [JobFlowDemo]
FROM DISK = N'C:\Users\Public\JobFlowDemo\poc-backup\JobFlowDemo_pre_poc_20261015.bak'
WITH REPLACE, RECOVERY, STATS = 10,
    MOVE N'JobFlowDemo' TO N'C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\DATA\JobFlowDemo.mdf',
    MOVE N'JobFlowDemo_log' TO N'C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\DATA\JobFlowDemo_log.ldf';
ALTER DATABASE [JobFlowDemo] SET MULTI_USER;
```

The portal database `JobFlowOohPortal` is not in this backup. Drop it separately if the submission keys should be cleared too.

A single marked job can instead be removed with `scripts/poc-cleanup-job.sql`. That script is not a substitute for this restore when the database should return to the baseline above.
