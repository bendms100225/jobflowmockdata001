# Open questions

These have to be answered before the portal writes to JobFlow. The prototype and `JobFlowDemo` disagree in several places. Where they disagree, the database evidence is recorded in `docs/jobflow-integration-map.md` and the prototype behaviour is in `docs/prototype-requirements.md`. Nothing below was guessed into a mapping.

The restored database is still on `localhost\SQLEXPRESS` as `JobFlowDemo`. It was not modified after the restore.

Direct mutation of hosted JobFlow remains blocked. The standalone `JobFlowDemo` database is disposable, and the proof of concept may insert marked `orders`, `selectsection`, and `userlog` rows there through `ExperimentalDirectSqlJobFlowJobWriter`. That writer is not production-safe and is not supplier-supported. `dbo.status`, `dbo.jobplan`, and `dbo.allocation` are still not written. The default registration outside that development gate remains `DisabledJobFlowJobWriter`. See `docs/poc-database-write-plan.md` and `docs/development-safety-boundary.md`.

A follow-up search found no local JobFlow program attached to that database. The only client is a RemoteApp on `JOBFLOW-RD01.AD.JOBFLOW.CO.UK` (`hosted.jobflow.co.uk`). It was not opened and not repointed. The create-job SQL trace is still blocked. Details are in `docs/jobflow-application-access.md`.

## Blockers

1. **Is this backup the Genco database the portal must write to?**  
   The backup is named `JobFlowDemo`, taken 13 Oct 2025 on `WEBNAN\SQLEXPRESS`. It looks like a working JobFlow database (15,792 jobs through 26 Sep 2025), but the prototype's response-category table does not match it. Confirm the live server, database name, and that this schema is current before any write design is treated as final.

2. **What rows does the JobFlow application insert when someone clicks to create a job?**  
   There are no stored procedures, triggers, or functions. Every job has an `orders` row and a `selectsection` row, and 15,773 of 15,792 have a `userlog` row with `formname = 'ORDER'`. Most jobs also have `status`, `jobplan` and `allocation` rows, but not all. `orders` has 211 columns. A portal insert that only fills the columns we understand can still produce a job the rest of JobFlow does not treat as valid.  
   Need a SQL trace, or the JobFlow source, for one normal reactive job: every `INSERT`, in order, with the columns that are not left to defaults.

3. **What is `orders.jobtype` for a reactive call-out?**  
   The column is an `int`. This backup contains only 0, 1 and 2. There is no lookup table and the word Reactive is not stored. The prototype's hidden value `Reactive` cannot be written until the integer (or whatever else the application sets) is known.

4. **What are the `section` / `subsection` codes for OOH?**  
   The prototype says both are new records named `OOH`. They are not in this database. Existing `sscode` values are short codes such as `16` (General Maintenance), not the word `OOH`. Someone has to create them in JobFlow, using JobFlow's own screen, and then tell us the `scode` and `sscode`. Do not invent those codes in the portal.

5. **Which table means "allocate this engineer"?**  
   Re-checked on the live `JobFlowDemo` database on 15 Oct 2026. No assignment write was added.  
   `jobplan.jobno` and `allocation.jobno` both reference `orders.jobno`. `jobplan.idno` and `allocation.idno` both reference `employee.idno`. They do not reference `operative.okey`. `operative.okey` is the operative-row identity; `operative.idno` is the employee. One employee has many operative rows, one per subsection. `orders` has no engineer column. `orders.jlono` is 0 on every job.  
   Of 15,809 jobs: 13,539 have both a plan and an attendance row, 19 have a plan only, 73 have attendance only, and 2,178 have neither. Every `jobplan` row has `jobno`, `idno`, and `pptime`. `petime` is null on 108 rows, and those are mostly the `Appointment` employee (`idno` 1) with `pptime` copied from the deadline. A job can have several engineers, and one engineer can have several `jobplan` or `allocation` rows on the same job.  
   `allocation` is the attendance record when both times are filled: 25,786 of 26,870 rows have arrival and departure, and those rows are exactly the rows with comments. 1,084 rows have neither time, no comments, no username, and no location. 1,018 of those still have a `jobplan` row for the same employee. `vdone` does not separate planned from attended: 13,366 of 15,416 `vdone = 0` plan rows already have an attendance row for that person.  
   Of 1,949 plan rows for a real operative with no attendance row, 935 carry notes such as `OFF`, bank holiday, or a project name. The other 1,014 have a blank note. Their windows are not the job's received time or deadline: 5 start within 10 minutes of `orders.date`, and 2 start within 10 minutes of `orders.deadline`. Across all 31,200 plans, 2,504 start near receipt and 210 end near the deadline. Durations are mixed (13,029 at 31–60 minutes, 10,210 at 4–8 hours).  
   The newest pre-POC jobs show the same split. On job 15910, Jerome Jarvis (`employee.idno` 13) and Shahram Omidi (`56`) have a one-hour `jobplan` and an `allocation` row with null arrival and departure. Andrew Wilkins (`11`) has a later one-hour plan and an `allocation` row with both times filled, and those times are not the plan window. Job 15915 has several day-long plans per person and one untimed `allocation` row each. `allocation.sdatetime` is null on all 25,786 timed rows, so it is not a create time.  
   `userlog` form names `Add - (Job Allocation) {employee.name}` match both tables: 18,427 of 20,244 name-matched logs have a plan and an attendance row for that person, 22 have a plan only, and 145 have attendance only. `jobplan.cdate` is within 10 seconds of that log on 6,097 rows, so the log does not identify a single insert.  
   Job 15940 was created by the POC at 17:40 with no engineer link. At 19:12 on this computer the JobFlow client wrote `userlog` `Add - (Job Allocation) Benoperative` and `allocation` `aekey` 29099 with `idno` 59. Arrival and departure are null. There is still no `jobplan` row and no `status` row. `operative.okey` 171 is not stored. The detail is in `docs/job-15940-engineer-trace.md`.  
   That one row is not the implementation template. The same log name on hosted jobs 15631, 15814, 15912, 15913, and 15915 also has a `jobplan` row whose `cdate` is 10–26 seconds after the log, and whose start and end are not the job receipt or deadline. Copying only the empty allocation would miss that plan. Copying a plan would invent the window. No experimental assignment write was added.

6. **Who is `orders.username` / `orders.calltakenby` when the person is an Entra engineer?**  
   On 15,789 jobs those two columns are the same, and they are JobFlow office logins (`loginuser`), not `employee.name` (0 matches). The portal's user is an engineer, and engineers are `employee.idno`. There are only four `loginuser` rows.  
   Need the login name, if any, that an OOH engineer should store, or confirmation that a service account is acceptable. `calltakenby` is not the external caller.

7. **How does an Entra object id become `employee.idno`?**  
   No object-id column exists. `employee.email` is filled for 11 of 51 people. `employee.pemail` is empty. Matching on email will miss most engineers. Need the join key, and where it will live (portal store versus a JobFlow column). Adding a column was not done in this investigation.

8. **Where do "Reported by" and "Site contact" go?**  
   The form has two names. `orders.contactname` (`varchar(50)`) is filled on 15,788 jobs. `orders.rpby` is filled on 4,914 and often differs from `contactname`; it matches an employee name on only 1,343 jobs. `orders.cpofcontact` is empty.  
   Need a JobFlow user to say which screen field is `contactname`, and what `rpby` means. Also decide what happens when a name is longer than 50 characters, and when the two portal names differ.

9. **Which engineers are allowed to appear, and which clients?**  
   `ooheng` is the only OOH-specific person table. It is empty. `operative` links people to trade subsections, and there is no OOH subsection. `client` has no "OOH cover" flag, no sector, and no PO-required flag. All 656 clients have `cd = 1`.  
   Need the production rule for the client list and the engineer list, and the rows JobFlow staff will maintain (`ooheng`, and/or an OOH subsection plus `operative` rows). The proof of concept does not wait for that rule: it lists every `dbo.employee` row that has a `dbo.operative` row. There is no proven active flag, and the OOH subsection is not required.

## Decisions the prototype leaves open

10. **Project manager, coordinator, and 30% margin.**  
    `orders.pmanager` and `orders.pcodinator` exist and are null or blank on every job. `orders.profitper` is null on every job. The prototype wants the literals `OOH`, `OOH` and `30`, stored server-side. Confirm those literals with Genco before writing them. Do not infer them from this backup.

11. **Who sends the engineer email?**  
    No procedure sends mail. `emaildone` has rows for 1,500 of 15,792 jobs and references `employee.idno`. Most employees have no email. The prototype allows the notification to be JobFlow's or the portal's, and it must survive a failed email without losing the job.  
    Need the from-address, the template, and what to do when `employee.email` is blank (the success screen currently tells the user to phone).

12. **Job number format.**  
    The prototype displays `JF-` plus an integer. `orders.jobno` is the integer alone. Confirm the screen should show `jobno` with no prefix.

13. **Response buttons.**  
    The prototype's two large buttons are 4 hours and 8 hours, and its mock category list has ten codes. This database's `category` rows are code 1 = 2 hours, 2 = 4 hours, 3 = 24 hours, 4 = 72 hours, 5 = 168 hours. There is no 8-hour row. `category.categoryname` is null; the hours are the integer `category.category`. 6,004 jobs use `categorycode` 0, which is not in the table.  
    The portal must read `category` rather than copy the prototype list. Confirm with Genco that this five-row table is complete, and which codes the two large buttons should use.

14. **Duplicate warning and Recent.**  
    The prototype keeps both in a portal audit so they work without reading JobFlow. Recent is "jobs I logged in 24 hours". The warning is "any logger, same site, 12 hours", and it does not block submit.  
    Confirm that office jobs raised directly in JobFlow do not need to appear in that warning. If they do, the read is `orders.sitecode` plus `orders.date`, which is a different population.

15. **Idempotency.**  
    Nothing in JobFlow deduplicates a retried create. The portal has to keep its own key and must not insert twice. Confirm that a failed create really inserted nothing before the portal retries. That depends on question 2 (how many statements the create uses, and whether they are in one transaction). The database has no transaction wrapper of its own.

16. **`orders.rjcdate`.**  
    It equals `orders.date` on 13,788 jobs and is later on 1,995. The deadline is calculated from `date`, not from `rjcdate`. The name is not defined. Do not write it until the trace shows the application setting it.

17. **`orders.access`.**  
    The column exists (`varchar(200)`) and is blank on every job. The prototype puts access in the description. Confirm access stays inside `description`.

18. **Length limits.**  
    `orderno` is `varchar(30)`. `contactname` and `username` / `calltakenby` are `varchar(50)`. `description` is `text`. The prototype does not limit length. Confirm truncation rules, especially the PO number.

19. **Inactive sites.**  
    Four `houselist` rows have `cd = 0`; the other 999 have `cd = 1`. `cd` is undocumented. Confirm whether `cd = 0` means "do not offer this site".

20. **Authentication boundary.**  
    Entra ID plus an "OOH Engineers" group is the prototype's production sign-in. `loginuser.password`, `loginuser.uemailpass`, `client.password`, `employee.epass` and `employee.trpassword` exist in JobFlow. The portal must not read or collect those passwords. Confirm the Entra tenant, the group name, and that JobFlow passwords stay unused.

## Not blockers for a later read-only prototype against this backup

These are already proven and can be read without further discovery: `client`, `houselist`, `orders.jobno`, `orders.description`, `orders.orderno`, `orders.date`, `orders.deadline`, `category`, `employee`, `operative`, `section`, `subsection`, `selectsection`. Writing still waits on the blockers above.
