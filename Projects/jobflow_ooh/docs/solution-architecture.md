# Solution architecture

.NET 8 solution. Razor Pages is the web host. The job screens are not built yet.

## Projects

| Project | Responsibility |
|---|---|
| `JobFlowOOH.Domain` | Client, site, engineer, response category, section, and subsection records. No SQL, ASP.NET Core, or Entra packages. |
| `JobFlowOOH.Application` | Reader and writer interfaces, `CreateJobCommand`, validation, and `JobCreationService`. |
| `JobFlowOOH.JobFlow` | Dapper `SELECT` readers and the disabled/simulated writers. |
| `JobFlowOOH.Infrastructure` | Portal connection options and the guard that keeps that connection off `JobFlowDemo`. |
| `JobFlowOOH.Web` | Composition root. Entra registration, health check, no portal screens. |
| `JobFlowOOH.UnitTests` | Writer, validation, registration, and SQL-keyword checks. |
| `JobFlowOOH.IntegrationTests` | Read-only queries against the local `JobFlowDemo` connection. |

References:

- Web references Application, JobFlow, and Infrastructure so it can register implementations.
- Infrastructure and JobFlow reference Application.
- Application references Domain.
- Domain references none of the other projects.

## Request path

`IJobCreationService` validates `CreateJobCommand` and then calls `IJobFlowJobWriter`. Validation failures throw `JobCommandValidationException` and do not call the writer.

The writer is chosen in `JobFlowWriterSelector`:

| Environment | `JobFlowWrites:Mode` | Writer |
|---|---|---|
| Development | `Simulated` | `SimulatedJobFlowJobWriter` |
| Anything else | Any value, including `Simulated` or `Direct` | `DisabledJobFlowJobWriter` |

Readers are registered in every environment. They use `ConnectionStrings:JobFlowReadOnly` and set `ApplicationIntent=ReadOnly` on the connection.

## Reference data the readers return

| Reader | Tables | Notes |
|---|---|---|
| `IJobFlowClientReader` | `client` | Code, name (`client`), town, postcode. No sector and no OOH flag. |
| `IJobFlowSiteReader` | `houselist` | Search is limited by `JobFlow:SiteSearchLimit` (default 25). |
| `IJobFlowEngineerReader` | `employee`, `operative`, `ooheng` | The selector uses `employee.idno` and `employee.name` for rows that also appear in `operative`. It does not require an OOH subsection. `ooheng` is read only for the separate OOH-id list and is empty. |
| `IJobFlowResponseCategoryReader` | `category` | `category.category` is exposed as hours. The name column is often null. |
| `IJobFlowSectionReader` | `section`, `subsection` | Returned as stored. No OOH row is invented. |

## Configuration

```json
"ConnectionStrings": {
  "JobFlowReadOnly": "",
  "PortalDatabase": ""
},
"JobFlowWrites": { "Mode": "Disabled" },
"JobFlow": {
  "DatabaseTimeZone": "Europe/London",
  "SiteSearchLimit": 25
}
```

Entra is configured with `Microsoft.Identity.Web` from the `AzureAd` section. Real tenant and client values belong in user-secrets, not in source.

The simulated reference date uses `JobFlow:DatabaseTimeZone`.
