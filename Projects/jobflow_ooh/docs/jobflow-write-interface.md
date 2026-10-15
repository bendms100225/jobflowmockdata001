# JobFlow write interface

`IJobFlowJobWriter` is the only type allowed to create a job. It accepts a validated `CreateJobCommand` and returns `JobWriteResult`.

```csharp
Task<JobWriteResult> CreateAsync(CreateJobCommand command, CancellationToken cancellationToken);
```

`JobCreationService` runs `CreateJobCommandValidator` first. The command carries the portal fields: client code, site code, description, order number, reported by, site contact, received time, response category code, requested engineer employee id (`dbo.employee.idno`), section, subsection, and an idempotency key. It does not include a JobFlow job number. The caller does not choose one.

The experimental proof of concept maps some of those fields onto `orders`, `selectsection`, and `userlog`. That map is documented in `docs/poc-field-mapping.md`. It is not a traced JobFlow workflow. `pmanager`, `pcodinator`, and `profitper` stay unset. The selected engineer is not written to `username` or `calltakenby`.

## JobWriteResult

| Property | Meaning |
|---|---|
| `Reference` | Value to show as the job reference |
| `IsSimulated` | `true` when JobFlow was not called |
| `DisplayText` | Text the UI shows. Simulated results say they were not created in JobFlow. Experimental results say the JobFlowDemo record is not a production integration. |
| `Receipt` | Set for an experimental JobFlowDemo create. Null for the disabled and simulated writers. |

## DisabledJobFlowJobWriter

Default registration. Constructor takes a logger only. `CreateAsync` logs a rejection and throws `JobFlowWriteNotConfiguredException`. It cannot open a connection.

## SimulatedJobFlowJobWriter

Registered only for Development plus `JobFlowWrites:Mode=Simulated`. It stores references in memory, keyed by `IdempotencyKey`, so a repeat of the same key returns the same reference. The shape is `SIM-yyyyMMdd-000001`, using `Europe/London` unless `JobFlow:DatabaseTimeZone` says otherwise. Each result has `IsSimulated` true. The log line includes the words `Simulated job reference` and states that the job was not created in JobFlow.

## ExperimentalDirectSqlJobFlowJobWriter

Registered only when all of these are true:

- `ASPNETCORE_ENVIRONMENT` is `Development`
- `JobFlowWrites:Mode` is `ExperimentalDirectSql`
- `JobFlowWrites:AllowDevelopmentDatabaseMutation` is true
- `ConnectionStrings:JobFlowExperimental` names the database `JobFlowDemo` and is not read-only

Any other database name is refused before a connection is opened. The connected `DB_NAME()` is checked again before the transaction starts. Outside Development, registration and `CreateAsync` throw. This writer is not production-ready and is not a supplier-supported integration.

It inserts `dbo.orders`, `dbo.selectsection`, and an `ORDER` row in `dbo.userlog` in one transaction, then reads them back and commits only if the read-back matches. It does not write `dbo.status`, `dbo.jobplan`, or `dbo.allocation`. Idempotency lives in the separate `JobFlowOohPortal` database.

## Not a traced workflow

The experimental writer is a minimum proof of concept for the standalone `JobFlowDemo` database. It does not replace a trace of the hosted JobFlow client. Do not point it at any other database.
