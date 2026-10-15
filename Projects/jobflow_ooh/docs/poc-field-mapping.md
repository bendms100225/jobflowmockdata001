# POC field mapping

These mappings are for the experimental writer only. They are not a traced JobFlow create-job workflow, and they are not supplier-supported. Columns that are not listed are left to null or to the database default. `pmanager`, `pcodinator`, and `profitper` are not set.

## Proven from the schema investigation

| Portal field | JobFlow column | Why this value |
|---|---|---|
| Client | `orders.clientcode` | Foreign key to `dbo.client.clientcode`. The writer checks the client exists. |
| Site | `orders.sitecode` | Foreign key to `dbo.houselist.sitecode`. The writer also checks `houselist.clientcode` equals the selected client. That column is not itself a foreign key, but every existing site matched its client. |
| PO / reference | `orders.orderno` | `varchar(30)`. Populated on 15,753 of 15,792 jobs. POC values start with `POC-`. |
| Description | `orders.description` | `text`. Populated on every existing job. POC values start with `POC-OOH-`. |
| Category | `orders.categorycode` | Loaded from `dbo.category`. The hour count is `category.category` (code 1 = 2, 2 = 4, 3 = 24, 4 = 72, 5 = 168). `categoryname` is null on all five rows. The writer does not hard-code hours. |
| Received time | `orders.date` | Agreed POC mapping. For jobs whose category exists, the average hours from `orders.date` to `orders.deadline` equals `category.category`. `orders.date` is the SLA clock start. It is stored as `datetime` in Europe/London, trimmed to the second. |
| Deadline | `orders.deadline` | `orders.date` plus the category's hour count, using London local clock arithmetic, stored as `datetime`. |
| Section / subsection | `selectsection.sscode` | `selectsection.jobno` references the new order. `selectsection.sscode` references `subsection.sscode`. The writer requires `subsection.scode` to equal the selected `section.scode`. OOH rows do not exist and are not created. Development defaults are existing codes `GM` and `16`. |
| Job number | `orders.jobno` | `int` identity, seed 30, increment 1. The writer does not supply it. `OUTPUT INSERTED.jobno` returns it. |
| Order audit | `userlog` | `formname` is `ORDER` on 15,773 rows. `luser` equals `orders.username` on all of those rows. `ldate` equals `orders.date` on none of them, so the POC stores the London insert time, not the received time. |

`orders.username` and `orders.calltakenby` are set from `JobFlowWrites:DevelopmentUsername` and `DevelopmentCallTakenBy`. Both must already exist in `dbo.loginuser`. On 15,789 jobs those two order columns are equal, and they match office logins rather than `employee.name`. The selected engineer is not written to either column.

`orders.jobtype` is set from `JobFlowWrites:DevelopmentJobType` only after a check that the same integer already occurs on `dbo.orders`. This database contains 0 (10,167 jobs), 1 (5,611), and 2 (14). The column default is 0. The business meaning of 0, 1, and 2 is unresolved. The screen shows the stored number under development diagnostics and does not call it Reactive.

## Temporary contact mapping

These two assignments are a proof-of-concept convenience. The investigation did not prove which JobFlow screen field they are.

| Portal field | JobFlow column | Limit |
|---|---|---|
| Site contact | `orders.contactname` | `varchar(50)`. Populated on 15,788 jobs. |
| Reported by | `orders.rpby` | `varchar(50)`. Populated on 4,914 jobs and often different from `contactname`. |

The result page labels both as a temporary mapping.

## Requested engineer

Inspected on the standalone `JobFlowDemo` database. The selectable value is `dbo.employee.idno` (`int`, identity, primary key). There is no separate employee number: `payrollno` is empty on every current row. There is no first-name or surname column in use: `name` (`varchar(100)`) is populated, and `sname` and `nameini` are empty on every current row. `empsur` is an integer, not a surname.

No column is a proven active or inactive flag. `engstatic` is 0 on every row. `tredate` is null on every row. `ce` and `contractor` contain both 0 and 1, including people who are clearly still listed, so neither is used as a filter. `employee.trade`, `egroup`, `ecategory`, and `eskill` are empty on every current row, so the screen does not invent a discipline. `phone` is the telephone column. `tele` is empty. `email` is sparse and is not used as the identifier.

`dbo.operative.idno` references `dbo.employee.idno`. One employee who has no operative row is excluded from the selector. That row is `idno` 1, `name` Appointment. Everyone else with a name and an operative row is selectable. The list is not filtered by subsection, and `dbo.ooheng` is not used, because OOH eligibility is not configured. The option text is `employee.name`. The posted value is `employee.idno`. Duplicate names exist, so the id is what distinguishes them. The id is not written to `orders.username` or `orders.calltakenby`.

## Explicitly not mapped

| Portal field | Decision |
|---|---|
| Requested engineer | Read from `dbo.employee.idno`. Not written to `allocation`, `jobplan`, `orders.username`, or `orders.calltakenby`. Stored as `RequestedEngineerId` on `JobFlowOohPortal.dbo.PocIdempotency`. That column is the employee id and is not a foreign key. The screen says `Requested engineer`, then `Requested engineer, not yet assigned in JobFlow`. |
| Idempotency key | Portal table only. No JobFlow column was added. |
| Correlation id | Portal table and the log. Not written to JobFlow. |
| Project manager, coordinator, margin | Left null. The prototype values OOH, OOH, and 30 are not written. |
| `userlog.cname` | Null. It is populated on every historical `ORDER` row (17 distinct values) and it never equals the client name. The meaning is unresolved, so the POC does not invent it. |
| `status`, `jobplan`, `allocation` | Not written by the POC. Job 15940 later received one untimed `allocation` from the JobFlow client and still has no `jobplan`. That pair of facts is not an approved insert. See `docs/job-15940-engineer-trace.md`. |
