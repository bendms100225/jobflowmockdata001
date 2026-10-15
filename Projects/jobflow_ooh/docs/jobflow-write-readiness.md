# JobFlow engineer-assignment write readiness

Decision on 15 Oct 2026: do not implement an experimental engineer assignment.

The database was not modified by this investigation. The POC writer still inserts `orders`, `selectsection`, and the `ORDER` userlog row only.

## What is proven for job 15940

Benoperative is `employee.idno` 59 and `operative.okey` 171. The job stores the employee id, not the operative key.

`allocation.aekey` 29099 has `jobno` 15940 and `idno` 59. Arrival and departure are null. There is no `jobplan` row and no `status` row. Two userlog rows at 19:12 record `Add - (Job Allocation) Benoperative` and `UPDATE-INFO(Job Allocation)`.

That is enough to say this job is linked to that employee through `allocation.idno`.

## What is not proven

The hosted client, using the same `Add - (Job Allocation)` form name, also writes `jobplan` about 10–26 seconds later. Those plans have a start and end that are not the job's received time and not its deadline. Job 15940 does not have that plan.

An insert of only `allocation (jobno, idno)` would match job 15940 and would not match jobs 15631, 15814, 15912, 15913, and 15915.

An insert of `jobplan` would require a `pptime`. No rule in the data turns `orders.date` and `orders.deadline` into that pair.

`userlog` accompanied the desktop action. It is an audit row. It does not replace the employee foreign key. `status` was not written for job 15940 or for those comparison jobs, so it is not required by the observed assignment.

`operative.okey` is not required. The foreign key used by both `allocation` and `jobplan` is `employee.idno`.

## Observed shape, not an approved statement

If a later trace shows that the current client really stops at the allocation row, the row on job 15940 is:

- table `dbo.allocation`
- supplied values `jobno`, `idno` = `employee.idno`
- `aekey` generated
- arrival, departure, comments, username, `jpkey`, and the image columns left null
- other non-null flags left to their defaults, including `chasstype` 1
- no `operative.okey`
- no second row for the same job and employee in this example
- read-back: `allocation.jobno` and `allocation.idno` join to `orders.jobno` and `employee.idno`, and `employee.name` must equal the selected name

That statement is not approved for execution. The hosted jobs show a diary row as well, and the diary times are not known.
