# Prototype requirements

Source: `Genco Construction_ out of hours (prototype) (1) 1.html` (title: Genco Construction: out of hours).

The file is a static, mobile-first prototype. Its own comment says the JobFlow function names are conceptual integration points, not a known JobFlow API. Mock clients, sites, engineers, categories and job numbers in the file are not JobFlow data. This document records what the prototype specifies. Whether each item exists in JobFlow is in `docs/jobflow-integration-map.md`.

## Product intent

A lightweight portal for on-call engineers to raise and allocate an out-of-hours job without using the full JobFlow dashboard. The engineer signs in with a Genco Microsoft work account. Creating a job writes a live JobFlow job and allocates an engineer. The engineer later completes the work in the existing JobFlow engineer app. The portal does not complete the job.

Branding is configuration, not hard-coded screen markup, so another customer can be re-skinned later. The current brand is Genco Construction (charcoal `#182126`, green `#76bf43`). Optional footer text: "Portal provided by DMS".

## Screens

| Screen | What the engineer sees |
|---|---|
| Login | Genco logo, "Out of hours job logging", "For on-call engineers", **Sign in with Microsoft**. Error if the account is not allowed. Hint: use the Genco work account, or call the office. Footer "Portal provided by DMS". |
| New job | "New out of hours job". Logged-in engineer name. Draft resume/discard. Error summary. Four groups: Customer, Job details, When, Who attends. Live deadline. Side summary on wide screens. Sticky bar: "On site by" and **Review job**. |
| Client sheet | Search "Client, site or postcode". Recent sites (up to 3), then all clients. A search lists matching sites first (max 25), then matching clients. Empty text: check spelling, or call the duty manager if the client is not set up for OOH. |
| Site sheet | Shown when the client has more than 6 sites. Search "Address, block or postcode". |
| Response sheet | Every response category, labelled in hours (or days when the value is at least 48 and divisible by 24). |
| Engineer sheet | Engineers on the OOH subsection. Signed-in engineer first, then A to Z. Shows discipline and phone. |
| Account sheet | Name, username, display theme (match phone / light / dark), **Sign out**. |
| Review | "Check before creating". Back to edit. Each field has Change. States that this creates a live JobFlow job and allocates it. Duplicate warning repeated. **Create & allocate job**, disabled while offline. |
| Submitting | "Creating and allocating job". Keep the screen open. Four steps, in order. |
| Success | "Job created and allocated", job reference with Copy, client/site, allocated engineer, response, deadline, notification result. Call the engineer if allocated to someone else. **Log another job**. Tells the user to complete the job in the JobFlow engineer app. |
| Recent | "My recent jobs". Jobs this engineer logged in the last 24 hours. Empty state. **Log a new job**. |
| Prototype tools | Not part of the product. Simulates JobFlow down, email failure, no signal, and the clock-change nights of 25 Oct 2026 and 29 Mar 2026. Shows an integration-call log. |

Header (after login): brand returns to New job, "My recent" with a count, account initials.

## Form fields

All of these are required before review. The form object is:

| Field | Control | Notes |
|---|---|---|
| Client | Search picker | Shows client name and sector. Changing client clears the site unless a site was chosen in the same action. |
| Site | Radios, or search if more than 6 sites | Name, town, postcode. If the client has one site, it is selected automatically. |
| Order / PO number | Text, characters capitalised by the browser | Placeholder "Customer's reference". Always required in validation. |
| Job description | Text area | Hint: "Fault, risks and access details." Placeholder describes a tenant fault, an RCD, and access via a concierge. |
| Reported by | Text | "Person who called it in". |
| Site contact | Text, or "Same as reported by" | "Who the engineer contacts on site". If same-as is on, the value saved is the reported-by name. |
| Call received | Now, or Earlier | Earlier: Today or Yesterday, plus a time. UK civil time (`Europe/London`), not the phone timezone. |
| On site within | Two buttons, 4 hours and 8 hours, plus "Other response time" | The form stores hours. The category **code** is what is sent to JobFlow. |
| Allocate to | "Assign to me" plus a picker | Engineer name, "(you)" if self, discipline. |

Hidden from the engineer, and specified as server-side configuration that must win over anything the browser sends:

| Name in the prototype | Value |
|---|---|
| section | `OOH` |
| subsection | `OOH` |
| jobType | `Reactive` |
| targetMarginPercent | `30` |
| projectManager | `OOH` |
| projectCoordinator | `OOH` |

The prototype says section and subsection are new JobFlow records, used for reporting and for which engineers can be allocated.

Payload passed to conceptual `createJob` (plus those defaults):

- client id, site id
- description (trimmed)
- reported by (trimmed)
- site contact name (resolved)
- order reference (trimmed, or null if blank — validation already rejects blank)
- received at (frozen)
- logged at (submit time)
- response category code
- response hours
- deadline (frozen)
- logged-by engineer id
- idempotency key

`createJob` returns `jobId` and `jobNumber`. The mock number format is `JF-` plus an integer starting at 248317. That format is prototype-only.

## Reference data the screens expect

| Data | Fields used on screen |
|---|---|
| Client | id, name, sector, poRequired. Search by name. Count of sites. |
| Site | id, client id, name, town, postcode. Search by name, town, postcode. |
| Engineer | id, name, discipline, phone, email. |
| Response category | code, hours. Sorted by hours for the "other" list. |
| Portal user | username, linked engineer id. Display as `username@genco.example` in the prototype only. |

`poRequired` is on every mock client and is `true`, but the tag renderer is empty and validation always requires the PO. The flag does not change behaviour in this prototype.

## Business rules

Time zone is `Europe/London` for received time, deadline display, and "today / tomorrow / yesterday". The phone's own timezone is ignored. The same rule is specified for the server.

Call received:

- **Now** is the current London time and keeps moving (the screen refreshes the clock every 30 seconds).
- **Earlier** is today or yesterday only, at a typed time.
- If that civil time does not exist (clocks spring forward, 01:00–02:00), reject it.
- If that civil time happens twice (clocks fall back), the engineer must choose the first occurrence (BST) or the second (GMT).
- Reject a received time more than about 2 minutes in the future. If they picked today, suggest yesterday.
- Reject a received time more than 24 hours ago. Message: log it in JobFlow instead.
- A restored draft must not treat "now" as the current time. It pins the saved received time and asks the engineer to check it.

Deadline:

- Deadline = received time + response hours of real elapsed time (not "same clock time next day").
- Show "On site by" with London time, timezone abbreviation, day label, and a relative phrase ("in 3 h 10 min" / "20 min ago").
- If the deadline is already past, warn and tell them to check the received time. Submission is not blocked by a passed deadline.
- If under 60 minutes remain, warn.
- If the timezone abbreviation changes between received and deadline, warn that the clocks change inside the window.
- The deadline used on submit is frozen when the engineer opens Review, not recalculated at submit.

Response categories:

- Do not hard-code the code-to-hours table. The prototype's own mock is not in hour order (`10` = 240 hours) and it says production must read it.
- The two large buttons are 4 and 8 hours, marked configurable (`PRIMARY_RESPONSES`).
- 8 hours and under use the urgent colour. Longer responses use the other colour.
- The value sent to JobFlow is the category code for that hour value, not the hour number alone.

Validation (in this order for the error summary): client, site, PO, description, reported by, site contact, received, response, engineer. Errors show after the field is touched, or all at once on Review. Review does not open while any error remains.

Other behaviour:

- Offline: the form can still be filled and drafted. Create is blocked. Banner: jobs cannot be created until back online.
- Draft is stored in browser `localStorage`, keyed by username. Empty forms are not stored. Resume and discard are offered on the next visit.
- Duplicate warning: same site, and a portal-logged job created in the last 12 hours. Includes jobs logged by other engineers. Warning only. It does not block submit. Copy: check it is not the same problem before creating another job.
- Recent list: jobs whose logged-by engineer is the signed-in engineer and whose created time is within 24 hours. The duplicate check uses a wider set (any logger, 12 hours, same site).
- Client search with an empty query shows up to 3 recent sites for this engineer, then every client.
- Site search matches are preferred over client-name matches.
- If nothing matches, the engineer is told to call the duty manager when the client is not set up for OOH.
- The prototype comment says a housing client may have thousands of properties, and production should search sites on the server rather than download them all at login. The prototype itself downloads every site at login.
- Idempotency key is created when Review opens and is kept until success, so a retry of a failed create does not mint a second key. A double-tap is also ignored while a submit is in progress.
- Success clears the draft, the form, and the idempotency key.
- JobFlow failure on create: no job was created, details are kept, return to Review. If it keeps failing, phone the duty manager.
- Allocation and diary steps in the prototype do not have their own failure path. Only create and email do.
- Email failure after the job exists: the job and diary entry are kept. The success screen says to phone the engineer now.
- If email succeeded and the response is 8 hours or under and the engineer is someone else: remind the user that overnight email can be missed, and offer a call.
- If the allocated engineer is someone else, show a `tel:` link. The prototype has no SMS.
- Copy job number uses the clipboard.
- Theme is stored in `localStorage`.
- Sign out clears the user and the form and returns to login. It does not clear the on-device draft key until a later sign-in (the draft key is per username).

## Submit workflow

1. Validate. Freeze received time and deadline. Ensure an idempotency key.
2. Create the job in JobFlow (`createJob`), merging server defaults.
3. Allocate the chosen engineer (`allocateEngineer`) by job id and engineer id.
4. Add a diary/app entry (`createDiaryEntry`) from the frozen received time to the frozen deadline. The prototype marks this span as an assumption.
5. Email the allocated engineer (`sendEngineerNotification`). Failure is recorded and does not roll back the job.
6. Record the submission in a portal-owned audit store (`recordSubmission`) so Recent and the duplicate warning work without reading JobFlow.
7. Show the job number.

## Authentication

Production sign-in is Microsoft Entra ID, then a check that the account is in an "OOH Engineers" group, then a map from the Entra object id to the JobFlow engineer record. The prototype's account chooser is fake. Rejected users see: the account is not set up for the portal; ask the office to add them to the OOH Engineers group.

The prototype also says the portal's own identity store would have one account per engineer, with MFA, lockout and device trust. The later comment replaces that with Entra plus the group check.

## What is portal-owned, not JobFlow

- Entra sign-in and the OOH Engineers group check.
- The draft in browser storage.
- The submission audit used for Recent and duplicate warnings.
- Branding and the 4-hour / 8-hour button choice.
- Idempotency key handling.
- The decision to email, if JobFlow does not already do it. The prototype leaves "JobFlow-native or portal-side" undecided.

## Explicit non-goals in the prototype

- No job completion, worksheets, parts, quotes, invoices, or photos.
- No rota. Every engineer on the OOH subsection can be picked. There is no availability check.
- No editing or cancelling a job after success.
- Recent is not a JobFlow inbox. It is "jobs you logged".
- The Prototype tools screen is not a product screen.
