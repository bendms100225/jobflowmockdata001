# Job 15940 engineer trace

Read-only inspection of `localhost\SQLEXPRESS` / `JobFlowDemo` on 15 Oct 2026, after 19:12. No row was inserted, updated, or deleted by this investigation. Application code was not changed.

## Classification

Job 15940 is associated with Benoperative through `dbo.allocation` only. That is classification **B** for this job.

The same desktop log name on the hosted JobFlow client also creates `dbo.jobplan`. Those comparison jobs mean the reusable write is classification **F**: the action name is shared, and the tables written are not the same. There is not enough evidence to implement an experimental POC assignment.

## Benoperative

One `dbo.employee` row matches the name exactly.

| Column | Value |
|---|---|
| `idno` | 59 |
| `name` | Benoperative |
| `phone` | `0` |
| `tele` | null |
| `email` | null |
| `trade` | null |
| `ce` | 1 |
| `contractor` | 0 |
| `jobapp` | 0 |
| `engstatic` | 0 |
| `tredate` | null |
| `payrollno` | null |
| `egroup`, `ecategory`, `eskill`, `sname`, `nameini` | null |
| `empsur` | 0 |
| `region`, `ecompany` | null |

No other employee has this name. `engstatic` is 0 and `tredate` is null, which is the same shape as the other employees. Those columns were not used as an active/inactive filter.

`dbo.operative` has one row for this person. The table has no status column. Its columns are the operative identity, the employee, and the subsection.

| `operative.okey` | `operative.idno` | `operative.sscode` | Subsection | Section |
|---|---|---|---|---|
| 171 | 59 | 2 | Commercial | Air Conditioning (`AC`) |

`operative.idno` is the foreign key to `employee.idno`. `operative.okey` is the operative-row identity. Job 15940 does not store `okey` 171.

Benoperative has no rows in `dbo.jobplan`, `dbo.engcale`, or `dbo.ooheng`. The only JobFlow job link is the allocation below.

## Job 15940 order

`orders.jobno` 15940 is the internal job. `orders.orderno` `POC-bp02` is the customer reference. They are different values.

| Field | Value |
|---|---|
| `jobno` | 15940 |
| `date` | 2026-10-15 17:32:00 |
| `clientcode` | A120 |
| `sitecode` | A120-1 |
| `contactname` | ben psitecontact |
| `description` | POC-OOH-bp02 desc |
| `categorycode` | 2 |
| `deadline` | 2026-10-15 21:32:00 |
| `orderno` | POC-bp02 |
| `calltakenby` | administrator |
| `username` | administrator |
| `rpby` | ben |
| `jobtype` | 0 |
| `jlock` | 1 |
| `juser` | administrator |
| `jlono` | 0 |
| `fvisit`, `jcdate`, `etadate`, `cstatus` | null |
| `jstatustwono` | 0 |
| `mvdone`, `qdone`, `partsin`, `fsttimefix`, `archivedjob` | 0 |

`rpby` is the text `ben`. It does not equal `Benoperative` and it is not `employee.idno`. `jlono` is 0. `username` and `calltakenby` are the office login. None of these order columns is the engineer link.

The POC insert writes `date`, client, site, contact, description, category, deadline, order number, call taken by, username, reported by, and job type. It does not write `jlock` or `juser`. Those two columns are therefore later values. `juser` is still the office login, not the engineer.

The portal database `JobFlowOohPortal.dbo.PocIdempotency` has `JobNumber` 15940, `RequestedEngineerId` 59, `Status` Completed. That portal column is not a foreign key into JobFlowDemo.

## Every JobFlowDemo reference to job 15940

Every `jobno` column, plus `orders.pjobno`, `orders.ivjobno`, `orders.ntjobno`, and `ppmlink.ivjobno` / `ppmlink.cjobno`, was counted. `itemexport.jobno` was compared as text. Four tables have rows.

| Table | Primary key | Matching column | Rows |
|---|---|---|---|
| `dbo.orders` | `jobno` | `jobno` | 1, the job itself |
| `dbo.selectsection` | `sskey` | `jobno` | 1: `sskey` 16807, `sscode` 2 |
| `dbo.userlog` | `ulkey` | `jobno` | 3 |
| `dbo.allocation` | `aekey` | `jobno` | 1: `aekey` 29099 |

`dbo.jobplan` has 0 rows. `dbo.status` has 0 rows. `dbo.engcale`, `dbo.anidlog`, `dbo.jobform`, `dbo.jobworksheet`, and the other `jobno` tables have 0 rows. No parent-job columns point at 15940.

`selectsection.sscode` 2 is Commercial / Air Conditioning. That is the same subsection as operative row 171. `selectsection` stores the job and the subsection. It does not store an employee or an operative key, so it is not the engineer relationship. The `ORDER` log is at 17:40 and the allocation log is at 19:12, so the section row belongs with job creation.

### userlog

| `ulkey` | `formname` | `luser` | `ldate` | `cname` |
|---|---|---|---|---|
| 199857 | `ORDER` | administrator | 2026-10-15 17:40:11 | null |
| 199858 | `Add - (Job Allocation) Benoperative` | administrator | 2026-10-15 19:12:38 | DESKTOP-34RVAO8 |
| 199859 | `UPDATE-INFO(Job Allocation)` | administrator | 2026-10-15 19:12:41 | DESKTOP-34RVAO8 |

The first log is the POC job create. The next two were written about 92 minutes later on this computer. The form name contains the employee display name.

## Path from the job to Benoperative

| Step | From | To | Value |
|---|---|---|---|
| 1 | `allocation.jobno` | `orders.jobno` | 15940 |
| 2 | `allocation.idno` | `employee.idno` | 59 |
| 3 | `employee.name` | | Benoperative |
| 4 | `operative.idno` | `employee.idno` | operative `okey` 171, not stored on the allocation |

`allocation.idno` 59 was joined back to `employee.idno`. It is not `operative.okey`. The operative key 171 does not appear on the job.

There is no `jobplan` path. There is no `engcale`, `ooheng`, `jobform`, or `jobworksheet` path.

## jobplan

Job 15940 has no `dbo.jobplan` row. Benoperative does not appear in `jobplan` for this job, or for any other job.

## allocation

One row, `aekey` 29099.

| Meaning | Column | Value |
|---|---|---|
| Row identity | `aekey` | 29099, identity |
| Job | `jobno` | 15940, foreign key to `orders.jobno` |
| Person | `idno` | 59, foreign key to `employee.idno` |
| Arrival | `arrivaltime` | null |
| Departure | `departuretime` | null |
| Plan time on this table | `pptime` | null |
| Other timestamps | `sdatetime`, `mapstime`, `mapetime` | null |
| Diary link | `jpkey` | null |
| Comments, username, location | `comments`, `username`, `lat`, `long` | null |
| Visit flag | `workdone` | 0 |
| Type flag | `etype` | 0 |
| Image columns | `tbform`, `tbsign`, `raform1`, `pwform1`, and the other image columns | null |

The remaining non-null values are the column defaults, including `chasstype` 1 and the `tinyint` zeros. The row does not contain an arrival, a departure, or a completion time.

Benoperative does appear in `allocation` for job 15940. This is his only allocation row. Arrival and departure are null, so the row does not record attendance.

`allocation` has no separate operative column. The person is `employee.idno`.

## status

`dbo.status` has no row for job 15940. Assigning Benoperative did not require a status row on this job. The five hosted comparison jobs below also have no `status` row.

## Comparison jobs

Benoperative has no earlier job, so the comparison uses the latest hosted `Add - (Job Allocation)` logs. Those logs were written on `JOBFLOW-RDSH01` / `JOBFLOW-RDSH02` on 25–26 Sep 2025. Each one has both a plan and an allocation. `jobplan.cdate` is 10 to 26 seconds after the add log for the first plan. Later extra plans on job 15915 are a few minutes after the same log. None of these jobs has a `status` row. None has an `UPDATE-INFO(Job Allocation)` log within 10 seconds, unlike job 15940.

| Job | Employee | `idno` | Plans | Allocations | Arrival populated | `status` rows |
|---|---|---|---|---|---|---|
| 15940 | Benoperative | 59 | 0 | 1 | no | 0 |
| 15631 | Shahram Omidi | 56 | 1 | 1 | no | 0 |
| 15814 | Owen Richards | 14 | 1 | 1 | no | 0 |
| 15814 | Shahram Omidi | 56 | 1 | 1 | no | 0 |
| 15912 | Owen Richards | 14 | 1 | 1 | no | 0 |
| 15913 | Greg Donovan | 38 | 1 | 1 | no | 0 |
| 15915 | Andrew Wilkins | 11 | 4 | 1 | no | 0 |

Operative keys are not stored on these rows. Each person is `employee.idno`, and each person has one or more `operative` rows for subsections.

The hosted plan windows are dispatcher times, not the job receipt or deadline. Examples: job 15631 is planned 2025-09-29 08:30–16:30; job 15912 is planned 2025-09-26 08:00–09:00. Job 15910, Andrew Wilkins, is the nearby case that later gained arrival 2025-09-26 12:00 and departure 14:30. Its plan is 13:24–14:24, so the attendance times are not the plan times.

Job 15940 is representative of the log name and of an untimed `allocation` row. It is not representative of the hosted action, because those jobs also have `jobplan`.

## Why this is not an implementation

The observed relationship on job 15940 is real:

`orders.jobno` = `allocation.jobno` = 15940, and `allocation.idno` = `employee.idno` = 59 = Benoperative.

Copying only that row would miss the `jobplan` row that the hosted client writes under the same log name. The plan requires `pptime`, and the hosted times are chosen windows. They are not `orders.date` and not `orders.deadline`. Writing a plan would invent that window. Writing only the empty allocation would not match the hosted jobs.

`userlog` was written by the client for this action. `status` was not. A POC write of the log text would be an imitation of the audit row, not proof that the engineer link exists. The engineer link is the allocation row.

No experimental insert is proposed for execution.
