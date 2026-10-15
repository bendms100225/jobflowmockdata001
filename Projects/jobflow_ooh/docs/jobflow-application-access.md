# JobFlow application access

Checked on this PC on 15 Oct 2026. No JobFlow program was started, and `JobFlowDemo` was not changed. No backup was taken because no test job was created.

## Result

There is no local JobFlow application connected to `localhost\SQLEXPRESS` / `JobFlowDemo`.

The only JobFlow client on this machine is a RemoteApp shortcut. It opens the hosted JobFlow desktop on `JOBFLOW-RD01.AD.JOBFLOW.CO.UK` through the gateway `hosted.jobflow.co.uk`. That is not the restored demo database. The shortcut was not launched, and its configuration was not changed.

Phases B, C and D (Extended Events, a `PORTAL-TRACE-*` job, before/after row diffs, and separate assign/plan/status traces) were not run. Doing them in the RemoteApp would write to the hosted JobFlow system, not to `JobFlowDemo`.

## What was found

| Place checked | Result |
|---|---|
| Windows services | No JobFlow or Genco service |
| Running processes | SQL Server Express only (`sqlservr`, `sqlwriter`, `sqlceip`, `SQL2025-SSEI-Expr`). No JobFlow process |
| IIS | `appcmd.exe` is not installed. No local web site |
| Installed programs (uninstall registry, 64-bit and 32-bit) | No JobFlow or Genco entry |
| `C:\Program Files` and `C:\Program Files (x86)` | No file or folder whose name contains `jobflow` (searched three levels down) |
| Downloads | No JobFlow installer |
| `%LOCALAPPDATA%`, `%APPDATA%`, Projects, Documents | No JobFlow application directory |
| SQL sessions on `localhost\SQLEXPRESS` | No application session in `JobFlowDemo`. The only user session was this investigation's `SQLCMD` |
| Start menu | One shortcut: `JobFlow (Work Resources).lnk` |

The shortcut runs `mstsc.exe` against a workspace RDP file. Relevant settings, with credentials and the file signature omitted:

| Setting | Value |
|---|---|
| Type | RemoteApp (`remoteapplicationmode`) |
| Published name | JobFlow |
| Remote application | `\|\|JobFlow` |
| Full address | `JOBFLOW-RD01.AD.JOBFLOW.CO.UK` port 3389 |
| Gateway | `hosted.jobflow.co.uk` |
| Workspace id | `JobFlow-RD01.ad.jobflow.co.uk` |
| Load balance | `JobFlow_Hosted01` |
| Feed resource alias | `JobFlow-JobFlow_Hosted01-CmsRdsh` |
| Feed updated | 15 Oct 2026 11:40 UTC |
| Credentials | Prompt on the client. No password is stored in the RDP file |

No local executable, DLL, `web.config`, connection string, installer, or JobFlow log was found. There is nothing here whose connection string can be shown to point at `JobFlowDemo`.

## Why a trace was not started

Extended Events on `JobFlowDemo` would only see SQL if a program connected to that database and created a job. Nothing is connected. Pointing the RemoteApp at `localhost\SQLEXPRESS` would mean changing the hosted application's configuration, which was not permitted.

A hand-written `INSERT` into `orders` would not be the application's workflow. It was not done.

## What is still required before the create-job SQL can be known

One of these, aimed at `JobFlowDemo` only:

- The JobFlow desktop (or its DLLs) installed locally, with a connection string that already uses `localhost\SQLEXPRESS` / `JobFlowDemo`, or
- Explicit permission to run a trace on a non-production JobFlow session that is already using this restored database.

Until then, `orders.date`, `deadline`, `orderno`, `description`, `contactname`, `rpby`, `username`, `calltakenby`, `jobtype`, `pmanager`, `pcodinator` and `profitper` stay as documented from SQL alone in `docs/jobflow-integration-map.md`. The screen that fills each one is still unknown, because the application was not observed.
