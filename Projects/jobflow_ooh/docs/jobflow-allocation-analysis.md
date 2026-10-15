# JobFlow allocation analysis

Read-only. The concrete row is `dbo.allocation.aekey` 29099 on job 15940. The wider counts below were taken earlier on 15 Oct 2026, before that row existed, except where this note says otherwise.

## Keys

`aekey` is the identity primary key.

`jobno` references `orders.jobno` (`FK__allocatio__jobno__45F365D3`).

`idno` references `employee.idno` (`FK__allocation__idno__46E78A0C`).

There is no foreign key to `operative.okey`. `jpkey` is `varchar(50)`, not a foreign key to `jobplan.jpkey`. On the 15 Oct count, 2,932 values were numeric and 1,622 of those matched a `jobplan` row for the same job and the same employee.

## Job 15940

`aekey` 29099 was written on 15 Oct 2026 at 19:12, with `userlog` `Add - (Job Allocation) Benoperative` (`ulkey` 199858, computer `DESKTOP-34RVAO8`).

| Column | Value |
|---|---|
| `jobno` | 15940 |
| `idno` | 59, which joins to `employee.name` Benoperative |
| `arrivaltime` | null |
| `departuretime` | null |
| `pptime`, `sdatetime`, `mapstime`, `mapetime` | null |
| `comments`, `username`, `jpkey`, location | null |
| `workdone` | 0 |
| `etype` | 0 |
| Image columns | null |

`operative.okey` 171 belongs to this employee and is not stored on the allocation. The job has no `jobplan` row and no `status` row.

This row shows that an allocation can exist before arrival. It does not show that an allocation is the only record the hosted client writes. See the comparison in `docs/job-15940-engineer-trace.md`.

## What the older rows show

Before `aekey` 29099, the table had 26,870 rows. Every row had `jobno` and `idno`. 25,786 had both arrival and departure, and those were exactly the rows with comments. 1,084 had neither time, no comments, no username, and no location. 1,018 of those untimed rows also had a `jobplan` row for the same employee. `sdatetime` was null on all 25,786 timed rows, so it is not a create timestamp. `workdone` was never 1. `etype` was 0 on every row. `absent` was never 1.

A job can have several engineers, and one engineer can have several allocation rows for the same job (4,754 job-and-employee pairs).

Timed rows are attendance. Untimed rows are not attendance, and on the hosted jobs they sit beside a diary row. Job 15940 is the untimed shape without the diary row.
