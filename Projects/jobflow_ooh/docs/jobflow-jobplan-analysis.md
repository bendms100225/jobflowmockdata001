# JobFlow jobplan analysis

Read-only. Job 15940 has no `dbo.jobplan` row. Benoperative (`employee.idno` 59) has no `jobplan` row on any job.

## Keys

`jpkey` is the identity primary key.

`jobno` references `orders.jobno` (`FK__jobplan__jobno__0697FACD`).

`idno` references `employee.idno` (`FK__jobplan__idno__078C1F06`).

There is no foreign key to `operative.okey`. The person column is the employee id.

## Columns that are populated

Across 31,200 rows, `jobno`, `idno`, and `pptime` are never null. `petime` is null on 108 rows. Those null ends are mostly employee `idno` 1, `Appointment`, with `pptime` copied from the job deadline. `cdate` is null on 3,751 rows. `info` is blank on 30,260 rows.

`vdone` is 0 on 15,416 rows and 1 on 15,784. It does not mean "not yet attended": 13,366 of the `vdone = 0` rows already have an allocation for the same person. `prapp` is 1 on 29,979 rows. `oncall` is never 1.

## The hosted allocation action does write a plan

The latest hosted logs named `Add - (Job Allocation) {employee.name}` were written on 25–26 Sep 2025. For those jobs the first `jobplan.cdate` is 10 to 26 seconds after the log:

| Job | Employee | `idno` | `jpkey` | Plan window | Seconds after the add log |
|---|---|---|---|---|---|
| 15631 | Shahram Omidi | 56 | 33475 | 2025-09-29 08:30–16:30 | 12 |
| 15814 | Owen Richards | 14 | 33490 | 2025-09-30 08:30–16:30 | 18 |
| 15814 | Shahram Omidi | 56 | 33491 | 2025-09-30 08:30–16:30 | 17 |
| 15912 | Owen Richards | 14 | 33471 | 2025-09-26 08:00–09:00 | 10 |
| 15913 | Greg Donovan | 38 | 33476 | 2025-09-29 08:30–11:30 | 13 |
| 15910 | Andrew Wilkins | 11 | 33472 | 2025-09-26 13:24–14:24 | 13 |

Job 15915 has several plans per person, the first about 20 seconds after the log and more plans a few minutes later. The windows are scheduled dates. They are not `orders.date` and they are not `orders.deadline`.

Across all plans, 2,504 start within 10 minutes of `orders.date` and 210 end within 10 minutes of `orders.deadline`. Durations are mixed: 13,029 are 31–60 minutes and 10,210 are 4–8 hours.

## Job 15940

The local log `Add - (Job Allocation) Benoperative` at 2026-10-15 19:12:38 did not create a plan. That is a real difference from the hosted rows above. It is not evidence that a plan is never part of allocation. The hosted client created one under the same form name, and the start and end were chosen rather than copied from the job.
