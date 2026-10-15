# JobFlow integration map

Database: `JobFlowDemo`, restored from the 13 Oct 2025 full backup taken on `WEBNAN\SQLEXPRESS`. Investigation was read-only after the restore. No job was inserted.

The prototype's JobFlow function names (`getClients`, `createJob`, `allocateEngineer`, and the rest) are not database objects. This backup has no views, stored procedures, functions, triggers, or other SQL modules. JobFlow's behaviour lives in the application, which inserts into tables.

Confidence values:

| Level | Meaning |
|---|---|
| High | Foreign key or measured data agrees with the column. |
| Medium | The column or table fits, and the data does not contradict it, but a label or write rule is not stored in the database. |
| Low | Name similarity only. |
| UNRESOLVED | Not proven. Do not implement this mapping yet. |

## How a job is stored

A job is a row in `dbo.orders`. The job number is `orders.jobno` (`int`, identity, primary key). Current identity value is 15917, which is also the maximum `jobno` in the table.

`orders.clientcode` is a foreign key to `client.clientcode`. `orders.sitecode` is a foreign key to `houselist.sitecode`. In this backup every job's site belongs to that job's client (0 mismatches, 0 orphans).

There is no procedure that creates a job. Coverage of child rows, measured across all 15,792 jobs:

| Table | Jobs present | What that shows |
|---|---|---|
| `selectsection` | 15,792 (all) | Every job has a subsection row. 15,756 have exactly one. |
| `userlog` with `formname = 'ORDER'` | 15,773 | One create-audit row each. `userlog.luser` equals `orders.username` on all 15,773. |
| `status` | 14,731 | One status row per job that has one (`status.jobno` is the primary key). Not every job. No foreign key. |
| `jobplan` | 13,558 | Diary rows. Many jobs have more than one. Not every job. |
| `allocation` | 13,612 | Visit rows. Many jobs have more than one. Not every job. |
| `anidlog` | 11,120 | Linked to `jobplan.jpkey` on 38,480 of 39,933 log rows. No foreign key. |
| `emaildone` | 1,500 | Sparse. |
| `engcale` | 124 | Not the main diary. |

So the desktop application writes `orders`, and in practice also writes `selectsection` and a `userlog` row labelled `ORDER`. It often writes `status`, `jobplan` and `allocation` as well. This backup cannot show the column list or the order of those inserts. That write set is UNRESOLVED until the JobFlow application is traced or its source is inspected.

`userlog.formname` is free text written by the application. The frequent values include `ORDER`, `EDIT ORDER`, `UPDATE-INFO(Job Allocation)`, `Add - (Job Allocation) {person name}`, `JOB-COMPLETE` and `JOB-DONE`. Allocation is a separate application action from creating the order. The person name is embedded in the audit text. It is not a foreign key.

## Proven integrations

### Clients

- Portal concept: client list and client search (`getClients`)
- Database object: `dbo.client`
- Important columns: `clientcode` (PK, `varchar(50)`), `client` (name, `varchar(100)`), `town`, `postcode`, `tel`, `cemail`
- Relationship: `orders.clientcode` → `client.clientcode`
- Operation: read
- Confidence: High for identity and name
- Evidence: 656 client rows. All 15,792 jobs reference a real `clientcode`. The name column is `client`, not `name`.

### Sites

- Portal concept: site on a client (`getClientSites`, site search)
- Database object: `dbo.houselist`
- Important columns: `sitecode` (PK, `varchar(50)`), `clientcode`, `house` (site name, `varchar(100)`), `address1`, `town`, `postcode`, `tel`
- Relationship: `orders.sitecode` → `houselist.sitecode`. `houselist.clientcode` matches `client.clientcode` in the data and is **not** a foreign key.
- Operation: read
- Confidence: High
- Evidence: 1,003 sites, 643 clients have at least one, maximum 56 sites on one client, average 1. 0 sites with an unknown client. 0 jobs with an unknown site. The prototype's "thousands of properties" case is not this backup.

### Client and site must match

- Portal concept: choosing a site also chooses its client
- Database object: `orders.clientcode` and `orders.sitecode`, checked against `houselist.clientcode`
- Important columns: those three
- Relationship: both order columns are foreign keys; the site-to-client link is data, not a constraint
- Operation: read when searching; write both columns on the job
- Confidence: High that they are stored separately and must agree
- Evidence: 0 jobs where `houselist.clientcode` differs from `orders.clientcode`.

### Job number

- Portal concept: the reference shown after create (`JF-248317` in the prototype)
- Database object: `dbo.orders.jobno`
- Important columns: `jobno int` identity, not null, clustered primary key. Seed property is 30 increment 1. `IDENT_CURRENT` is 15917.
- Relationship: parent key for the child tables listed above
- Operation: written by the identity. Read it back. Do not invent the number in the portal.
- Confidence: High that the number is `jobno`. UNRESOLVED for a `JF-` prefix.
- Evidence: `jobno` is the only job key. Values in this backup are plain integers from 12 to 15917. No column stores a `JF-` prefix. The prototype format is mock data.

### Job description

- Portal concept: job description, including fault, risks and access
- Database object: `dbo.orders.description`
- Important columns: `description text`, nullable. In this backup it is populated on all 15,792 jobs.
- Relationship: column on the job row
- Operation: write
- Confidence: High
- Evidence: column name and 100% population. The type is legacy `text`, not `varchar`.

### Customer order / PO

- Portal concept: Order / PO number (`orderRef`)
- Database object: `dbo.orders.orderno`
- Important columns: `orderno varchar(30)`, nullable. Also `orderno2` through `orderno6` (`varchar(30)`) and `orderref varchar(200)`.
- Relationship: column on the job row. No purchase-order table is linked from `orders`.
- Operation: write `orderno`
- Confidence: High for `orderno`. The other order columns are not the main PO.
- Evidence: `orderno` is set on 15,753 of 15,792 jobs. `orderno2` through `orderno6` are set on 0. `orderref` is set on 24. `wono`, `joref` and `faultref` are set on 0.

### Call received, and the deadline

- Portal concept: received time and "on site by"
- Database object: `dbo.orders.date` and `dbo.orders.deadline`, with hours in `dbo.category`
- Important columns: `orders.date datetime`, `orders.deadline datetime`, `orders.categorycode int`, `category.categorycode`, `category.category int`
- Relationship: `orders.categorycode` is **not** a foreign key. It matches `category.categorycode` for codes 1–5.
- Operation: read `category`; write `orders.date`, `orders.categorycode`, `orders.deadline`
- Confidence: High
- Evidence: for jobs whose `categorycode` is in `category`, the average hours from `orders.date` to `orders.deadline` equals `category.category`:

| categorycode | category (hours) | Jobs | Average hours from `date` to `deadline` |
|---|---|---|---|
| 1 | 2 | 84 | 2.00 |
| 2 | 4 | 563 | 4.00 |
| 3 | 24 | 5,948 | 24.03 |
| 4 | 72 | 1,179 | 72.00 |
| 5 | 168 | 2,014 | 168.00 |

`category.categoryname` is null on all 5 rows. The hour count is the integer column `category`, not the name. `deadline` is populated on every job. `date` is populated on every job (minimum 2000-01-01, maximum 2025-09-26).

The prototype's mock map (code `1` = 4 hours, code `2` = 8 hours, codes `6`–`10` present) does **not** match this table. This database has no 8-hour category and no codes above 5. 6,004 jobs have `categorycode = 0`, which is not a row in `category`.

### Subsection on the job

- Portal concept: server-side subsection `OOH`
- Database object: `dbo.selectsection`, `dbo.subsection`, `dbo.section`
- Important columns: `selectsection.sskey` identity PK, `selectsection.jobno`, `selectsection.sscode`, `subsection.sscode` PK, `subsection.scode`, `subsection.ssname`, `section.scode` PK, `section.sname`
- Relationship: `selectsection.jobno` → `orders.jobno`. `selectsection.sscode` → `subsection.sscode`. `subsection.scode` matches `section.scode` in the data except subsection `18`, whose `scode` is blank. That section link is not a foreign key.
- Operation: read the lookups; write one `selectsection` row for a new job
- Confidence: High that this is how a job gets a subsection. UNRESOLVED for the value `OOH`, because that section and subsection do not exist.
- Evidence: every job has a `selectsection` row, and 0 of those `sscode` values are missing from `subsection`. Sections in this backup are trade names (Electrical, Plumbing, Heating, General Maintenance, and others). None are named OOH. Subsection `16` / section `GM` is "General Maintenance" (2,785 jobs). That is not a proven substitute for OOH.

### People (engineers and staff)

- Portal concept: engineer record
- Database object: `dbo.employee`
- Important columns: `idno` identity PK, `name varchar(100)`, `phone`, `tele`, `email`, `trade varchar(100)`, `ce`, `contractor`, `jobapp`
- Relationship: `allocation.idno`, `jobplan.idno`, `operative.idno` and `ooheng.idno` all reference `employee.idno`
- Operation: read
- Confidence: High that the person key is `employee.idno`
- Evidence: 51 employees. 50 of them appear in `operative`. Foreign keys from diary and allocation land here, not on `loginuser`.

### Engineer assigned to a subsection

- Portal concept: "engineers assigned to the OOH subsection" (`getOOHEngineers`)
- Database object: `dbo.operative`
- Important columns: `okey` identity PK, `idno`, `sscode`
- Relationship: `operative.idno` → `employee.idno`. `operative.sscode` → `subsection.sscode`.
- Operation: read
- Confidence: High that this is the assignment table. UNRESOLVED as the OOH list, because there is no OOH subsection and `ooheng` is empty.
- Evidence: 163 rows, 50 distinct people, spread across the existing trade subsections (Air Conditioning Commercial 18, Plumbing Commercial 18, Heating Commercial 16, Electrical Commercial 16, General Maintenance 15, and smaller counts). One employee can be on many subsections.

### OOH engineer list

- Portal concept: the set of engineers allowed to take OOH work
- Database object: `dbo.ooheng`
- Important columns: `okey` identity PK, `idno`
- Relationship: `ooheng.idno` → `employee.idno`. No subsection column.
- Operation: read, once it contains rows
- Confidence: Medium that the table is that list. It cannot be used today.
- Evidence: the name, the foreign key, and 0 rows. `IDENT_CURRENT` is 1.

### Diary

- Portal concept: `createDiaryEntry` from received time to deadline
- Database object: `dbo.jobplan`
- Important columns: `jpkey` identity PK, `jobno`, `idno`, `pptime` (planned start), `petime` (planned end), `cdate`, `info varchar(50)`, `oncall`, `vdone`, `prapp` (default 1)
- Relationship: `jobplan.jobno` → `orders.jobno`. `jobplan.idno` → `employee.idno`.
- Operation: likely write, but the times the prototype would write are not how existing rows look
- Confidence: High that `jobplan` is the diary. UNRESOLVED for "start = received, end = deadline".
- Evidence: 31,200 rows, every row has `jobno` and `idno`, `pptime` is never null. Only 210 plans have `petime` within 10 minutes of the job deadline. Only 2,504 have `pptime` within 10 minutes of `orders.date`. Jobs that have plans usually have several (6,901 have one; the rest have more, including a few jobs with hundreds). `oncall` is 0 on 5,102 rows and null on 26,098. `info` is null on 30,259 rows; the filled values are notes such as `OFF`, bank holiday and training, not a response code. `engcale` (124 rows) also has `idno`, `jobno`, `stime`, `etime` and is not the table used for most jobs.

### Visit / engineer-app attendance

- Portal concept: the engineer completes the job in the JobFlow app; the prototype does not write that
- Database object: `dbo.allocation`
- Important columns: `aekey` identity PK, `jobno`, `idno`, `arrivaltime`, `departuretime`, `comments`, `lat`, `long`, `endlat`, `endlong`, `workdone`, `oohhrs`, `jpkey varchar(50)`
- Relationship: `allocation.jobno` → `orders.jobno`. `allocation.idno` → `employee.idno`. `allocation.jpkey` is text, not a foreign key to `jobplan.jpkey`.
- Operation: read for understanding. Do not assume the portal's "allocate" step should insert this row.
- Confidence: High that these are attendance records. UNRESOLVED whether the desktop "Job Allocation" action inserts `allocation`, `jobplan`, or both.
- Evidence: 26,870 rows. 25,786 have both arrival and departure; 1,084 have neither. `workdone` is 0 on every row. `userlog` records allocation with a different form name from `ORDER`. Multiple allocation rows per job are normal (7,278 jobs have one; the rest of the 13,612 have more).

### App sync log tied to diary rows

- Portal concept: "diary/app entry"
- Database object: `dbo.anidlog`
- Important columns: `ankey` identity PK, `idno`, `jpkey`, `jobno`, `dldate`
- Relationship: no foreign keys. 38,480 of 39,933 `jpkey` values exist in `jobplan.jpkey`.
- Operation: unknown. Not a proven portal write.
- Confidence: Medium that it records something about a diary row reaching an app. The table name is not defined anywhere in the database.
- Evidence: the join above. 11,120 distinct jobs. No trigger maintains it.

### Audit trail

- Portal concept: job history
- Database object: `dbo.userlog`
- Important columns: `ulkey` identity PK, `jobno`, `formname varchar(250)`, `luser varchar(100)`, `ldate`, `cname`
- Relationship: no foreign key. Every non-null `jobno` in this backup exists in `orders`.
- Operation: the application writes it. A portal that only inserts `orders` would not reproduce the `ORDER` audit row.
- Confidence: High that this is the history log. UNRESOLVED which `formname` / `luser` a portal user must write.
- Evidence: 197,711 rows. 15,773 jobs have exactly one `formname = 'ORDER'` row, and on those `luser` equals `orders.username`. There is no trigger.

### Job status row

- Portal concept: none directly. Included because most jobs have one.
- Database object: `dbo.status`
- Important columns: `jobno` PK (not an identity, and not a foreign key), `date`, `jobcheck`, `jobmfm`, `jobc`, `info`, `whomv`
- Relationship: `status.jobno` values all exist in `orders`, with no constraint
- Operation: UNRESOLVED
- Confidence: Medium that the application inserts it for a normal job. `info` does not describe the job.
- Evidence: 14,731 rows, 0 orphans, `info` null on every row. 1,061 jobs have no `status` row.

### Who logged the job inside JobFlow

- Portal concept: logged-by engineer
- Database object: `dbo.orders.username` and `dbo.orders.calltakenby`
- Important columns: both `varchar(50)`. `dbo.loginuser.username` is the office login (`varchar(30)` PK).
- Relationship: no foreign key from `orders.username` to `loginuser.username`
- Operation: write, once the value is known
- Confidence: High that these two order columns store the JobFlow login name. UNRESOLVED for an Entra engineer who is not a `loginuser`.
- Evidence: on 15,789 jobs `calltakenby` equals `username`, and they differ on 0 jobs. 15,130 of those usernames still exist in `loginuser`. There are only 6 distinct values in `orders.username`. `calltakenby` matches `employee.name` on 0 jobs. `loginuser` has 4 rows (`utype` Manager or Admin). It is the desktop login table, not the engineer table.

## Mappings that must stay unresolved

### Reported by, and site contact

- Portal concept: two names, "person who called it in" and "who the engineer contacts on site"
- Candidates: `orders.contactname varchar(50)` (set on 15,788 jobs), `orders.rpby varchar(50)` (set on 4,914), `orders.cpofcontact varchar(200)` (set on 0)
- Relationship: columns on the job. No contact table.
- Operation: write, after the meaning is confirmed
- Confidence: UNRESOLVED
- Evidence: `contactname` is the only contact column filled on almost every job. Where `rpby` is filled, it equals `contactname` on 2,206 jobs and differs on 2,708. `rpby` equals `employee.name` on 1,343 jobs, so it is sometimes a staff name and sometimes not. The column name does not prove "reported by". `cpofcontact` is unused. Nothing in the database shows two caller names being stored.

### Section and subsection values `OOH`

- Portal concept: hidden defaults `section = OOH`, `subsection = OOH`, described as new JobFlow records
- Database object: `dbo.section`, `dbo.subsection`
- Important columns: `section.scode`, `section.sname`, `subsection.sscode`, `subsection.scode`, `subsection.ssname`
- Relationship: see "Subsection on the job"
- Operation: would be a read of codes that are not in this backup. Creating them would be a data change and was not done.
- Confidence: UNRESOLVED
- Evidence: 0 section or subsection rows whose code or name contains `OOH` or "out of hours". `sscode` values already in use are short numbers and trade codes (`1`, `2`, `12`, `16`, `E`, `P`, `GM`, …), not the word `OOH`.

### Job type `Reactive`

- Portal concept: hidden default `jobType = Reactive`
- Database object: `dbo.orders.jobtype int` (default 0), and `orders.orgjobtype int`
- Important columns: those integers. `dbo.product` is equipment (Air Conditioning, Boiler, …), not a job-type list. `dbo.worktype` has 0 rows. `dbo.contracttype` is Weekly and Quarterly. `JobDetails.Jobtype`, `JobFlowJobSummary.jobtype` and `JobOperativeDetails.JobType` are `varchar` reporting tables with 0 rows.
- Relationship: `orders.jobtype` is not a foreign key
- Operation: UNRESOLVED
- Confidence: UNRESOLVED
- Evidence: values in `orders.jobtype` are only 0 (10,167 jobs), 1 (5,611) and 2 (14). `jobtype` 1 contains 1,180 of the jobs flagged `ppmiv = 1`. `jobtype` 0 contains 1,083 of the jobs flagged `qonly = 1`. That is not a definition of Reactive, PPM or Quote. No row anywhere sampled stores the word Reactive.

### Project manager and project coordinator `OOH`

- Portal concept: hidden text defaults
- Database object: `dbo.orders.pmanager varchar(200)`, `dbo.orders.pcodinator varchar(200)`
- Relationship: no lookup table and no foreign key. The coordinator column is spelled `pcodinator`.
- Operation: UNRESOLVED
- Confidence: Medium that these are the right columns, from the names alone. UNRESOLVED for writing `OOH`.
- Evidence: 14,216 jobs are null and 1,576 are blank. That is every job. 0 jobs contain the text `OOH`. The columns have never been used in this backup.

### Target margin 30%

- Portal concept: `targetMarginPercent = 30`
- Database object: `dbo.orders.profitper decimal(10,2)` is the only profit/margin column on the job
- Relationship: column on the job. `jfpimage.lmargin` and `rmargin` are image-layout fields, not a job margin.
- Operation: UNRESOLVED
- Confidence: Low
- Evidence: `profitper` is null on all 15,792 jobs. Nothing shows that 30 is a stored default. The column name is not `targetMargin`.

### Entra user mapped to an engineer

- Portal concept: Entra object id → JobFlow engineer, after membership of "OOH Engineers"
- Database object: none
- Candidates inspected: `employee` (no object-id column; `email` set on 11 of 51 people, 11 distinct; `pemail` set on 0; `phone` set on 50), `loginuser` (office users, password column, `uemail`)
- Operation: UNRESOLVED
- Confidence: UNRESOLVED
- Evidence: no column name contains `entra`, object id, or Azure AD. Email cannot join most engineers. `loginuser` is not `employee`.

### Engineer discipline, phone, email on the picker

- Portal concept: discipline, phone, email
- Database object: `employee.trade`, `employee.phone` / `employee.tele`, `employee.email`
- Operation: read
- Confidence: Medium for phone and `trade`. Low for using `email` as the notification address.
- Evidence: `trade` exists. `phone` is set on 50 of 51 employees. `email` is set on 11. No column is named discipline. `operative` does not store a trade; the subsection name is the trade grouping.

### Email the allocated engineer

- Portal concept: `sendEngineerNotification`, JobFlow-native or portal-side, undecided
- Database object: `dbo.emaildone`
- Important columns: `ekey` identity, `cdate`, `jstime`, `idno`, `jobno`, `suser`
- Relationship: `emaildone.jobno` → `orders.jobno`. `emaildone.idno` → `employee.idno`.
- Operation: UNRESOLVED
- Confidence: Medium that the table records an email already sent. No send procedure exists.
- Evidence: 2,328 rows, 1,500 jobs. `dbo.engappmsg` has 0 rows. There is no trigger and no procedure. Most employees have no email address.

### Recent jobs and the duplicate warning

- Portal concept: portal-owned audit, not a JobFlow read
- Database object: none for a portal submission log
- Operation: not a JobFlow table
- Confidence: High that the prototype keeps this outside JobFlow. A JobFlow equivalent for "jobs I logged" is UNRESOLVED because `orders.username` is an office login, not the attending engineer.
- Evidence: no portal table in this database. Duplicate detection by site and time could be read from `orders.sitecode` and `orders.date`, but that is not specified by a view or procedure, and it would include every JobFlow job, not only portal jobs.

### Idempotency key

- Portal concept: one key per review, so a retry does not create two jobs
- Database object: none found
- Operation: UNRESOLVED
- Confidence: UNRESOLVED
- Evidence: no idempotency column. The only uniqueness on `orders` is `jobno`. There are no nonclustered unique indexes in the database.

### Client sector, PO-required flag, and "clients with OOH cover"

- Portal concept: sector on the client row, `poRequired`, and only clients who have OOH cover
- Database object: none of these flags exist on `dbo.client`
- Nearby objects: `dbo.clienttype` has 0 rows. `client.cd` is 1 on all 656 clients. `houselist.cd` is 1 on 999 sites and 0 on 4. `cd` is not documented.
- Operation: UNRESOLVED
- Confidence: UNRESOLVED
- Evidence: no sector column, no PO-required column, no OOH-cover column.

### Access details as their own field

- Portal concept: access is part of the description hint, not its own input
- Database object: `dbo.orders.access varchar(200)` exists and is empty on all 15,792 jobs
- Operation: do not invent a separate write
- Confidence: High that current jobs do not use `orders.access`
- Evidence: 0 non-blank values.

### `offsitecode` is not a site

- Portal concept: site (do not map this here)
- Database object: `dbo.orders.offsitecode` (default 0) and `dbo.offsite`
- Important columns: `offsite.offsitecode`, `offsite.value`
- Relationship: no foreign key
- Operation: not a portal site write
- Confidence: High that this is a completion state
- Evidence: `offsite` values are `0 Not Complete.`, `1 Complete.`, `2 Needs a return visit`, `3 Temp repair. Needs another trade.`, `4 Needs parts.`, `5 Needs quote.`, `7 Cancelled.`, `8 No Access.`

## Objects checked and not used for these screens

| Object | Why it is not the portal's source |
|---|---|
| `dbo.loginuser` | Four office logins. Not engineers. Holds a password column. |
| `dbo.product` | 110 equipment names. Not job types. |
| `dbo.fault` | 11 fault names, no foreign key from `orders`. |
| `dbo.category` | Response hours, already mapped. Not a trade and not a client sector. |
| `dbo.gencojobtracker` | 0 rows. Extra project-tracking fields. `jobno` does reference `orders`. Not used by the prototype. |
| `dbo.JobDetails`, `dbo.JobFlowJobSummary`, `dbo.JobOperativeDetails` | Empty heaps with no primary key. Reporting shapes, not the live job. |
| `dbo.jwatchjobs`, `dbo.engappmsg` | 0 rows. |
| `dbo.engcale` | 124 rows. Not the diary used by most jobs. |
