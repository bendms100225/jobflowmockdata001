# JobFlowDemo schema inventory

Generated from the restored database `JobFlowDemo` on `localhost\SQLEXPRESS`. Read-only catalog queries only. No test data was inserted.

## Restore

| Item | Value |
|---|---|
| Backup file | `JobFlowDemo.bak` (full backup, 13 Oct 2025 10:18, server `WEBNAN\SQLEXPRESS`, SQL Server 15.0.2145) |
| Database name in backup | `JobFlowDemo` |
| Backup database version | 904 (SQL Server 2019), compatibility level 100, collation `Latin1_General_CI_AS`, recovery model SIMPLE |
| Restored to | `localhost\SQLEXPRESS` (MSSQL17), upgraded by the engine from version 904 to 998 |
| Data file size in backup | 2,150,825,984 bytes |
| Logical files | `JobFlowDemo` (data), `JobFlowDemo_log` (log) |
| User schema | `dbo` only |

## Object counts

| Object | Count |
|---|---|
| User tables | 229 |
| Columns | 2,732 |
| Primary keys | 224 (all single-column, all clustered) |
| Tables with no primary key (heaps) | 5: `ClientKPIReporting`, `JobDetails`, `JobFlowJobSummary`, `JobOperativeDetails`, `weekinfo` |
| Foreign keys | 63 (all `ON DELETE NO ACTION`, `ON UPDATE NO ACTION`) |
| Nonclustered indexes | 0 |
| Unique constraints other than primary keys | 0 |
| Check constraints | 0 |
| Default constraints | 443 |
| Views | 0 |
| Stored procedures | 0 |
| Functions | 0 |
| Triggers | 0 |
| Synonyms | 0 |
| Sequences | 0 |
| `sys.sql_modules` rows for user objects | 0 |

There is no database module that creates a job. JobFlow writes rows from the application. See `docs/jobflow-integration-map.md`.

## Indexes

Every primary key is the table's clustered index. No table has a nonclustered index, a unique constraint, or a filtered index. The five heaps listed above have no index at all.

## Foreign keys

| Constraint | Child table.column | Parent table.column |
|---|---|---|
| `FK__agreedpri__clien__79C80F94` | `agreedprices.clientcode` | `client.clientcode` |
| `FK__allocatio__jobno__45F365D3` | `allocation.jobno` | `orders.jobno` |
| `FK__allocation__idno__46E78A0C` | `allocation.idno` | `employee.idno` |
| `FK__allpartso__parts__681373AD` | `allpartsorder.partsorderno` | `partsorder.partsorderno` |
| `FK__assetfaul__jobno__3EDC53F0` | `assetfault.jobno` | `orders.jobno` |
| `FK__cenpartso__vorde__1881A0DE` | `cenpartsorderall.vorderno` | `vanpartsorder.vorderno` |
| `FK__clientlab__clien__575DE8F7` | `clientlabrate.clientcode` | `client.clientcode` |
| `FK__emaildone__idno__73501C2F` | `emaildone.idno` | `employee.idno` |
| `FK__emaildone__jobno__74444068` | `emaildone.jobno` | `orders.jobno` |
| `FK__emailsite__sitec__2B7F66B9` | `emailsite.sitecode` | `houselist.sitecode` |
| `FK__employeera__idno__2CF2ADDF` | `employeerate.idno` | `employee.idno` |
| `FK__engcale__idno__1F63A897` | `engcale.idno` | `employee.idno` |
| `FK__engcale__jobno__2057CCD0` | `engcale.jobno` | `orders.jobno` |
| `FK__gencojobt__jobno__4DD47EBD` | `gencojobtracker.jobno` | `orders.jobno` |
| `FK__hireplant__jobno__5D95E53A` | `hireplant.jobno` | `orders.jobno` |
| `FK__invoice__partsor__6DCC4D03` | `invoice.partsorderno` | `partsorder.partsorderno` |
| `FK__invoiceref__orno__23BE4960` | `invoiceref.orno` | `orderrefrigerant.orno` |
| `FK__invoiceva__vorde__370627FE` | `invoicevan.vorderno` | `vanpartsorder.vorderno` |
| `FK__jobfile__jobno__0C50D423` | `jobfile.jobno` | `orders.jobno` |
| `FK__jobform__idno__06ED0088` | `jobform.idno` | `employee.idno` |
| `FK__jobform__jobno__05F8DC4F` | `jobform.jobno` | `orders.jobno` |
| `FK__jobitem__jobno__3138400F` | `jobitem.jobno` | `orders.jobno` |
| `FK__jobitem__ourpno__30441BD6` | `jobitem.ourpno` | `materials.ourpno` |
| `FK__jobplan__idno__078C1F06` | `jobplan.idno` | `employee.idno` |
| `FK__jobplan__jobno__0697FACD` | `jobplan.jobno` | `orders.jobno` |
| `FK__jobworksh__jobno__3AA1AEB8` | `jobworksheet.jobno` | `orders.jobno` |
| `FK__jobworkshe__idno__3B95D2F1` | `jobworksheet.idno` | `employee.idno` |
| `FK__manageren__manid__68687968` | `managereng.manid` | `supmanager.manid` |
| `FK__managereng__idno__695C9DA1` | `managereng.idno` | `employee.idno` |
| `FK__managersi__manid__61BB7BD9` | `managersite.manid` | `supmanager.manid` |
| `FK__managersi__sitec__62AFA012` | `managersite.sitecode` | `houselist.sitecode` |
| `FK__managersite__tid__63A3C44B` | `managersite.tid` | `team.tid` |
| `FK__measureca__jobno__503BEA1C` | `measurecalc.jobno` | `orders.jobno` |
| `FK__measurefi__jobno__567ED357` | `measurefile.jobno` | `orders.jobno` |
| `FK__mslive__jobno__50E5F592` | `mslive.jobno` | `orders.jobno` |
| `FK__mssign__jobno__53C2623D` | `mssign.jobno` | `orders.jobno` |
| `FK__ooheng__idno__6B64E1A4` | `ooheng.idno` | `employee.idno` |
| `FK__operative__idno__71D1E811` | `operative.idno` | `employee.idno` |
| `FK__operative__sscod__72C60C4A` | `operative.sscode` | `subsection.sscode` |
| `FK__orderpart__jobno__46B27FE2` | `orderparts.jobno` | `orders.jobno` |
| `FK__orderpart__ourpn__47A6A41B` | `orderparts.ourpno` | `materials.ourpno` |
| `FK__orders__clientco__117F9D94` | `orders.clientcode` | `client.clientcode` |
| `FK__orders__sitecode__1273C1CD` | `orders.sitecode` | `houselist.sitecode` |
| `FK__otherform__idno__74CE504D` | `otherform.idno` | `employee.idno` |
| `FK__otherform__jobno__73DA2C14` | `otherform.jobno` | `orders.jobno` |
| `FK__paymentva__jobno__05D8E0BE` | `paymentvalue.jobno` | `orders.jobno` |
| `FK__qdmeasure__jobno__19AACF41` | `qdmeasure.jobno` | `orders.jobno` |
| `FK__quotetemp__oqtno__1BE81D6E` | `quotetemplate.oqtno` | `orderquotetemplate.oqtno` |
| `FK__selectsec__jobno__778AC167` | `selectsection.jobno` | `orders.jobno` |
| `FK__selectsec__sscod__787EE5A0` | `selectsection.sscode` | `subsection.sscode` |
| `FK__stockrefri__idno__3CBF0154` | `stockrefrigerant.idno` | `employee.idno` |
| `FK__team__idno__592635D8` | `team.idno` | `employee.idno` |
| `FK__teameng__idno__6F1576F7` | `teameng.idno` | `employee.idno` |
| `FK__teameng__tid__6E2152BE` | `teameng.tid` | `team.tid` |
| `FK__tomtomord__jobno__603D47BB` | `tomtomorder.jobno` | `orders.jobno` |
| `FK__tomtomorde__idno__61316BF4` | `tomtomorder.idno` | `employee.idno` |
| `FK__usemateri__jobno__2739D489` | `usematerials.jobno` | `orders.jobno` |
| `FK__usemateri__ourpn__282DF8C2` | `usematerials.ourpno` | `materials.ourpno` |
| `FK__userefrig__cfkey__4830B400` | `userefrigerant.cfkey` | `centralrefrigerant.cfkey` |
| `FK__userefrig__jobno__46486B8E` | `userefrigerant.jobno` | `orders.jobno` |
| `FK__userefrig__srefk__473C8FC7` | `userefrigerant.srefkey` | `stockrefrigerant.srefkey` |
| `FK__userefrige__idno__45544755` | `userefrigerant.idno` | `employee.idno` |
| `FK__vanpartso__vorde__7CD98669` | `vanpartsorderall.vorderno` | `vanpartsorder.vorderno` |

`orders.jobno` is the job key. It is referenced by `allocation`, `assetfault`, `emaildone`, `engcale`, `gencojobtracker`, `hireplant`, `jobfile`, `jobform`, `jobitem`, `jobplan`, `jobworksheet`, `measurecalc`, `measurefile`, `mslive`, `mssign`, `orderparts`, `paymentvalue`, `qdmeasure`, `selectsection`, `tomtomorder`, `usematerials` and `userefrigerant`.

`orders.clientcode` references `client.clientcode`. `orders.sitecode` references `houselist.sitecode`. `houselist.clientcode` is not a foreign key, but every site row matches a client and every job's site belongs to that job's client (0 orphans, 0 mismatches in this backup).

`employee.idno` is the person key. It is referenced by `allocation`, `emaildone`, `employeerate`, `engcale`, `jobform`, `jobplan`, `jobworksheet`, `managereng`, `ooheng`, `operative`, `otherform`, `stockrefrigerant`, `team`, `teameng`, `tomtomorder` and `userefrigerant`.

`selectsection` links a job to a subsection (`selectsection.sscode` to `subsection.sscode`). `subsection.scode` matches `section.scode` in the data, except `subsection` row `sscode='18'` (`ssname='Projects'`), whose `scode` is blank. That match is not a foreign key.

These job-related columns are **not** foreign keys: `orders.categorycode`, `orders.jobtype`, `orders.username`, `orders.pmanager`, `orders.pcodinator`, `status.jobno`, `userlog.jobno`, `anidlog.jobno`, `anidlog.jpkey`, `houselist.clientcode`.

## Tables

Row counts are from `sys.partitions` (clustered index or heap), not a table scan. Identity seed and increment in each table section are the column property, not the current value.

`IDENT_CURRENT` at inspection: `orders.jobno` 15917 (max `jobno` in the table is also 15917), `employee.idno` 58, `allocation.aekey` 29098, `jobplan.jpkey` 33491, `selectsection.sskey` 16784, `client.cnum` 1088, `userlog.ulkey` 199836, `operative.okey` 170, `ooheng.okey` 1. `ooheng` has 0 rows, so that identity value does not represent a stored engineer.

### dbo.accesslist

Rows: 0

Primary key: `akey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `akey` | int | no | 1, 1 |  |
| 2 | `name` | varchar(500) | yes |  |  |

### dbo.actionlist

Rows: 0

Primary key: `alkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `alkey` | int | no | 1, 1 |  |
| 2 | `name` | varchar(500) | yes |  |  |
| 3 | `inshave` | int | yes |  | ('0') |
| 4 | `actype` | int | yes |  | ('0') |

### dbo.actionlistsup

Rows: 0

Primary key: `alkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `alkey` | int | no | 1, 1 |  |
| 2 | `name` | varchar(500) | yes |  |  |
| 3 | `inshave` | int | yes |  | ('0') |
| 4 | `actype` | int | yes |  | ('0') |

### dbo.admincodes

Rows: 0

Primary key: `adckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `adckey` | int | no | 1, 1 |  |
| 2 | `codedes` | varchar(250) | yes |  |  |

### dbo.agreedprices

Rows: 0

Primary key: `apkey` (clustered).

Foreign keys out:

- `clientcode` to `client.clientcode` (`FK__agreedpri__clien__79C80F94`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `apkey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `clientcode` | varchar(50) | yes |  |  |
| 4 | `nsprice` | decimal(16,2) | yes |  |  |

### dbo.allcerts

Rows: 0

Primary key: `cetkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cetkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `certype` | int | yes |  |  |
| 4 | `idate` | datetime | yes |  |  |
| 5 | `cno` | varchar(200) | yes |  |  |
| 6 | `expdate` | datetime | yes |  |  |

### dbo.allocation

Rows: 26870

Primary key: `aekey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__allocatio__jobno__45F365D3`)
- `idno` to `employee.idno` (`FK__allocation__idno__46E78A0C`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `aekey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `arrivaltime` | datetime | yes |  |  |
| 5 | `departuretime` | datetime | yes |  |  |
| 6 | `fsttimefix` | tinyint | yes |  |  |
| 7 | `comments` | text | yes |  |  |
| 8 | `username` | varchar(30) | yes |  |  |
| 9 | `path` | varchar(250) | yes |  |  |
| 10 | `fpath` | varchar(250) | yes |  |  |
| 11 | `cpath1` | varchar(250) | yes |  |  |
| 12 | `cpath2` | varchar(250) | yes |  |  |
| 13 | `cpath3` | varchar(250) | yes |  |  |
| 14 | `cpath4` | varchar(250) | yes |  |  |
| 15 | `cpath5` | varchar(250) | yes |  |  |
| 16 | `cpath6` | varchar(250) | yes |  |  |
| 17 | `cpath7` | varchar(250) | yes |  |  |
| 18 | `cpath8` | varchar(250) | yes |  |  |
| 19 | `cpath9` | varchar(250) | yes |  |  |
| 20 | `cpath10` | varchar(250) | yes |  |  |
| 21 | `totaltime` | int | yes |  |  |
| 22 | `trtime` | int | yes |  |  |
| 23 | `pptime` | datetime | yes |  |  |
| 24 | `opay` | decimal(16,2) | yes |  |  |
| 25 | `parking` | decimal(16,2) | yes |  |  |
| 26 | `oexp` | decimal(16,2) | yes |  |  |
| 27 | `mileage` | decimal(16,2) | yes |  |  |
| 28 | `sb` | tinyint | yes |  | ('0') |
| 29 | `sdatetime` | datetime | yes |  |  |
| 30 | `offsitecode` | int | yes |  |  |
| 31 | `etype` | tinyint | yes |  | ('0') |
| 32 | `tbform` | image | yes |  |  |
| 33 | `tbsign` | image | yes |  |  |
| 34 | `tbsignonly` | image | yes |  |  |
| 35 | `postx` | varchar(50) | yes |  |  |
| 36 | `posty` | varchar(50) | yes |  |  |
| 37 | `phcheck` | tinyint | yes |  | ('0') |
| 38 | `ppmform` | tinyint | yes |  | ('0') |
| 39 | `raform1` | image | yes |  |  |
| 40 | `raform2` | image | yes |  |  |
| 41 | `raform3` | image | yes |  |  |
| 42 | `raform4` | image | yes |  |  |
| 43 | `asummary` | varchar(100) | yes |  |  |
| 44 | `chequecoll` | varchar(50) | yes |  |  |
| 45 | `cashcoll` | varchar(50) | yes |  |  |
| 46 | `chass` | tinyint | no |  | ('0') |
| 47 | `assid` | int | yes |  |  |
| 48 | `ltaken` | tinyint | no |  | ('0') |
| 49 | `chasstype` | tinyint | no |  | ('1') |
| 50 | `pwform1` | image | yes |  |  |
| 51 | `pwform2` | image | yes |  |  |
| 52 | `pwform3` | image | yes |  |  |
| 53 | `qform` | tinyint | yes |  | ('0') |
| 54 | `tempform` | image | yes |  |  |
| 55 | `tbftype` | tinyint | yes |  | ('0') |
| 56 | `pwform4` | image | yes |  |  |
| 57 | `pwform5` | image | yes |  |  |
| 58 | `tbcsform` | image | yes |  |  |
| 59 | `asbyes` | varchar(20) | yes |  |  |
| 60 | `asbyes1` | varchar(20) | yes |  |  |
| 61 | `asbyes2` | varchar(20) | yes |  |  |
| 62 | `fcdone` | tinyint | yes |  | ('0') |
| 63 | `cpath` | varchar(250) | yes |  |  |
| 64 | `rspath` | varchar(250) | yes |  |  |
| 65 | `rspath2` | varchar(250) | yes |  |  |
| 66 | `rspath3` | varchar(250) | yes |  |  |
| 67 | `rspath4` | varchar(250) | yes |  |  |
| 68 | `rspath5` | varchar(250) | yes |  |  |
| 69 | `rscdone1` | tinyint | yes |  | ('0') |
| 70 | `rscdone2` | tinyint | yes |  | ('0') |
| 71 | `rscdone3` | tinyint | yes |  | ('0') |
| 72 | `rscdone4` | tinyint | yes |  | ('0') |
| 73 | `cpath11` | varchar(250) | yes |  |  |
| 74 | `cpath12` | varchar(250) | yes |  |  |
| 75 | `absent` | tinyint | yes |  | ('0') |
| 76 | `payhour` | decimal(16,2) | yes |  |  |
| 77 | `lat` | varchar(50) | yes |  |  |
| 78 | `long` | varchar(50) | yes |  |  |
| 79 | `endlat` | varchar(50) | yes |  |  |
| 80 | `endlong` | varchar(50) | yes |  |  |
| 81 | `jpkey` | varchar(50) | yes |  |  |
| 82 | `mapstime` | datetime | yes |  |  |
| 83 | `mapetime` | datetime | yes |  |  |
| 84 | `workdone` | tinyint | yes |  | ('0') |
| 85 | `needcsign` | tinyint | no |  | ('0') |
| 86 | `emailvehdef` | tinyint | no |  | ('0') |
| 87 | `smileage` | varchar(50) | yes |  |  |
| 88 | `slunch` | varchar(50) | yes |  |  |
| 89 | `cfname` | varchar(100) | yes |  |  |
| 90 | `selassetid` | varchar(2000) | yes |  |  |
| 91 | `travel` | varchar(100) | yes |  |  |
| 92 | `supvision` | varchar(500) | yes |  |  |
| 93 | `overtime` | varchar(100) | yes |  |  |
| 94 | `commentsin` | varchar(500) | yes |  |  |
| 95 | `fcsdone` | tinyint | yes |  | ('0') |
| 96 | `fcspath` | varchar(250) | yes |  |  |
| 97 | `noofeng` | varchar(10) | yes |  |  |
| 98 | `whours` | varchar(50) | yes |  |  |
| 99 | `nshift` | varchar(50) | yes |  |  |
| 100 | `jofftype` | tinyint | yes |  | ('0') |
| 101 | `afeekey` | varchar(50) | yes |  |  |
| 102 | `lcostkey` | varchar(50) | yes |  |  |
| 103 | `lotherkey` | varchar(50) | yes |  |  |
| 104 | `qfsend` | tinyint | yes |  | ('0') |
| 105 | `qfsenddate` | datetime | yes |  |  |
| 106 | `qfapprove` | tinyint | yes |  | ('0') |
| 107 | `qfapprovedate` | datetime | yes |  |  |
| 108 | `qfworkdone` | tinyint | yes |  | ('0') |
| 109 | `ramstatus` | varchar(50) | yes |  |  |
| 110 | `ramdrec` | datetime | yes |  |  |
| 111 | `ramdapp` | datetime | yes |  |  |
| 112 | `postatus` | varchar(50) | yes |  |  |
| 113 | `podsend` | datetime | yes |  |  |
| 114 | `apptimedone` | tinyint | no |  | ('0') |
| 115 | `appntimedc` | decimal(16,2) | yes |  |  |
| 116 | `appotimedc` | decimal(16,2) | yes |  |  |
| 117 | `normalhrs` | decimal(16,2) | yes |  |  |
| 118 | `oohhrs` | decimal(16,2) | yes |  |  |
| 119 | `sundaybhhrs` | decimal(16,2) | yes |  |  |
| 120 | `nsrate` | tinyint | no |  | ('0') |
| 121 | `oohsrate` | tinyint | no |  | ('0') |
| 122 | `sundaybhshift` | tinyint | no |  | ('0') |
| 123 | `oncallrate` | tinyint | no |  | ('0') |
| 124 | `sitemanager` | tinyint | no |  | ('0') |
| 125 | `oncallwendshift` | tinyint | no |  | ('0') |
| 126 | `oohsmanagerrate` | tinyint | no |  | ('0') |
| 127 | `special` | varchar(2000) | yes |  |  |
| 128 | `dboxdone` | tinyint | yes |  | ('0') |
| 129 | `pdffile` | varchar(200) | yes |  |  |

### dbo.allorderrefrigerant

Rows: 0

Primary key: `rfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rfkey` | int | no | 1, 1 |  |
| 2 | `orno` | int | yes |  |  |
| 3 | `odate` | datetime | yes |  |  |
| 4 | `sname` | varchar(200) | yes |  |  |
| 5 | `sno` | int | yes |  |  |
| 6 | `refrigtype` | varchar(250) | yes |  |  |
| 7 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 8 | `qty` | decimal(16,2) | yes |  |  |
| 9 | `rdate` | datetime | yes |  |  |
| 10 | `price` | decimal(16,2) | yes |  |  |
| 11 | `lprice` | decimal(16,2) | yes |  |  |
| 12 | `scoll` | decimal(16,2) | yes |  | ('0') |
| 13 | `otype` | tinyint | yes |  | ('0') |
| 14 | `vehreg` | varchar(50) | yes |  |  |
| 15 | `ourpno` | varchar(50) | yes |  |  |
| 16 | `ostatus` | varchar(50) | yes |  |  |
| 17 | `jobno` | int | yes |  |  |
| 18 | `idno` | int | yes |  |  |
| 19 | `cslno` | int | yes |  |  |

### dbo.allpartsorder

Rows: 7160

Primary key: `pkey` (clustered).

Foreign keys out:

- `partsorderno` to `partsorder.partsorderno` (`FK__allpartso__parts__681373AD`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `partsorderno` | int | yes |  |  |
| 3 | `ptype` | varchar(5) | yes |  |  |
| 4 | `ourpno` | varchar(50) | yes |  |  |
| 5 | `sname` | varchar(100) | yes |  |  |
| 6 | `pname` | text | yes |  |  |
| 7 | `qty` | decimal(16,2) | yes |  |  |
| 8 | `odate` | datetime | yes |  |  |
| 9 | `cdate` | datetime | yes |  |  |
| 10 | `rdate` | datetime | yes |  |  |
| 11 | `ostatus` | varchar(50) | yes |  |  |
| 12 | `comments` | text | yes |  |  |
| 13 | `price` | decimal(16,2) | yes |  |  |
| 14 | `lprice` | decimal(16,2) | yes |  |  |
| 15 | `approve` | tinyint | yes |  | ('0') |
| 16 | `approveby` | varchar(50) | yes |  |  |
| 17 | `pncode` | varchar(50) | yes |  |  |
| 18 | `oqno` | int | yes |  |  |
| 19 | `poref` | varchar(50) | yes |  |  |
| 20 | `cqty` | decimal(16,2) | yes |  |  |
| 21 | `grqty` | decimal(16,2) | yes |  |  |
| 22 | `qprice` | decimal(16,2) | yes |  |  |
| 23 | `rrpval` | decimal(16,2) | yes |  |  |
| 24 | `concurdone` | tinyint | yes |  | ('0') |
| 25 | `duetext` | varchar(50) | yes |  |  |
| 26 | `eqtypeno` | tinyint | yes |  | ('0') |
| 27 | `subcon` | tinyint | yes |  | ('0') |
| 28 | `qdmono` | int | yes |  |  |
| 29 | `comcodename` | varchar(200) | yes |  |  |
| 30 | `sorparts` | tinyint | yes |  | ('0') |
| 31 | `epdone` | tinyint | yes |  | ('0') |
| 32 | `epdate` | datetime | yes |  |  |
| 33 | `appdet` | tinyint | yes |  | ('0') |
| 34 | `appexport` | tinyint | yes |  | ('0') |
| 35 | `appdetuser` | varchar(100) | yes |  |  |
| 36 | `appdetdate` | datetime | yes |  |  |
| 37 | `appexportuser` | varchar(100) | yes |  |  |
| 38 | `appexportdate` | datetime | yes |  |  |
| 39 | `uploadexport` | tinyint | yes |  | ('0') |
| 40 | `uploadexportdate` | datetime | yes |  |  |
| 41 | `cancelpo` | tinyint | yes |  | ('0') |
| 42 | `requestor` | varchar(100) | yes |  |  |
| 43 | `thisoby` | varchar(50) | yes |  |  |
| 44 | `serviceadd` | int | yes |  | ('0') |
| 45 | `rfilename` | varchar(500) | yes |  |  |

### dbo.ameasureclientrrp

Rows: 0

Primary key: `amkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `amkey` | int | no | 1, 1 |  |
| 2 | `clientcode` | varchar(100) | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `rrp` | decimal(16,2) | yes |  |  |

### dbo.anidlog

Rows: 39933

Primary key: `ankey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ankey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `jpkey` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `dldate` | datetime | yes |  |  |

### dbo.appparts

Rows: 0

Primary key: `apkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `apkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `jpkey` | varchar(50) | yes |  |  |
| 5 | `pname` | varchar(500) | yes |  |  |
| 6 | `sname` | varchar(500) | yes |  |  |
| 7 | `tstock` | varchar(20) | yes |  |  |
| 8 | `tfloat` | varchar(20) | yes |  |  |
| 9 | `qty` | varchar(50) | yes |  |  |
| 10 | `cdate` | datetime | yes |  |  |

### dbo.apppartshire

Rows: 0

Primary key: `apkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `apkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `jpkey` | varchar(50) | yes |  |  |
| 5 | `pused` | varchar(500) | yes |  |  |
| 6 | `rotwoused` | varchar(500) | yes |  |  |
| 7 | `hpused` | varchar(500) | yes |  |  |
| 8 | `cdate` | datetime | yes |  |  |

### dbo.asset

Rows: 3182

Primary key: `assetc` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `assetc` | int | no | 1, 1 |  |
| 2 | `assetid` | varchar(50) | yes |  |  |
| 3 | `sitecode` | varchar(50) | yes |  |  |
| 4 | `assetno` | varchar(50) | yes |  |  |
| 5 | `equipment` | varchar(250) | yes |  |  |
| 6 | `manufacturer` | varchar(250) | yes |  |  |
| 7 | `model` | varchar(250) | yes |  |  |
| 8 | `serialno` | varchar(100) | yes |  |  |
| 9 | `yearmf` | datetime | yes |  |  |
| 10 | `instdate` | datetime | yes |  |  |
| 11 | `location` | varchar(200) | yes |  |  |
| 12 | `condition` | int | yes |  |  |
| 13 | `refrigerant` | varchar(250) | yes |  |  |
| 14 | `refval` | decimal(16,2) | yes |  |  |
| 15 | `comment` | text | yes |  |  |
| 16 | `path` | varchar(100) | yes |  |  |
| 17 | `fpath` | varchar(100) | yes |  |  |
| 18 | `cin` | tinyint | yes |  | ('1') |
| 19 | `wflag` | tinyint | yes |  | ('0') |
| 20 | `wedate` | datetime | yes |  |  |
| 21 | `strunit` | varchar(50) | yes |  |  |
| 22 | `svpyear` | varchar(50) | yes |  |  |
| 23 | `dofvisit` | varchar(50) | yes |  |  |
| 24 | `mofvisit` | varchar(50) | yes |  |  |
| 25 | `asstkey` | int | yes |  |  |
| 26 | `asstlastupdate` | datetime | yes |  |  |

### dbo.assetfault

Rows: 3059

Primary key: `afkey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__assetfaul__jobno__3EDC53F0`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `afkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `eqname` | varchar(250) | yes |  |  |
| 4 | `fname` | varchar(250) | yes |  |  |
| 5 | `assetc` | int | yes |  |  |
| 6 | `assetid` | varchar(50) | yes |  |  |
| 7 | `subassetc` | int | yes |  |  |
| 8 | `subassetfault` | tinyint | yes |  | ('0') |
| 9 | `assetvdone` | tinyint | yes |  | ('0') |
| 10 | `hidethis` | tinyint | yes |  | ('0') |

### dbo.assetlocation

Rows: 115

Primary key: `alno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `alno` | int | no | 1, 1 |  |
| 2 | `lname` | varchar(250) | yes |  |  |

### dbo.assetref

Rows: 0

Primary key: `arkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `arkey` | int | no | 1, 1 |  |
| 2 | `adddate` | datetime | yes |  |  |
| 3 | `assetc` | int | yes |  |  |
| 4 | `assetid` | varchar(50) | yes |  |  |
| 5 | `refrigsno` | varchar(250) | yes |  |  |
| 6 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 7 | `ourpno` | varchar(50) | yes |  |  |
| 8 | `jobno` | int | yes |  |  |

### dbo.assettype

Rows: 0

Primary key: `asstkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `asstkey` | int | no | 1, 1 |  |
| 2 | `asstype` | varchar(200) | yes |  |  |

### dbo.atmcausetype

Rows: 0

Primary key: `atkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `atkey` | int | no | 1, 1 |  |
| 2 | `atmcid` | varchar(50) | yes |  |  |
| 3 | `atmcdes` | varchar(200) | yes |  |  |

### dbo.atmerror

Rows: 0

Primary key: `atkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `atkey` | int | no | 1, 1 |  |
| 2 | `atmerrtype` | varchar(10) | yes |  |  |
| 3 | `atmorderno` | varchar(200) | yes |  |  |
| 4 | `atmdes` | varchar(200) | yes |  |  |
| 5 | `atmerrdate` | datetime | yes |  |  |

### dbo.atmrepairtype

Rows: 0

Primary key: `atkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `atkey` | int | no | 1, 1 |  |
| 2 | `atmrid` | varchar(50) | yes |  |  |
| 3 | `atmrdes` | varchar(200) | yes |  |  |

### dbo.atriumsetting

Rows: 0

Primary key: `atkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `atkey` | int | no | 1, 1 |  |
| 2 | `aturl` | varchar(200) | yes |  |  |
| 3 | `atuser` | varchar(50) | yes |  |  |
| 4 | `atpwd` | varchar(50) | yes |  |  |
| 5 | `aturlkey` | varchar(200) | yes |  |  |
| 6 | `atmsscode` | varchar(20) | yes |  |  |
| 7 | `clientcode` | varchar(50) | yes |  |  |

### dbo.bankholidays

Rows: 25

Primary key: `bkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `bkey` | int | no | 1, 1 |  |
| 2 | `bhday` | datetime | yes |  |  |

### dbo.budgetsheet

Rows: 0

Primary key: `bkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `bkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `details` | text | yes |  |  |
| 4 | `destype` | int | yes |  |  |
| 5 | `bamount` | decimal(16,2) | yes |  |  |
| 6 | `cdate` | datetime | yes |  |  |
| 7 | `adddate` | datetime | yes |  |  |
| 8 | `description` | text | yes |  |  |

### dbo.category

Rows: 5

Primary key: `categorycode` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `categorycode` | int | no |  |  |
| 2 | `category` | int | yes |  |  |
| 3 | `categoryname` | varchar(50) | yes |  |  |

### dbo.cenpartsinout

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `cslno` | int | yes |  | ('0') |
| 4 | `qty` | int | yes |  |  |
| 5 | `pdate` | datetime | yes |  |  |
| 6 | `partsin` | int | yes |  | ('0') |
| 7 | `pfrom` | varchar(100) | yes |  |  |
| 8 | `pctype` | int | yes |  | ('0') |
| 9 | `comment` | text | yes |  |  |

### dbo.cenpartsorder

Rows: 0

Primary key: `vorderno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vorderno` | int | no | 1, 1 |  |
| 2 | `odate` | datetime | yes |  |  |
| 3 | `sname` | varchar(100) | yes |  |  |

### dbo.cenpartsorderall

Rows: 0

Primary key: `pkey` (clustered).

Foreign keys out:

- `vorderno` to `vanpartsorder.vorderno` (`FK__cenpartso__vorde__1881A0DE`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `vorderno` | int | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `cslno` | int | yes |  | ('0') |
| 5 | `qty` | int | yes |  |  |
| 6 | `odate` | datetime | yes |  |  |
| 7 | `rdate` | datetime | yes |  |  |
| 8 | `scollected` | int | yes |  | ('0') |
| 9 | `pcode` | decimal(16,4) | yes |  |  |
| 10 | `pcost` | decimal(16,4) | yes |  |  |
| 11 | `comcodename` | varchar(200) | yes |  |  |
| 12 | `appdet` | tinyint | yes |  | ('0') |
| 13 | `appexport` | tinyint | yes |  | ('0') |
| 14 | `appdetuser` | varchar(100) | yes |  |  |
| 15 | `appdetdate` | datetime | yes |  |  |
| 16 | `appexportuser` | varchar(100) | yes |  |  |
| 17 | `appexportdate` | datetime | yes |  |  |
| 18 | `uploadexport` | tinyint | yes |  | ('0') |
| 19 | `uploadexportdate` | datetime | yes |  |  |
| 20 | `cancelpo` | tinyint | yes |  | ('0') |
| 21 | `deliveryparts` | tinyint | yes |  | ('0') |
| 22 | `epdone` | tinyint | yes |  | ('0') |
| 23 | `epdate` | datetime | yes |  |  |
| 24 | `deliverydate` | datetime | yes |  |  |

### dbo.centralrefrigerant

Rows: 0

Primary key: `cfkey` (clustered).

Referenced by:

- `userefrigerant.cfkey` (`FK__userefrig__cfkey__4830B400`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cfkey` | int | no | 1, 1 |  |
| 2 | `adddate` | datetime | yes |  |  |
| 3 | `refrigtype` | varchar(250) | yes |  |  |
| 4 | `refrigsno` | varchar(250) | yes |  |  |
| 5 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 6 | `cslno` | int | yes |  |  |
| 7 | `rfkey` | int | yes |  |  |
| 8 | `ourpno` | varchar(50) | yes |  |  |
| 9 | `jobno` | int | yes |  |  |

### dbo.centralstock

Rows: 0

Primary key: `cskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cskey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `quantity` | int | yes |  |  |
| 4 | `cslno` | int | yes |  | ('0') |
| 5 | `csadd` | datetime | yes |  |  |
| 6 | `vsadd` | datetime | yes |  |  |
| 7 | `csluse` | datetime | yes |  |  |
| 8 | `rsl` | int | yes |  |  |
| 9 | `minrsl` | int | yes |  |  |

### dbo.centralstockcost

Rows: 0

Primary key: `csckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `csckey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `quantity` | decimal(18,2) | yes |  |  |
| 4 | `cslno` | int | yes |  | ('0') |
| 5 | `csadd` | datetime | yes |  |  |
| 6 | `vsadd` | datetime | yes |  |  |
| 7 | `csluse` | datetime | yes |  |  |
| 8 | `cskey` | int | yes |  |  |
| 9 | `cost` | decimal(16,2) | yes |  |  |
| 10 | `vorderno` | int | yes |  |  |
| 11 | `odate` | datetime | yes |  |  |
| 12 | `rdate` | datetime | yes |  |  |
| 13 | `scollected` | int | yes |  | ('0') |

### dbo.centralstocklocation

Rows: 1

Primary key: `cslno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cslno` | int | no | 0, 1 |  |
| 2 | `cslname` | varchar(250) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |
| 4 | `locaddress` | text | yes |  |  |
| 5 | `loccontact` | varchar(500) | yes |  |  |

### dbo.certstatus

Rows: 0

Primary key: `cskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cskey` | int | no | 1, 1 |  |
| 2 | `cstatus` | varchar(250) | yes |  |  |

### dbo.certstype

Rows: 5

Primary key: `certype` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `certype` | int | no | 1, 1 |  |
| 2 | `cername` | varchar(250) | yes |  |  |

### dbo.cetfile

Rows: 0

Primary key: `cfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cfkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `aekey` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `fullpath` | varchar(500) | yes |  |  |
| 7 | `pathonly` | varchar(500) | yes |  |  |
| 8 | `cetfname` | varchar(100) | yes |  |  |

### dbo.cformlist

Rows: 0

Primary key: `clkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `clkey` | int | no | 1, 1 |  |
| 2 | `cfname` | varchar(500) | yes |  |  |
| 3 | `qform` | tinyint | yes |  | ('0') |

### dbo.client

Rows: 656

Primary key: `clientcode` (clustered).

Referenced by:

- `agreedprices.clientcode` (`FK__agreedpri__clien__79C80F94`)
- `clientlabrate.clientcode` (`FK__clientlab__clien__575DE8F7`)
- `orders.clientcode` (`FK__orders__clientco__117F9D94`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `clientcode` | varchar(50) | no |  |  |
| 2 | `client` | varchar(100) | yes |  |  |
| 3 | `address1` | varchar(100) | yes |  |  |
| 4 | `town` | varchar(50) | yes |  |  |
| 5 | `county` | varchar(50) | yes |  |  |
| 6 | `postcode` | varchar(30) | yes |  |  |
| 7 | `tel` | varchar(30) | yes |  |  |
| 8 | `sagesc` | varchar(50) | yes |  |  |
| 9 | `pncode` | varchar(50) | yes |  |  |
| 10 | `fax` | varchar(30) | yes |  |  |
| 11 | `password` | varchar(50) | yes |  |  |
| 12 | `cemail` | varchar(100) | yes |  |  |
| 13 | `hal` | tinyint | yes |  | ('0') |
| 14 | `alvalue` | decimal(16,2) | yes |  | ('0') |
| 15 | `afee` | decimal(16,2) | yes |  |  |
| 16 | `lmarkup` | decimal(16,2) | yes |  |  |
| 17 | `pmarkup` | decimal(16,2) | yes |  |  |
| 18 | `cmarkup` | decimal(16,2) | yes |  |  |
| 19 | `asreg` | varchar(200) | yes |  |  |
| 20 | `riass` | varchar(200) | yes |  |  |
| 21 | `mest` | varchar(200) | yes |  |  |
| 22 | `nid` | tinyint | yes |  | ('0') |
| 23 | `nidtype` | varchar(100) | yes |  |  |
| 24 | `crb` | tinyint | yes |  | ('0') |
| 25 | `dpno` | varchar(50) | yes |  |  |
| 26 | `caccno` | varchar(50) | yes |  |  |
| 27 | `manualinvoice` | tinyint | yes |  | ('0') |
| 28 | `dcode` | varchar(20) | yes |  |  |
| 29 | `cd` | tinyint | yes |  | ('1') |
| 30 | `paytype` | tinyint | yes |  | ('0') |
| 31 | `hrate` | decimal(16,2) | yes |  |  |
| 32 | `oafee` | decimal(16,2) | yes |  |  |
| 33 | `hrateam` | decimal(16,2) | yes |  |  |
| 34 | `hratepm` | decimal(16,2) | yes |  |  |
| 35 | `cnum` | int | no | 100, 1 |  |
| 36 | `cmtno` | int | yes |  |  |
| 37 | `ivonly` | tinyint | yes |  | ('0') |
| 38 | `newsletter` | tinyint | yes |  | ('0') |
| 39 | `xmascard` | tinyint | yes |  | ('0') |
| 40 | `xmasgift` | tinyint | yes |  | ('0') |
| 41 | `xmasdes` | text | yes |  |  |
| 42 | `sssdate` | datetime | yes |  |  |
| 43 | `ssrdate` | datetime | yes |  |  |
| 44 | `cexpdate` | datetime | yes |  |  |
| 45 | `ccontact` | varchar(50) | yes |  |  |
| 46 | `ccontacttitle` | varchar(50) | yes |  |  |
| 47 | `jlono` | int | yes |  | ('0') |
| 48 | `ftype` | int | yes |  | ('0') |
| 49 | `raftype1` | int | yes |  | ('0') |
| 50 | `raftype2` | int | yes |  | ('0') |
| 51 | `tcname` | varchar(200) | yes |  |  |
| 52 | `ccharge` | varchar(500) | yes |  |  |
| 53 | `pdfformno` | int | yes |  | ('0') |
| 54 | `dcvat` | varchar(50) | yes |  |  |
| 55 | `optout` | tinyint | yes |  | ('0') |
| 56 | `sotcname` | varchar(50) | yes |  |  |
| 57 | `ritehiteinv` | tinyint | no |  | ('0') |
| 58 | `cislgross` | varchar(50) | yes |  |  |
| 59 | `adduname` | varchar(50) | yes |  |  |
| 60 | `updateuname` | varchar(50) | yes |  |  |
| 61 | `payterms` | int | yes |  |  |
| 62 | `cmanager` | varchar(100) | yes |  |  |
| 63 | `amanager` | varchar(100) | yes |  |  |
| 64 | `tadmins` | varchar(100) | yes |  |  |
| 65 | `info` | text | yes |  |  |

### dbo.clientactionlist

Rows: 0

Primary key: `ckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ckey` | int | no | 1, 1 |  |
| 2 | `alkey` | int | yes |  |  |
| 3 | `clientcode` | varchar(50) | yes |  |  |
| 4 | `actype` | int | yes |  | ('0') |

### dbo.ClientKPIReporting

Rows: 0

Primary key: none. Heap, no indexes.

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jobno` | int | yes |  |  |
| 2 | `Month` | varchar(30) | yes |  |  |
| 3 | `OrderDate` | datetime | yes |  |  |
| 4 | `ClientName` | varchar(255) | yes |  |  |
| 5 | `FirstVisited` | datetime | yes |  |  |
| 6 | `SLACategory` | int | yes |  |  |
| 7 | `QuoteDone` | int | yes |  |  |
| 8 | `A-C-R` | int | yes |  |  |
| 9 | `QuoteOnly` | int | yes |  |  |
| 10 | `FirstTimeFix` | int | yes |  |  |
| 11 | `JobType` | varchar(30) | yes |  |  |
| 12 | `Markup` | decimal(10,2) | yes |  |  |
| 13 | `ContractValue` | decimal(10,2) | yes |  |  |
| 14 | `SLACategoryCode` | int | yes |  |  |
| 15 | `OffSiteCode` | int | yes |  |  |
| 16 | `Measured` | int | yes |  |  |
| 17 | `SectionComplete` | int | yes |  |  |
| 18 | `Subsection` | varchar(50) | yes |  |  |
| 19 | `Section` | varchar(50) | yes |  |  |
| 20 | `Site` | varchar(255) | yes |  |  |
| 21 | `Town` | varchar(50) | yes |  |  |
| 22 | `PostCode` | varchar(30) | yes |  |  |
| 23 | `PostArea` | varchar(30) | yes |  |  |
| 24 | `DaystoComplete` | int | yes |  |  |
| 25 | `Engineers` | int | yes |  |  |
| 26 | `QuoteAccepted` | int | yes |  |  |
| 27 | `Quoted` | int | yes |  |  |
| 28 | `JobStatus` | varchar(50) | yes |  |  |

### dbo.clientlabrate

Rows: 0

Primary key: `clkey` (clustered).

Foreign keys out:

- `clientcode` to `client.clientcode` (`FK__clientlab__clien__575DE8F7`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `clkey` | int | no | 1, 1 |  |
| 2 | `clientcode` | varchar(50) | yes |  |  |
| 3 | `engih` | decimal(16,2) | yes |  |  |
| 4 | `engoh` | decimal(16,2) | yes |  |  |
| 5 | `eleih` | decimal(16,2) | yes |  |  |
| 6 | `eleoh` | decimal(16,2) | yes |  |  |
| 7 | `plumih` | decimal(16,2) | yes |  |  |
| 8 | `plumoh` | decimal(16,2) | yes |  |  |
| 9 | `callout` | decimal(16,2) | yes |  |  |

### dbo.clientlog

Rows: 0

Primary key: `ulkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ulkey` | int | no | 1, 1 |  |
| 2 | `lcdate` | datetime | yes |  |  |
| 3 | `clientcode` | varchar(50) | yes |  |  |
| 4 | `sitecode` | varchar(50) | yes |  |  |
| 5 | `info` | varchar(500) | yes |  |  |
| 6 | `herr` | tinyint | yes |  | ('0') |

### dbo.clientrulerate

Rows: 8

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `daynum` | int | yes |  |  |
| 3 | `s1_start` | datetime | yes |  |  |
| 4 | `s1_end` | datetime | yes |  |  |
| 5 | `s1_rule` | varchar(10) | yes |  |  |
| 6 | `s1_rate` | decimal(16,2) | yes |  |  |
| 7 | `s2_start` | datetime | yes |  |  |
| 8 | `s2_end` | datetime | yes |  |  |
| 9 | `s2_rule` | varchar(10) | yes |  |  |
| 10 | `s2_rate` | decimal(16,2) | yes |  |  |
| 11 | `s3_start` | datetime | yes |  |  |
| 12 | `s3_end` | datetime | yes |  |  |
| 13 | `s3_rule` | varchar(10) | yes |  |  |
| 14 | `s3_rate` | decimal(16,2) | yes |  |  |
| 15 | `s4_start` | datetime | yes |  |  |
| 16 | `s4_end` | datetime | yes |  |  |
| 17 | `s4_rule` | varchar(10) | yes |  |  |
| 18 | `s4_rate` | decimal(16,2) | yes |  |  |
| 19 | `jlono` | int | yes |  | ('0') |

### dbo.clienttype

Rows: 0

Primary key: `cmtno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cmtno` | int | no | 1, 1 |  |
| 2 | `ctname` | varchar(200) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.commoditylist

Rows: 0

Primary key: `clkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `clkey` | int | no | 1, 1 |  |
| 2 | `comcodename` | varchar(200) | yes |  |  |
| 3 | `comtype` | tinyint | yes |  | ('0') |
| 4 | `supppcode` | varchar(200) | yes |  |  |

### dbo.commoditysupcodelink

Rows: 0

Primary key: `ckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ckey` | int | no | 1, 1 |  |
| 2 | `supppcode` | varchar(200) | yes |  |  |
| 3 | `comcodename` | varchar(200) | yes |  |  |
| 4 | `supno` | varchar(10) | yes |  |  |

### dbo.conceptlist

Rows: 0

Primary key: `clkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `clkey` | int | no | 1, 1 |  |
| 2 | `cnname` | varchar(500) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.concretemeatrix

Rows: 0

Primary key: `cmkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cmkey` | int | no | 1, 1 |  |
| 2 | `wcoper` | varchar(10) | yes |  |  |
| 3 | `wcass` | varchar(10) | yes |  |  |
| 4 | `idrill` | varchar(10) | yes |  |  |
| 5 | `adrill` | varchar(10) | yes |  |  |
| 6 | `aswrig` | varchar(10) | yes |  |  |
| 7 | `hlwork` | varchar(10) | yes |  |  |

### dbo.contracttype

Rows: 2

Primary key: `ctkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ctkey` | int | no | 1, 1 |  |
| 2 | `ctname` | varchar(100) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.copydboxsetting

Rows: 0

Primary key: `dkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `dkey` | int | no | 1, 1 |  |
| 2 | `sfpath` | varchar(2000) | yes |  |  |
| 3 | `desfpath` | varchar(2000) | yes |  |  |
| 4 | `sdate` | datetime | yes |  |  |

### dbo.costheadlist

Rows: 0

Primary key: `echkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `echkey` | int | no | 1, 1 |  |
| 2 | `costhead` | varchar(200) | yes |  |  |

### dbo.discounts

Rows: 0

Primary key: `dckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `dckey` | int | no | 1, 1 |  |
| 2 | `pssid` | int | yes |  |  |
| 3 | `dcode` | varchar(20) | yes |  |  |
| 4 | `dcount` | decimal(16,2) | yes |  |  |

### dbo.displaywdes

Rows: 1

Primary key: `fid` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fid` | int | no | 1, 1 |  |
| 2 | `name` | varchar(500) | yes |  |  |

### dbo.dmsllic

Rows: 0

Primary key: `ckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ckey` | int | no | 1, 1 |  |
| 2 | `rcname` | varchar(250) | yes |  |  |

### dbo.dpparts

Rows: 0

Primary key: `dpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `dpkey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |

### dbo.ecategorylist

Rows: 0

Primary key: `eckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eckey` | int | no | 1, 1 |  |
| 2 | `ecategory` | varchar(200) | yes |  |  |

### dbo.ecompanylist

Rows: 0

Primary key: `eckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eckey` | int | no | 1, 1 |  |
| 2 | `ecompany` | varchar(200) | yes |  |  |

### dbo.editformlist

Rows: 1

Primary key: `efkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `efkey` | int | no | 1, 1 |  |
| 2 | `efname` | varchar(200) | yes |  |  |
| 3 | `efno` | varchar(10) | yes |  |  |

### dbo.emaildone

Rows: 2328

Primary key: `ekey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__emaildone__idno__73501C2F`)
- `jobno` to `orders.jobno` (`FK__emaildone__jobno__74444068`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `jstime` | datetime | yes |  |  |
| 4 | `idno` | int | yes |  |  |
| 5 | `jobno` | int | yes |  |  |
| 6 | `suser` | varchar(150) | yes |  |  |

### dbo.emailsite

Rows: 0

Primary key: `eskey` (clustered).

Foreign keys out:

- `sitecode` to `houselist.sitecode` (`FK__emailsite__sitec__2B7F66B9`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eskey` | int | no | 1, 1 |  |
| 2 | `sitecode` | varchar(50) | yes |  |  |
| 3 | `semail` | varchar(200) | yes |  |  |

### dbo.employee

Rows: 51

Primary key: `idno` (clustered).

Referenced by:

- `allocation.idno` (`FK__allocation__idno__46E78A0C`)
- `emaildone.idno` (`FK__emaildone__idno__73501C2F`)
- `employeerate.idno` (`FK__employeera__idno__2CF2ADDF`)
- `engcale.idno` (`FK__engcale__idno__1F63A897`)
- `jobform.idno` (`FK__jobform__idno__06ED0088`)
- `jobplan.idno` (`FK__jobplan__idno__078C1F06`)
- `jobworksheet.idno` (`FK__jobworkshe__idno__3B95D2F1`)
- `managereng.idno` (`FK__managereng__idno__695C9DA1`)
- `ooheng.idno` (`FK__ooheng__idno__6B64E1A4`)
- `operative.idno` (`FK__operative__idno__71D1E811`)
- `otherform.idno` (`FK__otherform__idno__74CE504D`)
- `stockrefrigerant.idno` (`FK__stockrefri__idno__3CBF0154`)
- `team.idno` (`FK__team__idno__592635D8`)
- `teameng.idno` (`FK__teameng__idno__6F1576F7`)
- `tomtomorder.idno` (`FK__tomtomorde__idno__61316BF4`)
- `userefrigerant.idno` (`FK__userefrige__idno__45544755`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `idno` | int | no | 1, 1 |  |
| 2 | `name` | varchar(100) | yes |  |  |
| 3 | `phone` | varchar(50) | yes |  |  |
| 4 | `vehreg` | varchar(100) | yes |  |  |
| 5 | `ce` | tinyint | yes |  | ('1') |
| 6 | `contractor` | tinyint | yes |  | ('0') |
| 7 | `hins` | tinyint | yes |  | ('0') |
| 8 | `plivalue` | decimal(16,2) | yes |  | ('0') |
| 9 | `pliedate` | datetime | yes |  |  |
| 10 | `address` | varchar(250) | yes |  |  |
| 11 | `town` | varchar(250) | yes |  |  |
| 12 | `county` | varchar(250) | yes |  |  |
| 13 | `pcode` | varchar(100) | yes |  |  |
| 14 | `tele` | varchar(50) | yes |  |  |
| 15 | `fax` | varchar(50) | yes |  |  |
| 16 | `email` | varchar(100) | yes |  |  |
| 17 | `tomono` | varchar(100) | yes |  |  |
| 18 | `jlono` | int | yes |  |  |
| 19 | `qinfo` | text | yes |  |  |
| 20 | `edob` | datetime | yes |  |  |
| 21 | `esdate` | datetime | yes |  |  |
| 22 | `enkin` | varchar(100) | yes |  |  |
| 23 | `nkcon` | varchar(100) | yes |  |  |
| 24 | `efa` | tinyint | yes |  | ('0') |
| 25 | `efaedate` | datetime | yes |  |  |
| 26 | `ecdm` | tinyint | yes |  | ('0') |
| 27 | `ecdmedate` | datetime | yes |  |  |
| 28 | `enic` | tinyint | yes |  | ('0') |
| 29 | `enicedate` | datetime | yes |  |  |
| 30 | `ecorgi` | tinyint | yes |  | ('0') |
| 31 | `ecorgiedate` | datetime | yes |  |  |
| 32 | `cscs` | datetime | yes |  |  |
| 33 | `crb` | tinyint | yes |  | ('0') |
| 34 | `crbedate` | datetime | yes |  |  |
| 35 | `photo1` | varchar(200) | yes |  |  |
| 36 | `nameini` | varchar(5) | yes |  |  |
| 37 | `acribno` | varchar(50) | yes |  |  |
| 38 | `acribdate` | datetime | yes |  |  |
| 39 | `region` | varchar(100) | yes |  |  |
| 40 | `jobapp` | tinyint | yes |  | ('0') |
| 41 | `epass` | varchar(50) | yes |  |  |
| 42 | `clshow` | int | yes |  | ('0') |
| 43 | `sno` | int | yes |  |  |
| 44 | `engveh` | tinyint | no |  | ('0') |
| 45 | `wgname` | varchar(100) | yes |  |  |
| 46 | `gaslicno` | varchar(100) | yes |  |  |
| 47 | `payrollno` | varchar(50) | yes |  |  |
| 48 | `trade` | varchar(100) | yes |  |  |
| 49 | `ecategory` | varchar(100) | yes |  |  |
| 50 | `ecompany` | varchar(200) | yes |  |  |
| 51 | `costhead` | varchar(100) | yes |  |  |
| 52 | `nino` | varchar(100) | yes |  |  |
| 53 | `paysture` | varchar(50) | yes |  |  |
| 54 | `trpassword` | varchar(50) | yes |  |  |
| 55 | `trsdate` | datetime | yes |  |  |
| 56 | `tredate` | datetime | yes |  |  |
| 57 | `sname` | varchar(50) | yes |  |  |
| 58 | `eskill` | varchar(50) | yes |  |  |
| 59 | `etablet` | varchar(50) | yes |  |  |
| 60 | `esssts` | datetime | yes |  |  |
| 61 | `esmsts` | datetime | yes |  |  |
| 62 | `efiras` | datetime | yes |  |  |
| 63 | `epasma` | datetime | yes |  |  |
| 64 | `eipaf` | datetime | yes |  |  |
| 65 | `eledge` | datetime | yes |  |  |
| 66 | `esentinel` | datetime | yes |  |  |
| 67 | `ehiltidx` | datetime | yes |  |  |
| 68 | `ehiltigx` | datetime | yes |  |  |
| 69 | `envq2` | datetime | yes |  |  |
| 70 | `envq3` | datetime | yes |  |  |
| 71 | `epts` | datetime | yes |  |  |
| 72 | `eici` | datetime | yes |  |  |
| 73 | `shstart` | datetime | yes |  |  |
| 74 | `shend` | datetime | yes |  |  |
| 75 | `slunch` | varchar(50) | yes |  |  |
| 76 | `egroup` | varchar(100) | yes |  |  |
| 77 | `engstatic` | tinyint | no |  | ('0') |
| 78 | `dwarning` | tinyint | no |  | ('0') |
| 79 | `wmsg` | varchar(100) | yes |  |  |
| 80 | `ephoto` | image | yes |  |  |
| 81 | `empsur` | int | yes |  | ('0') |
| 82 | `pemail` | varchar(100) | yes |  |  |
| 83 | `lastupdate` | datetime | yes |  |  |
| 84 | `covidvac` | tinyint | no |  | ('0') |
| 85 | `smanager` | int | yes |  | ('0') |
| 86 | `ocardno` | varchar(200) | yes |  |  |
| 87 | `appcode` | varchar(50) | yes |  |  |

### dbo.employeerate

Rows: 67

Primary key: `erkey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__employeera__idno__2CF2ADDF`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `erkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `rate` | decimal(16,2) | no |  | ('0.00') |
| 4 | `hrate` | decimal(16,2) | no |  | ('0.00') |
| 5 | `overhead` | decimal(16,2) | no |  | ('0.00') |
| 6 | `overtime` | decimal(16,2) | no |  | ('0.00') |
| 7 | `startdate` | datetime | yes |  |  |
| 8 | `enddate` | datetime | yes |  |  |

### dbo.engappmsg

Rows: 0

Primary key: `eakey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eakey` | int | no | 1, 1 |  |
| 2 | `sendmsg` | varchar(500) | yes |  |  |
| 3 | `cdate` | datetime | yes |  |  |

### dbo.engcale

Rows: 124

Primary key: `ckey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__engcale__idno__1F63A897`)
- `jobno` to `orders.jobno` (`FK__engcale__jobno__2057CCD0`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ckey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `stime` | datetime | yes |  |  |
| 5 | `etime` | datetime | yes |  |  |
| 6 | `des` | text | yes |  |  |

### dbo.engexpdate

Rows: 0

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `alkey` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `expdate` | datetime | yes |  |  |
| 5 | `senddate` | datetime | yes |  |  |
| 6 | `plivalue` | decimal(16,2) | yes |  |  |
| 7 | `actype` | int | yes |  | ('0') |

### dbo.engexplog

Rows: 0

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `name` | varchar(500) | yes |  |  |
| 5 | `des` | varchar(500) | yes |  |  |

### dbo.engfile

Rows: 0

Primary key: `efkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `efkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `cdate` | datetime | yes |  |  |
| 4 | `fullpath` | varchar(500) | yes |  |  |
| 5 | `fname` | varchar(500) | yes |  |  |

### dbo.engrate

Rows: 8

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `daynum` | int | yes |  |  |
| 3 | `ntrule` | varchar(10) | yes |  |  |
| 4 | `ntstart` | datetime | yes |  |  |
| 5 | `ntend` | datetime | yes |  |  |
| 6 | `ntrate` | decimal(16,2) | yes |  |  |
| 7 | `otrule` | varchar(10) | yes |  |  |
| 8 | `otrate` | decimal(16,2) | yes |  |  |
| 9 | `jlono` | int | yes |  | ('0') |

### dbo.engtimerate

Rows: 8

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `daynum` | int | yes |  |  |
| 3 | `s1_start` | datetime | yes |  |  |
| 4 | `s1_end` | datetime | yes |  |  |
| 5 | `s1_rule` | varchar(10) | yes |  |  |
| 6 | `s1_rate` | decimal(16,2) | yes |  |  |
| 7 | `s2_start` | datetime | yes |  |  |
| 8 | `s2_end` | datetime | yes |  |  |
| 9 | `s2_rule` | varchar(10) | yes |  |  |
| 10 | `s2_rate` | decimal(16,2) | yes |  |  |
| 11 | `s3_start` | datetime | yes |  |  |
| 12 | `s3_end` | datetime | yes |  |  |
| 13 | `s3_rule` | varchar(10) | yes |  |  |
| 14 | `s3_rate` | decimal(16,2) | yes |  |  |
| 15 | `s4_start` | datetime | yes |  |  |
| 16 | `s4_end` | datetime | yes |  |  |
| 17 | `s4_rule` | varchar(10) | yes |  |  |
| 18 | `s4_rate` | decimal(16,2) | yes |  |  |
| 19 | `jlono` | int | yes |  | ('0') |

### dbo.engtrackerinfo

Rows: 0

Primary key: `etakey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `etakey` | int | no | 1, 1 |  |
| 2 | `etkey` | varchar(50) | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `cdate` | datetime | yes |  |  |
| 5 | `logdate` | datetime | yes |  |  |
| 6 | `lat` | varchar(50) | yes |  |  |
| 7 | `long` | varchar(50) | yes |  |  |
| 8 | `thisaddress` | varchar(300) | yes |  |  |
| 9 | `updatetime` | datetime | yes |  |  |

### dbo.engtraining

Rows: 0

Primary key: `etkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `etkey` | int | no | 1, 1 |  |
| 2 | `tctype` | varchar(200) | yes |  |  |
| 3 | `tcexpdate` | datetime | yes |  |  |
| 4 | `tccomment` | varchar(500) | yes |  |  |
| 5 | `idno` | int | yes |  |  |

### dbo.fault

Rows: 11

Primary key: `fid` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fid` | int | no | 1, 1 |  |
| 2 | `name` | varchar(250) | yes |  |  |
| 3 | `install` | tinyint | yes |  | ('0') |

### dbo.fileerrorcopy

Rows: 0

Primary key: `feno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `feno` | int | no | 1, 1 |  |
| 2 | `sfname` | varchar(250) | yes |  |  |
| 3 | `dfname` | varchar(250) | yes |  |  |
| 4 | `etime` | datetime | yes |  |  |
| 5 | `ectime` | datetime | yes |  |  |
| 6 | `edone` | tinyint | yes |  | ('0') |

### dbo.fileimportlist

Rows: 0

Primary key: `fkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fkey` | int | no | 1, 1 |  |
| 2 | `name` | varchar(500) | yes |  |  |
| 3 | `adddate` | datetime | yes |  |  |

### dbo.fileimportlisttp

Rows: 0

Primary key: `fkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fkey` | int | no | 1, 1 |  |
| 2 | `name` | varchar(500) | yes |  |  |
| 3 | `adddate` | datetime | yes |  |  |

### dbo.filepermissionjob

Rows: 0

Primary key: `fkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fkey` | int | no | 1, 1 |  |
| 2 | `username` | varchar(30) | yes |  |  |
| 3 | `fno` | int | yes |  |  |

### dbo.filepermissionsite

Rows: 0

Primary key: `fkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fkey` | int | no | 1, 1 |  |
| 2 | `username` | varchar(30) | yes |  |  |
| 3 | `fno` | int | yes |  |  |

### dbo.floorplan

Rows: 0

Primary key: `fpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fpkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `fpname` | varchar(100) | yes |  |  |
| 4 | `floorno` | varchar(20) | yes |  |  |
| 5 | `blockno` | varchar(20) | yes |  |  |
| 6 | `filename` | varchar(250) | yes |  |  |
| 7 | `gridno` | varchar(20) | yes |  |  |
| 8 | `allfilename` | varchar(250) | yes |  |  |
| 9 | `fpkeyapp` | varchar(250) | yes |  |  |
| 10 | `addbyapp` | int | yes |  | ('0') |
| 11 | `filenamebig` | varchar(250) | yes |  |  |
| 12 | `allfilenamebig` | varchar(250) | yes |  |  |
| 13 | `scaleupx` | decimal(16,6) | yes |  |  |
| 14 | `scaleupy` | decimal(16,6) | yes |  |  |
| 15 | `scaledownx` | decimal(16,6) | yes |  |  |
| 16 | `scaledowny` | decimal(16,6) | yes |  |  |
| 17 | `needscale` | tinyint | yes |  | ('0') |
| 18 | `sendeng` | tinyint | yes |  | ('1') |
| 19 | `sealfilenamefill` | varchar(250) | yes |  |  |
| 20 | `sealfilenamefillall` | varchar(250) | yes |  |  |
| 21 | `onlysur` | tinyint | yes |  | ('0') |

### dbo.floorseal

Rows: 0

Primary key: `fpskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fpskey` | int | no | 1, 1 |  |
| 2 | `fpkey` | int | yes |  |  |
| 3 | `filename` | varchar(250) | yes |  |  |
| 4 | `sealfilename` | varchar(250) | yes |  |  |
| 5 | `sealno` | varchar(100) | yes |  |  |
| 6 | `photo1` | varchar(250) | yes |  |  |
| 7 | `photo2` | varchar(250) | yes |  |  |
| 8 | `xpoint` | int | yes |  |  |
| 9 | `ypoint` | int | yes |  |  |
| 10 | `jobno` | int | yes |  |  |
| 11 | `idno` | int | yes |  |  |
| 12 | `cdate` | datetime | yes |  |  |
| 13 | `fsize` | int | yes |  | ('20') |
| 14 | `path` | varchar(250) | yes |  |  |
| 15 | `sealfilenamebig` | varchar(250) | yes |  |  |
| 16 | `xpoints` | int | yes |  |  |
| 17 | `ypoints` | int | yes |  |  |
| 18 | `sealfilenamefill` | varchar(250) | yes |  |  |
| 19 | `sealfilenamefillall` | varchar(250) | yes |  |  |
| 20 | `sealfilenamefillallthis` | varchar(250) | yes |  |  |
| 21 | `photo3` | varchar(250) | yes |  |  |
| 22 | `photo4` | varchar(250) | yes |  |  |
| 23 | `photo5` | varchar(250) | yes |  |  |
| 24 | `item` | varchar(500) | yes |  |  |
| 25 | `surcomment` | varchar(1000) | yes |  |  |
| 26 | `revisited` | int | yes |  | ('0') |
| 27 | `completed` | int | yes |  | ('0') |
| 28 | `surtag` | int | yes |  | ('0') |
| 29 | `fpdetail` | varchar(100) | yes |  |  |
| 30 | `cref` | varchar(50) | yes |  |  |
| 31 | `frating` | varchar(10) | yes |  |  |
| 32 | `acrating` | varchar(10) | yes |  |  |
| 33 | `fpother1` | varchar(100) | yes |  |  |
| 34 | `fpother2` | varchar(100) | yes |  |  |
| 35 | `ourpno` | varchar(100) | yes |  |  |

### dbo.floorsealfill

Rows: 0

Primary key: `fsfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fsfkey` | int | no | 1, 1 |  |
| 2 | `fpkey` | int | yes |  |  |
| 3 | `filename` | varchar(250) | yes |  |  |
| 4 | `fillsealfilename` | varchar(250) | yes |  |  |
| 5 | `sealno` | varchar(100) | yes |  |  |
| 6 | `xpoints` | int | yes |  |  |
| 7 | `ypoints` | int | yes |  |  |
| 8 | `xpointe` | int | yes |  |  |
| 9 | `ypointe` | int | yes |  |  |
| 10 | `path` | varchar(250) | yes |  |  |
| 11 | `sfcolor` | varchar(50) | yes |  |  |
| 12 | `fillsealfilenamebig` | varchar(250) | yes |  |  |
| 13 | `jobno` | int | yes |  |  |
| 14 | `idno` | int | yes |  |  |
| 15 | `cdate` | datetime | yes |  |  |
| 16 | `sealfilenamefill` | varchar(250) | yes |  |  |
| 17 | `sealfilenamefillall` | varchar(250) | yes |  |  |

### dbo.gencojobtracker

Rows: 0

Primary key: `gkey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__gencojobt__jobno__4DD47EBD`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `gkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `gstatus` | varchar(300) | yes |  |  |
| 4 | `gpc` | varchar(300) | yes |  |  |
| 5 | `gpcidrec` | varchar(50) | yes |  |  |
| 6 | `cppw` | varchar(50) | yes |  |  |
| 7 | `loarec` | varchar(50) | yes |  |  |
| 8 | `subqrec` | varchar(50) | yes |  |  |
| 9 | `subqapp` | varchar(50) | yes |  |  |
| 10 | `gstartdate` | datetime | yes |  |  |
| 11 | `gdd` | datetime | yes |  |  |
| 12 | `glastupdate` | text | yes |  |  |
| 13 | `cpprsent` | datetime | yes |  |  |
| 14 | `cpprapproved` | datetime | yes |  |  |
| 15 | `powstatus` | varchar(100) | yes |  |  |
| 16 | `powsent` | datetime | yes |  |  |
| 17 | `powapproved` | datetime | yes |  |  |
| 18 | `loareceived` | datetime | yes |  |  |
| 19 | `smanagerreq` | tinyint | yes |  | ('0') |
| 20 | `smname` | varchar(100) | yes |  |  |
| 21 | `onsitecdate` | datetime | yes |  |  |
| 22 | `omreq` | varchar(100) | yes |  |  |
| 23 | `omstatus` | varchar(100) | yes |  |  |
| 24 | `psreq` | varchar(100) | yes |  |  |
| 25 | `psstatus` | varchar(100) | yes |  |  |
| 26 | `handsreq` | varchar(200) | yes |  |  |
| 27 | `cppramcomdate` | datetime | yes |  |  |
| 28 | `iworkreq` | varchar(10) | yes |  |  |
| 29 | `sasetsreq` | varchar(100) | yes |  |  |
| 30 | `cclistsave` | varchar(100) | yes |  |  |
| 31 | `ramscportal` | varchar(10) | yes |  |  |

### dbo.hireplant

Rows: 2

Primary key: `phkey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__hireplant__jobno__5D95E53A`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `phkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `pname` | text | yes |  |  |
| 4 | `sname` | varchar(100) | yes |  |  |
| 5 | `description` | text | yes |  |  |
| 6 | `price` | decimal(16,2) | yes |  |  |
| 7 | `hdate` | datetime | yes |  |  |
| 8 | `rdate` | datetime | yes |  |  |
| 9 | `href` | varchar(50) | yes |  |  |
| 10 | `pret` | tinyint | yes |  | ('0') |
| 11 | `ohpno` | int | yes |  |  |
| 12 | `ostatus` | varchar(50) | yes |  |  |
| 13 | `pncode` | varchar(50) | yes |  |  |
| 14 | `hptype` | tinyint | yes |  | ('0') |
| 15 | `concurdone` | tinyint | yes |  | ('0') |
| 16 | `comcodename` | varchar(200) | yes |  |  |
| 17 | `epdone` | tinyint | yes |  | ('0') |
| 18 | `epdate` | datetime | yes |  |  |
| 19 | `appdet` | tinyint | yes |  | ('0') |
| 20 | `appexport` | tinyint | yes |  | ('0') |
| 21 | `appdetuser` | varchar(100) | yes |  |  |
| 22 | `appdetdate` | datetime | yes |  |  |
| 23 | `appexportuser` | varchar(100) | yes |  |  |
| 24 | `appexportdate` | datetime | yes |  |  |
| 25 | `uploadexport` | tinyint | yes |  | ('0') |
| 26 | `uploadexportdate` | datetime | yes |  |  |
| 27 | `cancelpo` | tinyint | yes |  | ('0') |
| 28 | `requestor` | varchar(100) | yes |  |  |
| 29 | `thisoby` | varchar(50) | yes |  |  |

### dbo.houselist

Rows: 1003

Primary key: `sitecode` (clustered).

Referenced by:

- `emailsite.sitecode` (`FK__emailsite__sitec__2B7F66B9`)
- `managersite.sitecode` (`FK__managersi__sitec__62AFA012`)
- `orders.sitecode` (`FK__orders__sitecode__1273C1CD`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sitecode` | varchar(50) | no |  |  |
| 2 | `clientcode` | varchar(50) | yes |  |  |
| 3 | `house` | varchar(100) | yes |  |  |
| 4 | `address1` | varchar(100) | yes |  |  |
| 5 | `town` | varchar(50) | yes |  |  |
| 6 | `county` | varchar(50) | yes |  |  |
| 7 | `postcode` | varchar(30) | yes |  |  |
| 8 | `tel` | varchar(30) | yes |  |  |
| 9 | `fax` | varchar(30) | yes |  |  |
| 10 | `email` | varchar(70) | yes |  |  |
| 11 | `surveyor` | varchar(100) | yes |  |  |
| 12 | `surveyortel` | varchar(100) | yes |  |  |
| 13 | `region` | varchar(100) | yes |  |  |
| 14 | `concept` | varchar(50) | yes |  |  |
| 15 | `sman` | varchar(100) | yes |  |  |
| 16 | `cd` | tinyint | yes |  | ('1') |
| 17 | `info` | text | yes |  |  |
| 18 | `lgitude` | int | yes |  |  |
| 19 | `laitude` | int | yes |  |  |
| 20 | `amname` | varchar(100) | yes |  |  |
| 21 | `amtel` | varchar(100) | yes |  |  |
| 22 | `ophour` | varchar(100) | yes |  |  |
| 23 | `etedate` | datetime | yes |  |  |
| 24 | `gtedate` | datetime | yes |  |  |
| 25 | `cpark` | tinyint | yes |  | ('0') |
| 26 | `afee` | decimal(16,2) | yes |  |  |
| 27 | `oafee` | decimal(16,2) | yes |  |  |
| 28 | `noroom` | varchar(50) | yes |  |  |
| 29 | `sixmdue` | datetime | yes |  |  |
| 30 | `uprn` | varchar(50) | yes |  |  |
| 31 | `lg` | int | yes |  |  |
| 32 | `la` | int | yes |  |  |
| 33 | `nochair` | varchar(50) | yes |  |  |
| 34 | `newsletter` | tinyint | yes |  | ('0') |
| 35 | `xmascard` | tinyint | yes |  | ('0') |
| 36 | `xmasgift` | tinyint | yes |  | ('0') |
| 37 | `xmasdes` | text | yes |  |  |
| 38 | `sssdate` | datetime | yes |  |  |
| 39 | `ssrdate` | datetime | yes |  |  |
| 40 | `cexpdate` | datetime | yes |  |  |
| 41 | `connumber` | varchar(50) | yes |  |  |
| 42 | `contype` | varchar(100) | yes |  |  |
| 43 | `idname` | varchar(100) | yes |  |  |
| 44 | `conlost` | datetime | yes |  |  |
| 45 | `conval` | varchar(50) | yes |  |  |
| 46 | `jlono` | int | yes |  | ('0') |
| 47 | `dmsg` | int | yes |  | ('0') |
| 48 | `msgtext` | varchar(300) | yes |  |  |
| 49 | `patqty` | varchar(100) | yes |  |  |
| 50 | `patexp` | datetime | yes |  |  |
| 51 | `noofapp` | varchar(100) | yes |  |  |
| 52 | `noofcircuits` | varchar(100) | yes |  |  |
| 53 | `leads` | int | yes |  | ('0') |
| 54 | `micro` | int | yes |  | ('0') |
| 55 | `fixapp` | int | yes |  | ('0') |
| 56 | `speciallab` | int | yes |  | ('0') |
| 57 | `scharge` | varchar(500) | yes |  |  |
| 58 | `salesinfo` | text | yes |  |  |
| 59 | `sotsname` | varchar(50) | yes |  |  |
| 60 | `atmsitecode` | varchar(100) | yes |  |  |
| 61 | `monfristart` | datetime | yes |  |  |
| 62 | `monfriend` | datetime | yes |  |  |
| 63 | `satstart` | datetime | yes |  |  |
| 64 | `satend` | datetime | yes |  |  |
| 65 | `sunstart` | datetime | yes |  |  |
| 66 | `sunend` | datetime | yes |  |  |
| 67 | `screcove` | tinyint | yes |  | ('0') |
| 68 | `cherrypused` | tinyint | yes |  | ('0') |
| 69 | `adduname` | varchar(50) | yes |  |  |
| 70 | `updateuname` | varchar(50) | yes |  |  |
| 71 | `patexp3` | datetime | yes |  |  |
| 72 | `patexp6` | datetime | yes |  |  |
| 73 | `patexp12` | datetime | yes |  |  |
| 74 | `patexp24` | datetime | yes |  |  |
| 75 | `patexp36` | datetime | yes |  |  |
| 76 | `noofapp3` | varchar(100) | yes |  |  |
| 77 | `noofapp6` | varchar(100) | yes |  |  |
| 78 | `noofapp12` | varchar(100) | yes |  |  |
| 79 | `noofapp24` | varchar(100) | yes |  |  |
| 80 | `noofapp36` | varchar(100) | yes |  |  |
| 81 | `nolight` | varchar(100) | yes |  |  |
| 82 | `fadate` | datetime | yes |  |  |
| 83 | `elightdate` | datetime | yes |  |  |
| 84 | `eicrdate` | datetime | yes |  |  |
| 85 | `ivclientcode` | varchar(50) | yes |  |  |
| 86 | `regiongc` | varchar(100) | yes |  |  |

### dbo.idlist

Rows: 0

Primary key: `idkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `idkey` | int | no | 1, 1 |  |
| 2 | `idname` | varchar(100) | yes |  |  |

### dbo.invoice

Rows: 4928

Primary key: `ipkey` (clustered).

Foreign keys out:

- `partsorderno` to `partsorder.partsorderno` (`FK__invoice__partsor__6DCC4D03`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ipkey` | int | no | 1, 1 |  |
| 2 | `partsorderno` | int | yes |  |  |
| 3 | `ivno` | varchar(50) | yes |  |  |
| 4 | `ivdate` | datetime | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `amount` | decimal(16,2) | yes |  |  |
| 7 | `sagedone` | tinyint | yes |  | ('0') |
| 8 | `lamount` | decimal(16,2) | yes |  |  |
| 9 | `pamount` | decimal(16,2) | yes |  |  |
| 10 | `lncode` | varchar(50) | yes |  |  |
| 11 | `pncode` | varchar(50) | yes |  |  |
| 12 | `pvat` | decimal(16,2) | yes |  |  |
| 13 | `pvcode` | varchar(50) | yes |  |  |
| 14 | `plvat` | decimal(16,2) | yes |  |  |
| 15 | `plvcode` | varchar(50) | yes |  |  |
| 16 | `pfno` | varchar(50) | yes |  |  |
| 17 | `aftotal` | decimal(16,2) | yes |  |  |
| 18 | `vamt` | decimal(16,2) | yes |  |  |
| 19 | `lvamt` | decimal(16,2) | yes |  |  |
| 20 | `ivpaid` | tinyint | yes |  | ('0') |
| 21 | `ivduedate` | datetime | yes |  |  |
| 22 | `cisamount` | decimal(16,2) | yes |  |  |
| 23 | `cisncode` | varchar(50) | yes |  |  |
| 24 | `cisvcode` | varchar(50) | yes |  |  |
| 25 | `cisvat` | decimal(16,2) | yes |  |  |
| 26 | `cisvamt` | decimal(16,2) | yes |  |  |
| 27 | `concurinv` | tinyint | yes |  | ('0') |
| 28 | `ivupdate` | datetime | yes |  |  |
| 29 | `upfilename` | varchar(100) | yes |  |  |
| 30 | `ivfull` | tinyint | no |  | ('0') |

### dbo.invoicehireplant

Rows: 0

Primary key: `ihpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ihpkey` | int | no | 1, 1 |  |
| 2 | `ohpno` | int | yes |  |  |
| 3 | `ivno` | varchar(50) | yes |  |  |
| 4 | `ivdate` | datetime | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `amount` | decimal(16,2) | yes |  |  |
| 7 | `sagedone` | tinyint | yes |  | ('0') |
| 8 | `lamount` | decimal(16,2) | yes |  |  |
| 9 | `pamount` | decimal(16,2) | yes |  |  |
| 10 | `pvat` | decimal(16,2) | yes |  |  |
| 11 | `pvcode` | varchar(50) | yes |  |  |
| 12 | `pfno` | varchar(50) | yes |  |  |
| 13 | `aftotal` | decimal(16,2) | yes |  |  |
| 14 | `vamt` | decimal(16,2) | yes |  |  |
| 15 | `ivduedate` | datetime | yes |  |  |
| 16 | `ivupdate` | datetime | yes |  |  |
| 17 | `upfilename` | varchar(100) | yes |  |  |
| 18 | `ivfull` | tinyint | no |  | ('0') |

### dbo.invoiceref

Rows: 0

Primary key: `ivpkey` (clustered).

Foreign keys out:

- `orno` to `orderrefrigerant.orno` (`FK__invoiceref__orno__23BE4960`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ivpkey` | int | no | 1, 1 |  |
| 2 | `orno` | int | yes |  |  |
| 3 | `ivno` | varchar(50) | yes |  |  |
| 4 | `ivdate` | datetime | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `amount` | decimal(16,2) | yes |  |  |
| 7 | `sagedone` | tinyint | yes |  | ('0') |
| 8 | `lamount` | decimal(16,2) | yes |  |  |
| 9 | `pamount` | decimal(16,2) | yes |  |  |
| 10 | `pvat` | decimal(16,2) | yes |  |  |
| 11 | `pvcode` | varchar(50) | yes |  |  |
| 12 | `pfno` | varchar(50) | yes |  |  |
| 13 | `aftotal` | decimal(16,2) | yes |  |  |
| 14 | `vamt` | decimal(16,2) | yes |  |  |

### dbo.invoicevan

Rows: 0

Primary key: `ivpkey` (clustered).

Foreign keys out:

- `vorderno` to `vanpartsorder.vorderno` (`FK__invoiceva__vorde__370627FE`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ivpkey` | int | no | 1, 1 |  |
| 2 | `vorderno` | int | yes |  |  |
| 3 | `ivno` | varchar(50) | yes |  |  |
| 4 | `ivdate` | datetime | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `amount` | decimal(16,2) | yes |  |  |
| 7 | `sagedone` | tinyint | yes |  | ('0') |
| 8 | `lamount` | decimal(16,2) | yes |  |  |
| 9 | `pamount` | decimal(16,2) | yes |  |  |
| 10 | `pvat` | decimal(16,2) | yes |  |  |
| 11 | `pvcode` | varchar(50) | yes |  |  |
| 12 | `pfno` | varchar(50) | yes |  |  |
| 13 | `aftotal` | decimal(16,2) | yes |  |  |
| 14 | `vamt` | decimal(16,2) | yes |  |  |
| 15 | `jlono` | int | yes |  | ('0') |
| 16 | `ivduedate` | datetime | yes |  |  |
| 17 | `ivupdate` | datetime | yes |  |  |
| 18 | `upfilename` | varchar(100) | yes |  |  |

### dbo.itemeng

Rows: 0

Primary key: `iekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `iekey` | int | no | 1, 1 |  |
| 2 | `vlkey` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `vskey` | int | yes |  |  |

### dbo.itemexport

Rows: 0

Primary key: `ikey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ikey` | int | no | 1, 1 |  |
| 2 | `copydate` | datetime | yes |  |  |
| 3 | `floorname` | varchar(500) | yes |  |  |
| 4 | `floorno` | varchar(50) | yes |  |  |
| 5 | `blockno` | varchar(50) | yes |  |  |
| 6 | `gridno` | varchar(50) | yes |  |  |
| 7 | `sealno` | varchar(100) | yes |  |  |
| 8 | `installdate` | varchar(50) | yes |  |  |
| 9 | `workhrs` | varchar(50) | yes |  |  |
| 10 | `nightshift` | varchar(50) | yes |  |  |
| 11 | `ourpno` | varchar(50) | yes |  |  |
| 12 | `partsname` | varchar(500) | yes |  |  |
| 13 | `des` | varchar(1000) | yes |  |  |
| 14 | `exczone` | varchar(20) | yes |  |  |
| 15 | `qty` | varchar(20) | yes |  |  |
| 16 | `length` | varchar(20) | yes |  |  |
| 17 | `width` | varchar(20) | yes |  |  |
| 18 | `depth` | varchar(20) | yes |  |  |
| 19 | `dimension` | varchar(20) | yes |  |  |
| 20 | `measurement` | varchar(20) | yes |  |  |
| 21 | `idno` | varchar(20) | yes |  |  |
| 22 | `maineng` | varchar(200) | yes |  |  |
| 23 | `alleng` | varchar(1000) | yes |  |  |
| 24 | `watercontrol` | varchar(50) | yes |  |  |
| 25 | `dustcontrol` | varchar(50) | yes |  |  |
| 26 | `corewatch` | varchar(50) | yes |  |  |
| 27 | `inverted` | varchar(50) | yes |  |  |
| 28 | `angled` | varchar(50) | yes |  |  |
| 29 | `acrowsuctionweightedrig` | varchar(50) | yes |  |  |
| 30 | `highlevelworking` | varchar(50) | yes |  |  |
| 31 | `rate` | varchar(50) | yes |  |  |
| 32 | `location` | varchar(200) | yes |  |  |
| 33 | `material` | varchar(200) | yes |  |  |
| 34 | `mmused` | varchar(50) | yes |  |  |
| 35 | `bitbroken` | varchar(50) | yes |  |  |
| 36 | `uniqueid` | varchar(50) | yes |  |  |
| 37 | `floorplan` | varchar(50) | yes |  |  |
| 38 | `jobsheet` | varchar(500) | yes |  |  |
| 39 | `jobsheet1` | varchar(500) | yes |  |  |
| 40 | `jobsheet2` | varchar(500) | yes |  |  |
| 41 | `jobsheet3` | varchar(500) | yes |  |  |
| 42 | `jobsheet4` | varchar(500) | yes |  |  |
| 43 | `jobsheet5` | varchar(500) | yes |  |  |
| 44 | `jobsheet6` | varchar(500) | yes |  |  |
| 45 | `jobsheet7` | varchar(500) | yes |  |  |
| 46 | `jobsheet8` | varchar(500) | yes |  |  |
| 47 | `jobsheet9` | varchar(500) | yes |  |  |
| 48 | `jobsheet10` | varchar(500) | yes |  |  |
| 49 | `jobno` | varchar(50) | yes |  |  |
| 50 | `xpoint` | varchar(50) | yes |  |  |
| 51 | `ypoint` | varchar(50) | yes |  |  |
| 52 | `xpoints` | varchar(50) | yes |  |  |
| 53 | `ypoints` | varchar(50) | yes |  |  |
| 54 | `itemphotoname` | varchar(500) | yes |  |  |
| 55 | `tagphoto1` | varchar(500) | yes |  |  |
| 56 | `tagphoto2` | varchar(500) | yes |  |  |
| 57 | `tagphoto3` | varchar(500) | yes |  |  |
| 58 | `tagphoto4` | varchar(500) | yes |  |  |
| 59 | `tagphoto5` | varchar(500) | yes |  |  |
| 60 | `floorfilename` | varchar(1000) | yes |  |  |
| 61 | `thistagfloorplan` | varchar(1000) | yes |  |  |
| 62 | `firerating` | varchar(50) | yes |  |  |
| 63 | `acousticrating` | varchar(50) | yes |  |  |
| 64 | `detailref` | varchar(500) | yes |  |  |
| 65 | `clientref` | varchar(500) | yes |  |  |
| 66 | `other1` | varchar(1000) | yes |  |  |
| 67 | `other2` | varchar(1000) | yes |  |  |
| 68 | `tagkey` | varchar(50) | yes |  |  |
| 69 | `allcationkey` | varchar(50) | yes |  |  |
| 70 | `visitkey` | varchar(50) | yes |  |  |
| 71 | `clientcode` | varchar(50) | yes |  |  |
| 72 | `orderno` | varchar(50) | yes |  |  |
| 73 | `indateonly` | datetime | yes |  |  |
| 74 | `jobsheet11` | varchar(500) | yes |  |  |
| 75 | `jobsheet12` | varchar(500) | yes |  |  |
| 76 | `jobsheet13` | varchar(500) | yes |  |  |
| 77 | `jobsheet14` | varchar(500) | yes |  |  |
| 78 | `jobsheet15` | varchar(500) | yes |  |  |
| 79 | `jobsheet16` | varchar(500) | yes |  |  |
| 80 | `jobsheet17` | varchar(500) | yes |  |  |
| 81 | `jobsheet18` | varchar(500) | yes |  |  |
| 82 | `jobsheet19` | varchar(500) | yes |  |  |
| 83 | `jobsheet20` | varchar(500) | yes |  |  |
| 84 | `jobsheet21` | varchar(500) | yes |  |  |
| 85 | `jobsheet22` | varchar(500) | yes |  |  |
| 86 | `jobsheet23` | varchar(500) | yes |  |  |
| 87 | `jobsheet24` | varchar(500) | yes |  |  |
| 88 | `jobsheet25` | varchar(500) | yes |  |  |

### dbo.jfimage

Rows: 1

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `jflogo` | image | yes |  |  |
| 3 | `picalm` | varchar(50) | yes |  |  |
| 4 | `sizemo` | varchar(50) | yes |  |  |
| 5 | `htop` | varchar(50) | yes |  |  |
| 6 | `jlono` | int | yes |  | ('0') |
| 7 | `ltsec` | int | yes |  | ('3600') |
| 8 | `assrate` | decimal(16,4) | yes |  | ('0.5000') |
| 9 | `htopn` | varchar(50) | yes |  |  |
| 10 | `htops` | varchar(50) | yes |  |  |
| 11 | `hbottomhp` | varchar(50) | yes |  |  |
| 12 | `emailtext` | text | yes |  |  |
| 13 | `emailsubject` | text | yes |  |  |

### dbo.jfmapsetup

Rows: 1

Primary key: `mpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mpkey` | int | no | 1, 1 |  |
| 2 | `mapurl` | varchar(250) | yes |  |  |
| 3 | `mapcode` | varchar(250) | yes |  |  |
| 4 | `gpttime` | int | yes |  |  |
| 5 | `gpposttime` | int | yes |  |  |
| 6 | `mapapiurl` | varchar(200) | yes |  |  |

### dbo.jfolder

Rows: 6

Primary key: `fno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fno` | int | no | 1, 1 |  |
| 2 | `fname` | varchar(250) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.jfpimage

Rows: 1

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `hdis` | tinyint | yes |  | ('0') |
| 3 | `jfhead` | image | yes |  |  |
| 4 | `fdis` | tinyint | yes |  | ('0') |
| 5 | `jffoot` | image | yes |  |  |
| 6 | `hhight` | varchar(50) | yes |  |  |
| 7 | `fhight` | varchar(50) | yes |  |  |
| 8 | `jlono` | int | yes |  | ('0') |
| 9 | `rmargin` | varchar(50) | yes |  |  |
| 10 | `lmargin` | varchar(50) | yes |  |  |
| 11 | `opidno` | varchar(50) | yes |  |  |
| 12 | `quotetemppath` | varchar(50) | yes |  |  |

### dbo.jfpostcode

Rows: 0

Primary key: `pcode` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pcode` | varchar(20) | no |  |  |
| 2 | `lat` | varchar(50) | yes |  |  |
| 3 | `lng` | varchar(50) | yes |  |  |

### dbo.jfrunlic

Rows: 0

Primary key: `jfno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jfno` | int | no | 1, 1 |  |
| 2 | `jfcname` | varchar(300) | yes |  |  |
| 3 | `jfnum` | varchar(10) | yes |  |  |
| 4 | `jflic` | varchar(400) | yes |  |  |
| 5 | `jlono` | int | yes |  | ('0') |
| 6 | `gcstartdate` | datetime | yes |  |  |
| 7 | `timeastartdate` | datetime | yes |  |  |

### dbo.jobcertificate

Rows: 0

Primary key: `jckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jckey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `aekey` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `ccdate` | datetime | yes |  |  |
| 6 | `jcform` | image | yes |  |  |
| 7 | `jctype` | tinyint | yes |  | ('0') |

### dbo.JobDetails

Rows: 0

Primary key: none. Heap, no indexes.

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `Jobtype` | varchar(30) | yes |  |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `client` | varchar(50) | yes |  |  |
| 4 | `Section` | varchar(50) | yes |  |  |
| 5 | `SubSection` | varchar(50) | yes |  |  |
| 6 | `town` | varchar(50) | yes |  |  |
| 7 | `house` | varchar(100) | yes |  |  |
| 8 | `PostArea` | varchar(30) | yes |  |  |
| 9 | `postcode` | varchar(30) | yes |  |  |
| 10 | `InvoiceComplete` | varchar(30) | yes |  |  |
| 11 | `Status` | varchar(50) | yes |  |  |
| 12 | `orderno` | varchar(30) | yes |  |  |
| 13 | `OrderDate` | varchar(30) | yes |  |  |
| 14 | `OrderMonth` | varchar(30) | yes |  |  |
| 15 | `Measured` | decimal(10,2) | yes |  |  |
| 16 | `Invoiced` | decimal(10,2) | yes |  |  |
| 17 | `Subcontract` | decimal(10,2) | yes |  |  |
| 18 | `Materials` | decimal(10,2) | yes |  |  |
| 19 | `Hired` | decimal(10,2) | yes |  |  |
| 20 | `Labour` | decimal(10,2) | yes |  |  |
| 21 | `Totalcost` | decimal(10,2) | yes |  |  |
| 22 | `GP` | decimal(10,2) | yes |  |  |
| 23 | `FirstInvoiceMonth` | varchar(30) | yes |  |  |
| 24 | `LastInvoiceMonth` | varchar(30) | yes |  |  |
| 25 | `FirstInvoiceDate` | varchar(30) | yes |  |  |
| 26 | `LastInvoiceDate` | varchar(30) | yes |  |  |
| 27 | `Fiscalyear` | decimal(10,2) | yes |  |  |
| 28 | `Quarter` | varchar(30) | yes |  |  |
| 29 | `Month` | varchar(30) | yes |  |  |
| 30 | `PurHireInvs` | decimal(10,2) | yes |  |  |
| 31 | `PurSubconInvs` | decimal(10,2) | yes |  |  |
| 32 | `PurMatsInvs` | decimal(10,2) | yes |  |  |
| 33 | `PurInvCosts` | decimal(10,2) | yes |  |  |
| 34 | `A-C-R` | varchar(30) | yes |  |  |
| 35 | `Interim` | varchar(30) | yes |  |  |
| 36 | `convalue` | decimal(10,2) | yes |  |  |
| 37 | `category` | int | yes |  |  |
| 38 | `Sameday` | int | yes |  |  |
| 39 | `jobcheck` | tinyint | yes |  |  |
| 40 | `StatusQueue` | varchar(50) | yes |  |  |
| 41 | `ValNo` | varchar(50) | yes |  |  |
| 42 | `PotentialSale` | decimal(10,2) | yes |  |  |

### dbo.joberror

Rows: 70

Primary key: `ulkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ulkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `errdes` | varchar(200) | yes |  |  |
| 4 | `formname` | varchar(50) | yes |  |  |
| 5 | `luser` | varchar(50) | yes |  |  |
| 6 | `ldate` | datetime | yes |  |  |
| 7 | `cname` | varchar(100) | yes |  |  |

### dbo.jobfile

Rows: 436

Primary key: `jfkey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__jobfile__jobno__0C50D423`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jfkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `cptime` | datetime | yes |  |  |
| 4 | `fpath` | varchar(100) | yes |  |  |
| 5 | `fname` | varchar(100) | yes |  |  |
| 6 | `fno` | int | yes |  |  |

### dbo.JobFlowJobSummary

Rows: 0

Primary key: none. Heap, no indexes.

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jobtype` | varchar(30) | yes |  |  |
| 2 | `client` | varchar(50) | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `Trade` | varchar(50) | yes |  |  |
| 5 | `deadline` | datetime | yes |  |  |
| 6 | `offsitecode` | int | yes |  |  |
| 7 | `JobStatus` | varchar(30) | yes |  |  |
| 8 | `Expired` | int | yes |  |  |
| 9 | `Now` | int | yes |  |  |
| 10 | `All` | int | yes |  |  |
| 11 | `ReturnVisit` | int | yes |  |  |
| 12 | `ToQuote` | int | yes |  |  |
| 13 | `AwaitClientResponse` | int | yes |  |  |
| 14 | `Status` | int | yes |  |  |
| 15 | `ToMeasure` | int | yes |  |  |
| 16 | `ToInvoice` | int | yes |  |  |
| 17 | `SubcontractOrder` | int | yes |  |  |
| 18 | `OnHire` | int | yes |  |  |

### dbo.jobflowlocation

Rows: 1

Primary key: `jlono` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jlono` | int | no | 0, 1 |  |
| 2 | `loname` | varchar(250) | yes |  |  |
| 3 | `company` | varchar(100) | yes |  |  |
| 4 | `address1` | varchar(100) | yes |  |  |
| 5 | `address2` | varchar(100) | yes |  |  |
| 6 | `town` | varchar(100) | yes |  |  |
| 7 | `county` | varchar(100) | yes |  |  |
| 8 | `postcode` | varchar(20) | yes |  |  |
| 9 | `tel` | varchar(30) | yes |  |  |
| 10 | `fax` | varchar(30) | yes |  |  |
| 11 | `email` | varchar(100) | yes |  |  |

### dbo.jobform

Rows: 0

Primary key: `jfkey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__jobform__idno__06ED0088`)
- `jobno` to `orders.jobno` (`FK__jobform__jobno__05F8DC4F`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jfkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `cdate` | datetime | yes |  |  |
| 5 | `tbforma` | image | yes |  |  |
| 6 | `tbformb` | image | yes |  |  |
| 7 | `tbformc` | image | yes |  |  |

### dbo.jobitem

Rows: 0

Primary key: `jikey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__jobitem__jobno__3138400F`)
- `ourpno` to `materials.ourpno` (`FK__jobitem__ourpno__30441BD6`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jikey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `jobno` | int | yes |  |  |

### dbo.jobmarkupper

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `startdate` | datetime | yes |  |  |
| 3 | `enddate` | datetime | yes |  |  |
| 4 | `mupper` | decimal(16,2) | yes |  |  |

### dbo.JobOperativeDetails

Rows: 0

Primary key: none. Heap, no indexes.

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `JobType` | varchar(30) | yes |  |  |
| 2 | `name` | varchar(100) | yes |  |  |
| 3 | `Dayofweek` | varchar(30) | yes |  |  |
| 4 | `timeonsite` | int | yes |  |  |
| 5 | `Travel` | int | yes |  |  |
| 6 | `TTCost` | decimal(10,2) | yes |  |  |
| 7 | `Day` | varchar(30) | yes |  |  |
| 8 | `LabourCost` | decimal(10,2) | yes |  |  |
| 9 | `client` | varchar(50) | yes |  |  |
| 10 | `house` | varchar(100) | yes |  |  |
| 11 | `postcode` | varchar(30) | yes |  |  |
| 12 | `PostArea` | varchar(30) | yes |  |  |
| 13 | `period` | varchar(30) | yes |  |  |
| 14 | `WithinSLA` | int | yes |  |  |
| 15 | `ftf` | tinyint | yes |  |  |

### dbo.jobplan

Rows: 31200

Primary key: `jpkey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__jobplan__idno__078C1F06`)
- `jobno` to `orders.jobno` (`FK__jobplan__jobno__0697FACD`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jpkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `pptime` | datetime | yes |  |  |
| 5 | `petime` | datetime | yes |  |  |
| 6 | `info` | varchar(50) | yes |  |  |
| 7 | `vdone` | tinyint | yes |  | ('0') |
| 8 | `prapp` | tinyint | yes |  | ('1') |
| 9 | `cdate` | datetime | yes |  |  |
| 10 | `pbook` | tinyint | yes |  | ('0') |
| 11 | `vnote` | varchar(500) | yes |  |  |
| 12 | `jofftype` | tinyint | yes |  | ('0') |
| 13 | `oncall` | tinyint | yes |  | ('0') |
| 14 | `apptimedone` | tinyint | no |  | ('0') |
| 15 | `appntimedc` | decimal(16,2) | yes |  |  |
| 16 | `appotimedc` | decimal(16,2) | yes |  |  |

### dbo.jobsalesman

Rows: 0

Primary key: `jskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jskey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `smname` | varchar(100) | yes |  |  |

### dbo.jobstatustwo

Rows: 0

Primary key: `jstatustwono` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jstatustwono` | int | no | 0, 1 |  |
| 2 | `jstwo` | varchar(50) | yes |  |  |

### dbo.jobworksheet

Rows: 0

Primary key: `aekey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__jobworksh__jobno__3AA1AEB8`)
- `idno` to `employee.idno` (`FK__jobworkshe__idno__3B95D2F1`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `aekey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `arrivaltime` | datetime | yes |  |  |
| 5 | `departuretime` | datetime | yes |  |  |
| 6 | `fsttimefix` | tinyint | yes |  |  |
| 7 | `comments` | text | yes |  |  |
| 8 | `username` | varchar(30) | yes |  |  |
| 9 | `path` | varchar(250) | yes |  |  |
| 10 | `fpath` | varchar(250) | yes |  |  |
| 11 | `cpath1` | varchar(250) | yes |  |  |
| 12 | `cpath2` | varchar(250) | yes |  |  |
| 13 | `cpath3` | varchar(250) | yes |  |  |
| 14 | `cpath4` | varchar(250) | yes |  |  |
| 15 | `cpath5` | varchar(250) | yes |  |  |
| 16 | `cpath6` | varchar(250) | yes |  |  |
| 17 | `cpath7` | varchar(250) | yes |  |  |
| 18 | `cpath8` | varchar(250) | yes |  |  |
| 19 | `cpath9` | varchar(250) | yes |  |  |
| 20 | `cpath10` | varchar(250) | yes |  |  |
| 21 | `totaltime` | int | yes |  |  |
| 22 | `trtime` | int | yes |  |  |
| 23 | `pptime` | datetime | yes |  |  |
| 24 | `opay` | decimal(16,2) | yes |  |  |
| 25 | `parking` | decimal(16,2) | yes |  |  |
| 26 | `oexp` | decimal(16,2) | yes |  |  |
| 27 | `mileage` | decimal(16,2) | yes |  |  |
| 28 | `sb` | tinyint | yes |  | ('0') |
| 29 | `sdatetime` | datetime | yes |  |  |
| 30 | `offsitecode` | int | yes |  |  |
| 31 | `etype` | tinyint | yes |  | ('0') |

### dbo.jwatchjobs

Rows: 0

Primary key: `jwjkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jwjkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `cdate` | datetime | yes |  |  |
| 5 | `engdate` | datetime | yes |  |  |
| 6 | `cuser` | varchar(50) | yes |  |  |
| 7 | `jwtosend` | tinyint | no |  | ('0') |
| 8 | `jwsend` | tinyint | no |  | ('0') |
| 9 | `jwsenddate` | datetime | yes |  |  |
| 10 | `jwjobid` | varchar(50) | yes |  |  |
| 11 | `jwcancel` | tinyint | no |  | ('0') |
| 12 | `jwcanceldate` | datetime | yes |  |  |
| 13 | `jwcanceltosend` | tinyint | no |  | ('0') |
| 14 | `jwsaveerror` | varchar(100) | yes |  |  |
| 15 | `jwcancelerror` | varchar(100) | yes |  |  |
| 16 | `jwreceived` | tinyint | no |  | ('0') |
| 17 | `jwreceiveddone` | tinyint | no |  | ('0') |
| 18 | `jwreceiveddate` | datetime | yes |  |  |
| 19 | `jwjobno` | varchar(100) | yes |  |  |

### dbo.jwatchposterror

Rows: 0

Primary key: `jwpekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jwpekey` | int | no | 1, 1 |  |
| 2 | `jwerrordes` | varchar(250) | yes |  |  |
| 3 | `cdate` | datetime | yes |  |  |
| 4 | `jwerrortype` | tinyint | yes |  | ('0') |
| 5 | `jobno` | int | yes |  |  |

### dbo.jwatchpostfile

Rows: 0

Primary key: `jwpfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jwpfkey` | int | no | 1, 1 |  |
| 2 | `jwpostfile` | varchar(200) | yes |  |  |
| 3 | `jwpostpath` | varchar(50) | yes |  |  |
| 4 | `cdate` | datetime | yes |  |  |
| 5 | `ptdone` | tinyint | yes |  | ('0') |
| 6 | `jobno` | int | yes |  |  |

### dbo.jwatchsetting

Rows: 0

Primary key: `jwkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jwkey` | int | no | 1, 1 |  |
| 2 | `jwurl` | varchar(200) | yes |  |  |
| 3 | `jwuser` | varchar(50) | yes |  |  |
| 4 | `jwpwd` | varchar(50) | yes |  |  |
| 5 | `jwurlkey` | varchar(200) | yes |  |  |

### dbo.locregion

Rows: 0

Primary key: `lrkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `lrkey` | int | no | 1, 1 |  |
| 2 | `jlono` | int | yes |  |  |
| 3 | `region` | varchar(100) | yes |  |  |

### dbo.loginuser

Rows: 4

Primary key: `username` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `username` | varchar(30) | no |  |  |
| 2 | `password` | varchar(20) | yes |  |  |
| 3 | `utype` | varchar(50) | yes |  |  |
| 4 | `capprove` | tinyint | yes |  | ('0') |
| 5 | `uin` | varchar(20) | yes |  |  |
| 6 | `jlono` | int | yes |  |  |
| 7 | `vall` | tinyint | yes |  | ('0') |
| 8 | `uemail` | varchar(200) | yes |  |  |
| 9 | `uemailpass` | varchar(50) | yes |  |  |
| 10 | `qapprove` | int | yes |  | ('0') |
| 11 | `dsapprove` | int | yes |  | ('0') |
| 12 | `qlimit` | decimal(16,2) | yes |  |  |
| 13 | `plimit` | decimal(16,2) | yes |  |  |
| 14 | `qsuser` | int | yes |  | ('0') |
| 15 | `cmuser` | int | yes |  | ('0') |
| 16 | `adminright` | int | yes |  | ('0') |
| 17 | `editctstsup` | int | yes |  | ('0') |
| 18 | `measureap` | int | yes |  | ('0') |
| 19 | `supsortype` | int | yes |  | ('0') |
| 20 | `suryortype` | int | yes |  | ('0') |
| 21 | `boardright` | int | yes |  | ('0') |
| 22 | `editdes` | int | yes |  | ('0') |
| 23 | `disuser` | int | yes |  | ('0') |
| 24 | `outuser` | int | yes |  | ('0') |
| 25 | `flogin` | int | yes |  | ('0') |
| 26 | `obranchview` | int | yes |  | ('0') |
| 27 | `supflag` | int | yes |  | ('0') |
| 28 | `conlogin` | int | yes |  | ('0') |
| 29 | `pmanlogin` | int | yes |  | ('0') |
| 30 | `applevel` | decimal(16,2) | yes |  |  |
| 31 | `appdet` | int | yes |  | ('0') |
| 32 | `canop` | int | yes |  | ('0') |
| 33 | `canupedit` | int | yes |  | ('0') |
| 34 | `pacancel` | int | yes |  | ('0') |
| 35 | `papbycc` | int | yes |  | ('0') |
| 36 | `papbypi` | int | yes |  | ('0') |
| 37 | `pasage` | int | yes |  | ('0') |
| 38 | `pamstock` | int | yes |  | ('0') |

### dbo.managereng

Rows: 0

Primary key: `mekey` (clustered).

Foreign keys out:

- `manid` to `supmanager.manid` (`FK__manageren__manid__68687968`)
- `idno` to `employee.idno` (`FK__managereng__idno__695C9DA1`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mekey` | int | no | 1, 1 |  |
| 2 | `manid` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |

### dbo.managersite

Rows: 0

Primary key: `mkey` (clustered).

Foreign keys out:

- `manid` to `supmanager.manid` (`FK__managersi__manid__61BB7BD9`)
- `sitecode` to `houselist.sitecode` (`FK__managersi__sitec__62AFA012`)
- `tid` to `team.tid` (`FK__managersite__tid__63A3C44B`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mkey` | int | no | 1, 1 |  |
| 2 | `manid` | int | yes |  |  |
| 3 | `sitecode` | varchar(50) | yes |  |  |
| 4 | `tid` | int | yes |  |  |

### dbo.materials

Rows: 0

Primary key: `ourpno` (clustered).

Referenced by:

- `jobitem.ourpno` (`FK__jobitem__ourpno__30441BD6`)
- `orderparts.ourpno` (`FK__orderpart__ourpn__47A6A41B`)
- `usematerials.ourpno` (`FK__usemateri__ourpn__282DF8C2`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ourpno` | varchar(50) | no |  |  |
| 2 | `sno` | int | yes |  |  |
| 3 | `partno` | int | yes |  |  |
| 4 | `spartno` | varchar(50) | yes |  |  |
| 5 | `manufacturer` | varchar(100) | yes |  |  |
| 6 | `manufacturerpcode` | varchar(50) | yes |  |  |
| 7 | `ptype` | int | yes |  | ('0') |
| 8 | `shelfno` | varchar(50) | yes |  |  |
| 9 | `jlono` | int | yes |  | ('0') |
| 10 | `inputtype` | int | yes |  | ('0') |
| 11 | `pstype` | int | yes |  | ('0') |
| 12 | `phide` | int | no |  | ('0') |
| 13 | `pncode` | varchar(50) | yes |  |  |
| 14 | `sncode` | varchar(50) | yes |  |  |
| 15 | `sorparts` | tinyint | yes |  | ('0') |
| 16 | `comcodename` | varchar(200) | yes |  |  |
| 17 | `deliverparts` | tinyint | yes |  | ('0') |

### dbo.materialsprice

Rows: 0

Primary key: `mpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mpkey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `terms` | decimal(16,2) | yes |  |  |
| 4 | `startdate` | datetime | yes |  |  |
| 5 | `enddate` | datetime | yes |  |  |
| 6 | `listprice` | decimal(16,2) | no |  | ('0.00') |
| 7 | `rrp` | decimal(16,2) | yes |  |  |
| 8 | `expdate` | datetime | yes |  |  |
| 9 | `labhour` | decimal(16,2) | yes |  |  |
| 10 | `labrate` | decimal(16,2) | yes |  |  |

### dbo.measurecalc

Rows: 1636

Primary key: `pqkey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__measureca__jobno__503BEA1C`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pqkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `description` | text | yes |  |  |
| 4 | `aov` | decimal(16,2) | yes |  |  |
| 5 | `afee` | decimal(16,2) | yes |  |  |
| 6 | `lhour` | decimal(16,2) | yes |  |  |
| 7 | `lrate` | decimal(16,2) | yes |  |  |
| 8 | `lmakeup100` | int | yes |  |  |
| 9 | `lmakeupp` | decimal(16,2) | yes |  |  |
| 10 | `ltotal` | decimal(16,2) | yes |  |  |
| 11 | `pprice` | decimal(16,2) | yes |  |  |
| 12 | `pmakeup100` | int | yes |  |  |
| 13 | `pmakeupp` | decimal(16,2) | yes |  |  |
| 14 | `ptotal` | decimal(16,2) | yes |  |  |
| 15 | `ftotal` | decimal(16,2) | yes |  |  |
| 16 | `cdate` | datetime | yes |  |  |
| 17 | `cmarkup` | decimal(16,2) | yes |  |  |

### dbo.measurefile

Rows: 0

Primary key: `mfno` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__measurefi__jobno__567ED357`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mfno` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `mdate` | datetime | yes |  |  |
| 4 | `mvalue` | decimal(16,2) | yes |  |  |
| 5 | `mfile` | varchar(200) | yes |  |  |
| 6 | `surveyor` | varchar(100) | yes |  |  |
| 7 | `approve` | tinyint | yes |  | ('0') |

### dbo.measurevalue

Rows: 14688

Primary key: `jobno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jobno` | int | no |  |  |
| 2 | `mvalue` | decimal(16,2) | yes |  |  |
| 3 | `bowcisno` | varchar(100) | yes |  |  |
| 4 | `date` | datetime | yes |  |  |
| 5 | `surveyor` | varchar(50) | yes |  |  |
| 6 | `paydone` | tinyint | yes |  | ('0') |
| 7 | `pono` | tinyint | yes |  | ('0') |
| 8 | `aret` | tinyint | yes |  | ('0') |
| 9 | `retdate` | datetime | yes |  |  |
| 10 | `retp` | decimal(10,4) | yes |  |  |
| 11 | `rettime` | int | yes |  |  |
| 12 | `retvalue` | decimal(16,2) | yes |  |  |
| 13 | `retdone` | tinyint | yes |  | ('0') |
| 14 | `aftotal` | decimal(16,2) | yes |  |  |

### dbo.mslive

Rows: 0

Primary key: `mskey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__mslive__jobno__50E5F592`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mskey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `revno` | varchar(50) | yes |  |  |
| 4 | `msfilename` | varchar(100) | yes |  |  |
| 5 | `jobno` | int | yes |  |  |

### dbo.mssign

Rows: 0

Primary key: `mskey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__mssign__jobno__53C2623D`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `mskey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `revno` | varchar(50) | yes |  |  |
| 4 | `engids` | varchar(500) | yes |  |  |
| 5 | `msfilename` | varchar(100) | yes |  |  |
| 6 | `jobno` | int | yes |  |  |
| 7 | `idno` | int | yes |  |  |
| 8 | `uplddate` | datetime | yes |  |  |
| 9 | `signmsno` | varchar(50) | yes |  |  |

### dbo.offsite

Rows: 9

Primary key: `offsitecode` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `offsitecode` | int | no |  |  |
| 2 | `value` | varchar(50) | yes |  |  |

### dbo.ooheng

Rows: 0

Primary key: `okey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__ooheng__idno__6B64E1A4`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `okey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |

### dbo.operative

Rows: 163

Primary key: `okey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__operative__idno__71D1E811`)
- `sscode` to `subsection.sscode` (`FK__operative__sscod__72C60C4A`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `okey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `sscode` | varchar(20) | yes |  |  |

### dbo.ordereq

Rows: 0

Primary key: `eqkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eqkey` | int | no | 1, 1 |  |
| 2 | `eqlkey` | int | yes |  |  |
| 3 | `modeldes` | text | yes |  |  |
| 4 | `model` | varchar(1000) | yes |  |  |
| 5 | `lprice` | decimal(16,2) | yes |  |  |
| 6 | `cprice` | decimal(16,2) | yes |  |  |
| 7 | `sprice` | decimal(16,2) | yes |  |  |
| 8 | `jobno` | int | yes |  |  |
| 9 | `cdate` | datetime | yes |  |  |
| 10 | `expdate` | datetime | yes |  |  |

### dbo.ordereqlink

Rows: 0

Primary key: `eqlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eqlkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `cdate` | datetime | yes |  |  |
| 4 | `sitecode` | varchar(50) | yes |  |  |
| 5 | `clientcode` | varchar(50) | yes |  |  |
| 6 | `orderno` | varchar(50) | yes |  |  |
| 7 | `lguser` | varchar(50) | yes |  |  |
| 8 | `fcost` | decimal(16,2) | yes |  |  |
| 9 | `inscost` | decimal(16,2) | yes |  |  |
| 10 | `sman1` | varchar(100) | yes |  |  |
| 11 | `sman1per` | varchar(10) | yes |  |  |
| 12 | `sman2` | varchar(100) | yes |  |  |
| 13 | `sman2per` | varchar(10) | yes |  |  |
| 14 | `sman3` | varchar(100) | yes |  |  |
| 15 | `sman3per` | varchar(10) | yes |  |  |
| 16 | `expdate` | datetime | yes |  |  |

### dbo.orderhireplant

Rows: 2

Primary key: `ohpno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ohpno` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `cdate` | datetime | yes |  |  |
| 4 | `sname` | varchar(100) | yes |  |  |
| 5 | `hnote` | text | yes |  |  |
| 6 | `oby` | varchar(50) | yes |  |  |

### dbo.orderparts

Rows: 0

Primary key: `pono` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__orderpart__jobno__46B27FE2`)
- `ourpno` to `materials.ourpno` (`FK__orderpart__ourpn__47A6A41B`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pono` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `quantity` | int | yes |  |  |
| 5 | `odate` | datetime | yes |  |  |
| 6 | `cdate` | datetime | yes |  |  |
| 7 | `rdate` | datetime | yes |  |  |
| 8 | `comments` | text | yes |  |  |
| 9 | `price` | decimal(16,2) | yes |  |  |
| 10 | `lprice` | decimal(16,2) | yes |  |  |
| 11 | `ostatus` | varchar(50) | yes |  |  |

### dbo.orderquotation

Rows: 4499

Primary key: `oqno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `oqno` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `cdate` | datetime | yes |  |  |
| 4 | `sname` | varchar(100) | yes |  |  |
| 5 | `oby` | varchar(50) | yes |  |  |
| 6 | `edate` | datetime | yes |  |  |
| 7 | `qsel` | tinyint | yes |  | ('0') |
| 8 | `qrjob` | tinyint | yes |  | ('0') |
| 9 | `oldjob` | varchar(50) | yes |  |  |
| 10 | `oldqno` | varchar(50) | yes |  |  |
| 11 | `qapprove` | tinyint | yes |  | ('0') |
| 12 | `qcname` | varchar(50) | yes |  |  |
| 13 | `qnote` | text | yes |  |  |
| 14 | `qsname` | varchar(100) | yes |  |  |
| 15 | `qnote2` | text | yes |  |  |
| 16 | `rejreason` | text | yes |  |  |
| 17 | `qtitle` | varchar(200) | yes |  |  |
| 18 | `japprovedate` | datetime | yes |  |  |
| 19 | `acr` | tinyint | yes |  | ('0') |
| 20 | `senddate` | datetime | yes |  |  |

### dbo.orderquotetemplate

Rows: 0

Primary key: `oqtno` (clustered).

Referenced by:

- `quotetemplate.oqtno` (`FK__quotetemp__oqtno__1BE81D6E`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `oqtno` | int | no | 1, 1 |  |
| 2 | `odate` | datetime | yes |  |  |
| 3 | `tname` | varchar(100) | yes |  |  |
| 4 | `oby` | varchar(50) | yes |  |  |

### dbo.orderrefrigerant

Rows: 0

Primary key: `orno` (clustered).

Referenced by:

- `invoiceref.orno` (`FK__invoiceref__orno__23BE4960`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `orno` | int | no | 1, 1 |  |
| 2 | `odate` | datetime | yes |  |  |
| 3 | `sname` | varchar(200) | yes |  |  |
| 4 | `sno` | int | yes |  |  |
| 5 | `oby` | varchar(50) | yes |  |  |
| 6 | `otype` | tinyint | yes |  | ('0') |
| 7 | `ourpno` | varchar(50) | yes |  |  |
| 8 | `ostatus` | varchar(50) | yes |  |  |
| 9 | `jobno` | int | yes |  |  |
| 10 | `rnote` | text | yes |  |  |

### dbo.orders

Rows: 15792

Primary key: `jobno` (clustered).

Foreign keys out:

- `clientcode` to `client.clientcode` (`FK__orders__clientco__117F9D94`)
- `sitecode` to `houselist.sitecode` (`FK__orders__sitecode__1273C1CD`)

Referenced by:

- `allocation.jobno` (`FK__allocatio__jobno__45F365D3`)
- `assetfault.jobno` (`FK__assetfaul__jobno__3EDC53F0`)
- `emaildone.jobno` (`FK__emaildone__jobno__74444068`)
- `engcale.jobno` (`FK__engcale__jobno__2057CCD0`)
- `gencojobtracker.jobno` (`FK__gencojobt__jobno__4DD47EBD`)
- `hireplant.jobno` (`FK__hireplant__jobno__5D95E53A`)
- `jobfile.jobno` (`FK__jobfile__jobno__0C50D423`)
- `jobform.jobno` (`FK__jobform__jobno__05F8DC4F`)
- `jobitem.jobno` (`FK__jobitem__jobno__3138400F`)
- `jobplan.jobno` (`FK__jobplan__jobno__0697FACD`)
- `jobworksheet.jobno` (`FK__jobworksh__jobno__3AA1AEB8`)
- `measurecalc.jobno` (`FK__measureca__jobno__503BEA1C`)
- `measurefile.jobno` (`FK__measurefi__jobno__567ED357`)
- `mslive.jobno` (`FK__mslive__jobno__50E5F592`)
- `mssign.jobno` (`FK__mssign__jobno__53C2623D`)
- `orderparts.jobno` (`FK__orderpart__jobno__46B27FE2`)
- `otherform.jobno` (`FK__otherform__jobno__73DA2C14`)
- `paymentvalue.jobno` (`FK__paymentva__jobno__05D8E0BE`)
- `qdmeasure.jobno` (`FK__qdmeasure__jobno__19AACF41`)
- `selectsection.jobno` (`FK__selectsec__jobno__778AC167`)
- `tomtomorder.jobno` (`FK__tomtomord__jobno__603D47BB`)
- `usematerials.jobno` (`FK__usemateri__jobno__2739D489`)
- `userefrigerant.jobno` (`FK__userefrig__jobno__46486B8E`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jobno` | int | no | 30, 1 |  |
| 2 | `date` | datetime | yes |  |  |
| 3 | `rjcdate` | datetime | yes |  |  |
| 4 | `clientcode` | varchar(50) | yes |  |  |
| 5 | `sitecode` | varchar(50) | yes |  |  |
| 6 | `cpdate` | datetime | yes |  |  |
| 7 | `contactname` | varchar(50) | yes |  |  |
| 8 | `description` | text | yes |  |  |
| 9 | `categorycode` | int | yes |  |  |
| 10 | `deadline` | datetime | yes |  |  |
| 11 | `orgdline` | datetime | yes |  |  |
| 12 | `offsitecode` | int | yes |  | ('0') |
| 13 | `orderno` | varchar(30) | yes |  |  |
| 14 | `faultref` | varchar(30) | yes |  |  |
| 15 | `calltakenby` | varchar(50) | yes |  |  |
| 16 | `manualinvoice` | tinyint | yes |  | ('0') |
| 17 | `comments` | text | yes |  |  |
| 18 | `wcomments` | text | yes |  |  |
| 19 | `username` | varchar(50) | yes |  |  |
| 20 | `mvdone` | tinyint | yes |  | ('0') |
| 21 | `recall` | tinyint | yes |  | ('0') |
| 22 | `pjobno` | int | yes |  |  |
| 23 | `waitingpw` | tinyint | yes |  | ('0') |
| 24 | `chase` | tinyint | yes |  | ('0') |
| 25 | `qonly` | tinyint | yes |  | ('0') |
| 26 | `acr` | tinyint | yes |  | ('0') |
| 27 | `qdone` | tinyint | yes |  | ('0') |
| 28 | `partsin` | tinyint | yes |  | ('0') |
| 29 | `partsorder` | tinyint | yes |  | ('0') |
| 30 | `partsdate` | datetime | yes |  |  |
| 31 | `phired` | tinyint | yes |  | ('0') |
| 32 | `fsttimefix` | tinyint | yes |  | ('0') |
| 33 | `wono` | varchar(50) | yes |  |  |
| 34 | `pccomments` | text | yes |  |  |
| 35 | `monly` | tinyint | yes |  | ('0') |
| 36 | `certdone` | tinyint | yes |  | ('0') |
| 37 | `ewnote` | text | yes |  |  |
| 38 | `alapprove` | tinyint | yes |  | ('0') |
| 39 | `jobtype` | int | yes |  | ('0') |
| 40 | `vtno` | int | yes |  | ('0') |
| 41 | `jlock` | tinyint | yes |  | ('0') |
| 42 | `juser` | varchar(50) | yes |  |  |
| 43 | `jlono` | int | yes |  | ('0') |
| 44 | `convalue` | decimal(10,2) | yes |  |  |
| 45 | `jmarkup` | decimal(10,2) | yes |  |  |
| 46 | `fvisit` | datetime | yes |  |  |
| 47 | `crb` | tinyint | yes |  | ('0') |
| 48 | `nidtype` | varchar(100) | yes |  |  |
| 49 | `jcreal` | tinyint | yes |  | ('0') |
| 50 | `jcdate` | datetime | yes |  |  |
| 51 | `ndap` | tinyint | yes |  | ('0') |
| 52 | `ndapsend` | tinyint | yes |  | ('0') |
| 53 | `ndapwho` | varchar(50) | yes |  |  |
| 54 | `ndapsdate` | datetime | yes |  |  |
| 55 | `jrbyq` | tinyint | yes |  | ('0') |
| 56 | `awnote` | varchar(250) | yes |  |  |
| 57 | `qdqdone` | tinyint | yes |  | ('0') |
| 58 | `joref` | varchar(100) | yes |  |  |
| 59 | `cloc` | tinyint | yes |  | ('0') |
| 60 | `ppmiv` | tinyint | yes |  | ('0') |
| 61 | `cppm` | tinyint | yes |  | ('0') |
| 62 | `ivjobno` | int | yes |  |  |
| 63 | `ntjobno` | int | yes |  |  |
| 64 | `webjob` | tinyint | yes |  | ('0') |
| 65 | `rpby` | varchar(50) | yes |  |  |
| 66 | `ppmpvt` | tinyint | yes |  | ('0') |
| 67 | `wandt` | tinyint | yes |  | ('0') |
| 68 | `qofwork` | tinyint | yes |  | ('0') |
| 69 | `sfsystem` | tinyint | yes |  | ('0') |
| 70 | `socontact` | tinyint | yes |  | ('0') |
| 71 | `aontime` | tinyint | yes |  |  |
| 72 | `wcdate` | datetime | yes |  |  |
| 73 | `oldwq` | varchar(50) | yes |  |  |
| 74 | `satser` | tinyint | yes |  |  |
| 75 | `metexp` | tinyint | yes |  |  |
| 76 | `srvdue` | tinyint | yes |  |  |
| 77 | `repsnt` | tinyint | yes |  |  |
| 78 | `noofeng` | varchar(50) | yes |  |  |
| 79 | `ivclientcode` | varchar(50) | yes |  |  |
| 80 | `mandays` | varchar(50) | yes |  |  |
| 81 | `momgmm` | varchar(50) | yes |  |  |
| 82 | `lvisit` | tinyint | no |  | ('0') |
| 83 | `ppmvtype` | tinyint | no |  | ('0') |
| 84 | `ppmcnum` | int | yes |  |  |
| 85 | `ppmcvsel` | int | yes |  |  |
| 86 | `acrperson` | varchar(100) | yes |  |  |
| 87 | `acrtask` | varchar(500) | yes |  |  |
| 88 | `conler` | varchar(50) | yes |  |  |
| 89 | `prepdone` | tinyint | no |  | ('0') |
| 90 | `qrname` | varchar(50) | yes |  |  |
| 91 | `epdone` | tinyint | no |  | ('0') |
| 92 | `opemail` | varchar(100) | yes |  |  |
| 93 | `optele` | varchar(50) | yes |  |  |
| 94 | `asbyes` | varchar(20) | yes |  |  |
| 95 | `asbyes1` | varchar(20) | yes |  |  |
| 96 | `asbyes2` | varchar(20) | yes |  |  |
| 97 | `cstatus` | varchar(250) | yes |  |  |
| 98 | `salesp` | varchar(250) | yes |  |  |
| 99 | `oppunitno` | varchar(250) | yes |  |  |
| 100 | `orderref` | varchar(200) | yes |  |  |
| 101 | `jivnote` | varchar(1000) | yes |  |  |
| 102 | `codedes` | varchar(1000) | yes |  |  |
| 103 | `charget` | varchar(50) | yes |  |  |
| 104 | `engrate` | varchar(50) | yes |  |  |
| 105 | `qpress` | tinyint | no |  | ('0') |
| 106 | `upliftsreq` | tinyint | no |  | ('0') |
| 107 | `upliftamt` | decimal(16,2) | yes |  |  |
| 108 | `intcomments` | text | yes |  |  |
| 109 | `upliftdate` | datetime | yes |  |  |
| 110 | `siteissue` | tinyint | no |  | ('0') |
| 111 | `siteclear` | tinyint | no |  | ('0') |
| 112 | `cleardt` | datetime | yes |  |  |
| 113 | `jwtosend` | tinyint | no |  | ('0') |
| 114 | `jwsend` | tinyint | no |  | ('0') |
| 115 | `jwsenddate` | datetime | yes |  |  |
| 116 | `jwjobid` | varchar(50) | yes |  |  |
| 117 | `jwcancel` | tinyint | no |  | ('0') |
| 118 | `jwcanceldate` | datetime | yes |  |  |
| 119 | `jwcanceltosend` | tinyint | no |  | ('0') |
| 120 | `jwsaveerror` | varchar(100) | yes |  |  |
| 121 | `jwcancelerror` | varchar(100) | yes |  |  |
| 122 | `jwreceived` | tinyint | no |  | ('0') |
| 123 | `jwreceiveddone` | tinyint | no |  | ('0') |
| 124 | `jwreceiveddate` | datetime | yes |  |  |
| 125 | `supervisor` | varchar(50) | yes |  |  |
| 126 | `surveyor` | varchar(50) | yes |  |  |
| 127 | `archivedjob` | tinyint | no |  | ('0') |
| 128 | `atmjob` | int | yes |  | ('0') |
| 129 | `atmtaskid` | varchar(50) | yes |  |  |
| 130 | `atmrepairtype` | varchar(50) | yes |  |  |
| 131 | `atmcase` | varchar(50) | yes |  |  |
| 132 | `atmmoved` | tinyint | no |  | ('0') |
| 133 | `atmtosend` | tinyint | no |  | ('0') |
| 134 | `atmsend` | tinyint | no |  | ('0') |
| 135 | `atmsenddate` | datetime | yes |  |  |
| 136 | `ppminvmonth` | int | yes |  |  |
| 137 | `firstmeasuredate` | datetime | yes |  |  |
| 138 | `ppmnummonth` | int | yes |  |  |
| 139 | `japprovedate` | datetime | yes |  |  |
| 140 | `atmcomments` | varchar(500) | yes |  |  |
| 141 | `dwacktosend` | tinyint | no |  | ('0') |
| 142 | `dwacksend` | tinyint | no |  | ('0') |
| 143 | `dwacksenddate` | datetime | yes |  |  |
| 144 | `antsdate` | datetime | yes |  |  |
| 145 | `measurenote` | varchar(1000) | yes |  |  |
| 146 | `vehjob` | tinyint | no |  | ('0') |
| 147 | `eqjob` | tinyint | no |  | ('0') |
| 148 | `jobrecwcr` | datetime | yes |  |  |
| 149 | `pmanager` | varchar(200) | yes |  |  |
| 150 | `pcodinator` | varchar(200) | yes |  |  |
| 151 | `profitper` | decimal(10,2) | yes |  |  |
| 152 | `antcomdate` | datetime | yes |  |  |
| 153 | `awtsurvey` | tinyint | no |  | ('0') |
| 154 | `emailapdone` | tinyint | no |  | ('0') |
| 155 | `emailappdate` | datetime | yes |  |  |
| 156 | `ivstatus` | tinyint | yes |  |  |
| 157 | `ivchdate` | datetime | yes |  |  |
| 158 | `ivapuser` | varchar(100) | yes |  |  |
| 159 | `qonlys` | tinyint | no |  | ('0') |
| 160 | `mawr` | tinyint | no |  | ('0') |
| 161 | `cancelq` | tinyint | no |  | ('0') |
| 162 | `ramissdate` | datetime | yes |  |  |
| 163 | `ramauthdate` | datetime | yes |  |  |
| 164 | `fcdate` | datetime | yes |  |  |
| 165 | `wcdates` | datetime | yes |  |  |
| 166 | `acrs` | tinyint | no |  | ('0') |
| 167 | `needdate` | tinyint | no |  | ('0') |
| 168 | `ppmservice` | tinyint | no |  | ('0') |
| 169 | `orderno2` | varchar(30) | yes |  |  |
| 170 | `orderno3` | varchar(30) | yes |  |  |
| 171 | `cpofcontact` | varchar(200) | yes |  |  |
| 172 | `jobreq` | tinyint | no |  | ('0') |
| 173 | `reqbyloc` | int | no |  | ('1000') |
| 174 | `reqbyuser` | varchar(200) | yes |  |  |
| 175 | `reqdate` | datetime | yes |  |  |
| 176 | `reqapproved` | tinyint | no |  | ('0') |
| 177 | `reqappby` | varchar(200) | yes |  |  |
| 178 | `reqappdate` | datetime | yes |  |  |
| 179 | `reqjobdone` | tinyint | no |  | ('0') |
| 180 | `oonedone` | tinyint | no |  | ('0') |
| 181 | `ooneamt` | varchar(500) | yes |  |  |
| 182 | `otwodone` | tinyint | no |  | ('0') |
| 183 | `otwoamt` | varchar(500) | yes |  |  |
| 184 | `othreedone` | tinyint | no |  | ('0') |
| 185 | `othreeamt` | varchar(500) | yes |  |  |
| 186 | `orderno4` | varchar(30) | yes |  |  |
| 187 | `orderno5` | varchar(30) | yes |  |  |
| 188 | `orderno6` | varchar(30) | yes |  |  |
| 189 | `ofourdone` | tinyint | no |  | ('0') |
| 190 | `ofouramt` | varchar(500) | yes |  |  |
| 191 | `ofivedone` | tinyint | no |  | ('0') |
| 192 | `ofiveamt` | varchar(500) | yes |  |  |
| 193 | `osixdone` | tinyint | no |  | ('0') |
| 194 | `osixamt` | varchar(500) | yes |  |  |
| 195 | `appparts` | varchar(500) | yes |  |  |
| 196 | `etadate` | datetime | yes |  |  |
| 197 | `inttwodone` | tinyint | no |  | ('0') |
| 198 | `inttwoamt` | varchar(500) | yes |  |  |
| 199 | `intthreedone` | tinyint | no |  | ('0') |
| 200 | `intthreeamt` | varchar(500) | yes |  |  |
| 201 | `pouddone` | tinyint | no |  | ('0') |
| 202 | `orgjobtype` | int | yes |  |  |
| 203 | `notcompflag` | tinyint | no |  | ('0') |
| 204 | `chasecdate` | datetime | yes |  |  |
| 205 | `chasetask` | varchar(500) | yes |  |  |
| 206 | `chaseperson` | varchar(200) | yes |  |  |
| 207 | `timeallow` | varchar(50) | yes |  |  |
| 208 | `noofmen` | varchar(50) | yes |  |  |
| 209 | `access` | varchar(200) | yes |  |  |
| 210 | `jstatustwono` | tinyint | no |  | ('0') |
| 211 | `ramscportaldone` | tinyint | no |  | ('0') |

### dbo.otherform

Rows: 0

Primary key: `ofkey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__otherform__idno__74CE504D`)
- `jobno` to `orders.jobno` (`FK__otherform__jobno__73DA2C14`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ofkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `cdate` | datetime | yes |  |  |
| 5 | `otherform` | image | yes |  |  |

### dbo.parts

Rows: 0

Primary key: `partno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `partno` | int | no | 1, 1 |  |
| 2 | `name` | varchar(100) | yes |  |  |
| 3 | `pssid` | int | yes |  |  |
| 4 | `aclass` | varchar(20) | yes |  |  |
| 5 | `jlono` | int | yes |  | ('0') |

### dbo.partsdeletelog

Rows: 25

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `pono` | varchar(50) | yes |  |  |
| 4 | `plineno` | varchar(50) | yes |  |  |
| 5 | `ptype` | varchar(5) | yes |  |  |
| 6 | `cdate` | datetime | yes |  |  |
| 7 | `updone` | int | yes |  | ('0') |
| 8 | `luser` | varchar(50) | yes |  |  |

### dbo.partseqtype

Rows: 0

Primary key: `eqtypeno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `eqtypeno` | int | no | 1, 1 |  |
| 2 | `ptypename` | varchar(200) | yes |  |  |

### dbo.partslog

Rows: 0

Primary key: `ulkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ulkey` | int | no | 1, 1 |  |
| 2 | `lcdate` | datetime | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `info` | varchar(500) | yes |  |  |
| 5 | `herr` | tinyint | yes |  | ('0') |

### dbo.partsorder

Rows: 5401

Primary key: `partsorderno` (clustered).

Referenced by:

- `allpartsorder.partsorderno` (`FK__allpartso__parts__681373AD`)
- `invoice.partsorderno` (`FK__invoice__partsor__6DCC4D03`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `partsorderno` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `odate` | datetime | yes |  |  |
| 4 | `sname` | varchar(100) | yes |  |  |
| 5 | `ot` | varchar(10) | yes |  |  |
| 6 | `oby` | varchar(50) | yes |  |  |
| 7 | `dadd` | text | yes |  |  |
| 8 | `pref` | varchar(50) | yes |  |  |
| 9 | `pnote` | text | yes |  |  |
| 10 | `concurdone` | tinyint | yes |  | ('0') |
| 11 | `allidno` | varchar(500) | yes |  |  |
| 12 | `serviceadd` | int | yes |  | ('0') |
| 13 | `rfilename` | varchar(500) | yes |  |  |

### dbo.partsquotation

Rows: 13006

Primary key: `pqkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pqkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `sname` | varchar(100) | yes |  |  |
| 5 | `qtype` | varchar(20) | yes |  |  |
| 6 | `description` | text | yes |  |  |
| 7 | `ptype` | varchar(10) | yes |  |  |
| 8 | `partname` | varchar(100) | yes |  |  |
| 9 | `qty` | decimal(16,2) | yes |  |  |
| 10 | `price` | decimal(16,2) | yes |  |  |
| 11 | `afee` | decimal(16,2) | yes |  |  |
| 12 | `makeup100` | decimal(16,2) | yes |  |  |
| 13 | `makeupp` | decimal(16,2) | yes |  |  |
| 14 | `tvalue` | decimal(16,2) | yes |  |  |
| 15 | `cdate` | datetime | yes |  |  |
| 16 | `note` | text | yes |  |  |
| 17 | `oqno` | int | yes |  |  |
| 18 | `ofee` | decimal(16,2) | yes |  |  |
| 19 | `qmdone` | tinyint | yes |  | ('0') |
| 20 | `oqtno` | int | yes |  |  |
| 21 | `makeup2` | decimal(16,2) | yes |  | ('0') |
| 22 | `lkey` | int | yes |  |  |
| 23 | `needdo` | tinyint | yes |  | ('0') |
| 24 | `rrpval` | decimal(16,2) | yes |  |  |
| 25 | `sorparts` | tinyint | yes |  | ('0') |
| 26 | `labtype` | tinyint | yes |  | ('0') |
| 27 | `noofper` | decimal(16,2) | yes |  | ('1') |
| 28 | `disorder` | int | yes |  | ('0') |

### dbo.partsrange

Rows: 4

Primary key: `prkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `prkey` | int | no | 1, 1 |  |
| 2 | `sprice` | decimal(16,2) | yes |  |  |
| 3 | `eprice` | decimal(16,2) | yes |  |  |
| 4 | `pmup` | decimal(16,2) | yes |  |  |
| 5 | `jlono` | int | yes |  | ('0') |

### dbo.partssection

Rows: 0

Primary key: `psid` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `psid` | int | no | 1, 1 |  |
| 2 | `name` | varchar(100) | yes |  |  |
| 3 | `scode` | varchar(20) | yes |  |  |
| 4 | `jlono` | int | yes |  | ('0') |

### dbo.partssubsection

Rows: 0

Primary key: `pssid` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pssid` | int | no | 1, 1 |  |
| 2 | `name` | varchar(100) | yes |  |  |
| 3 | `psid` | int | yes |  |  |
| 4 | `jlono` | int | yes |  | ('0') |

### dbo.patfile

Rows: 0

Primary key: `pfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pfkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `aekey` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `fullpath` | varchar(500) | yes |  |  |
| 7 | `pathonly` | varchar(500) | yes |  |  |
| 8 | `patfname` | varchar(100) | yes |  |  |

### dbo.payinfo

Rows: 2

Primary key: `pinfono` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pinfono` | int | no | 1, 1 |  |
| 2 | `ystart` | datetime | yes |  |  |
| 3 | `yend` | datetime | yes |  |  |
| 4 | `msick` | decimal(16,2) | yes |  |  |
| 5 | `hday` | decimal(16,2) | yes |  |  |
| 6 | `hweek` | decimal(16,2) | yes |  |  |
| 7 | `opaycode` | varchar(50) | yes |  |  |
| 8 | `jlono` | int | yes |  | ('0') |

### dbo.paymentvalue

Rows: 14725

Primary key: `pvno` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__paymentva__jobno__05D8E0BE`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pvno` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `pvalue` | decimal(16,2) | yes |  |  |
| 4 | `valuationno` | varchar(50) | yes |  |  |
| 5 | `cdate` | datetime | yes |  |  |
| 6 | `sagedone` | tinyint | yes |  | ('0') |
| 7 | `clientcode` | varchar(50) | yes |  |  |
| 8 | `idesc` | text | yes |  |  |
| 9 | `tval` | varchar(250) | yes |  |  |
| 10 | `pvdone` | tinyint | yes |  | ('0') |
| 11 | `pterm` | varchar(250) | yes |  |  |
| 12 | `ivfoot` | text | yes |  |  |
| 13 | `vamt` | decimal(16,2) | yes |  |  |
| 14 | `ivclientcode` | varchar(50) | yes |  |  |
| 15 | `ivupdate` | datetime | yes |  |  |
| 16 | `upfilename` | varchar(100) | yes |  |  |
| 17 | `orderno` | varchar(30) | yes |  |  |

### dbo.permittowork

Rows: 0

Primary key: `jpwkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jpwkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `aekey` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `ccdate` | datetime | yes |  |  |
| 6 | `jpwform` | image | yes |  |  |
| 7 | `jpwtype` | tinyint | yes |  | ('0') |

### dbo.pivlimit

Rows: 1

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `lmval` | decimal(16,2) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.poorders

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `pono` | varchar(50) | yes |  |  |
| 4 | `ptype` | int | yes |  |  |
| 5 | `amt` | varchar(500) | yes |  |  |
| 6 | `done` | tinyint | no |  | ('0') |

### dbo.ppmlink

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `ivjobno` | int | yes |  |  |
| 3 | `cjobno` | int | yes |  |  |

### dbo.ppmtask

Rows: 0

Primary key: `ppmkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ppmkey` | int | no | 1, 1 |  |
| 2 | `ptid` | int | yes |  |  |
| 3 | `noofvisit` | int | yes |  |  |
| 4 | `visitno` | int | yes |  |  |
| 5 | `ppmlist` | text | yes |  |  |

### dbo.product

Rows: 110

Primary key: `pid` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pid` | int | no | 1, 1 |  |
| 2 | `name` | varchar(250) | yes |  |  |

### dbo.producttype

Rows: 0

Primary key: `ptid` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ptid` | int | no | 1, 1 |  |
| 2 | `ptname` | varchar(250) | yes |  |  |
| 3 | `pid` | int | yes |  |  |

### dbo.qdmeasure

Rows: 15419

Primary key: `qdmono` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__qdmeasure__jobno__19AACF41`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `qdmono` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `mdate` | datetime | yes |  |  |
| 4 | `mvalue` | decimal(16,2) | yes |  |  |
| 5 | `mfile` | varchar(200) | yes |  |  |
| 6 | `bowcisno` | varchar(100) | yes |  |  |
| 7 | `surveyor` | varchar(100) | yes |  |  |
| 8 | `approve` | tinyint | yes |  | ('1') |
| 9 | `pdesc` | text | yes |  |  |
| 10 | `ncode` | varchar(50) | yes |  |  |
| 11 | `pvno` | int | yes |  |  |
| 12 | `aov` | decimal(16,2) | yes |  |  |
| 13 | `description` | text | yes |  |  |
| 14 | `afee` | decimal(16,2) | yes |  |  |
| 15 | `lhour` | decimal(16,2) | yes |  |  |
| 16 | `lrate` | decimal(16,2) | yes |  |  |
| 17 | `lmakeup100` | decimal(16,2) | yes |  |  |
| 18 | `lmakeupp` | decimal(16,2) | yes |  |  |
| 19 | `ltotal` | decimal(16,2) | yes |  |  |
| 20 | `pprice` | decimal(16,2) | yes |  |  |
| 21 | `pmakeup100` | decimal(16,2) | yes |  |  |
| 22 | `pmakeupp` | decimal(16,2) | yes |  |  |
| 23 | `ptotal` | decimal(16,2) | yes |  |  |
| 24 | `ftotal` | decimal(16,2) | yes |  |  |
| 25 | `cdate` | datetime | yes |  |  |
| 26 | `cmarkup` | decimal(16,2) | yes |  |  |
| 27 | `svat` | decimal(16,2) | yes |  |  |
| 28 | `svcode` | varchar(50) | yes |  |  |
| 29 | `pfno` | varchar(50) | yes |  |  |
| 30 | `aftotal` | decimal(16,2) | yes |  |  |
| 31 | `vamt` | decimal(16,2) | yes |  |  |
| 32 | `qty` | decimal(16,2) | yes |  |  |
| 33 | `pcprice` | decimal(16,2) | yes |  |  |
| 34 | `partno` | varchar(50) | yes |  |  |
| 35 | `appby` | varchar(50) | yes |  |  |
| 36 | `uprice` | decimal(16,2) | yes |  |  |
| 37 | `uqty` | decimal(16,2) | yes |  |  |
| 38 | `idno` | int | yes |  |  |
| 39 | `wtname` | varchar(200) | yes |  |  |
| 40 | `umname` | varchar(200) | yes |  |  |
| 41 | `uplddone` | tinyint | yes |  | ('0') |
| 42 | `loname` | varchar(250) | yes |  |  |
| 43 | `docis` | tinyint | yes |  | ('0') |
| 44 | `cisper` | decimal(16,2) | yes |  |  |
| 45 | `orderno` | varchar(30) | yes |  |  |

### dbo.qdquote

Rows: 0

Primary key: `qdqono` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `qdqono` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `qdate` | datetime | yes |  |  |
| 4 | `qvalue` | decimal(16,2) | yes |  |  |
| 5 | `qfile` | varchar(250) | yes |  |  |

### dbo.quotationrate

Rows: 0

Primary key: `pqrkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pqrkey` | int | no | 1, 1 |  |
| 2 | `qty` | decimal(16,2) | yes |  |  |
| 3 | `price` | decimal(16,2) | yes |  |  |
| 4 | `oqno` | int | yes |  |  |
| 5 | `afee` | decimal(16,2) | yes |  |  |
| 6 | `makeup100` | decimal(16,2) | yes |  |  |
| 7 | `makeupp` | decimal(16,2) | yes |  |  |
| 8 | `tvalue` | decimal(16,2) | yes |  |  |
| 9 | `description` | text | yes |  |  |
| 10 | `cdate` | datetime | yes |  |  |
| 11 | `note` | text | yes |  |  |
| 12 | `ofee` | decimal(16,2) | yes |  |  |

### dbo.quotetemplate

Rows: 0

Primary key: `qtkey` (clustered).

Foreign keys out:

- `oqtno` to `orderquotetemplate.oqtno` (`FK__quotetemp__oqtno__1BE81D6E`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `qtkey` | int | no | 1, 1 |  |
| 2 | `oqtno` | int | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |

### dbo.raanswers

Rows: 0

Primary key: `raakey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `raakey` | int | no | 1, 1 |  |
| 2 | `raqkey` | int | yes |  |  |
| 3 | `raanswer` | text | yes |  |  |
| 4 | `anstype` | int | yes |  |  |
| 5 | `jobno` | int | yes |  |  |

### dbo.rajobvisit

Rows: 0

Primary key: `rajvkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rajvkey` | int | no | 1, 1 |  |
| 2 | `jvkey` | int | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `raqkey` | int | yes |  |  |
| 5 | `raakey` | int | yes |  |  |

### dbo.raquestions

Rows: 0

Primary key: `raqkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `raqkey` | int | no | 1, 1 |  |
| 2 | `raquestion` | text | yes |  |  |
| 3 | `anstype` | int | yes |  |  |
| 4 | `jobno` | int | yes |  |  |

### dbo.regionlist

Rows: 0

Primary key: `rlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rlkey` | int | no | 1, 1 |  |
| 2 | `rname` | varchar(100) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.regionlistgc

Rows: 0

Primary key: `rlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rlkey` | int | no | 1, 1 |  |
| 2 | `rnamegc` | varchar(100) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.regsetting

Rows: 1

Primary key: `regkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `regkey` | int | no | 1, 1 |  |
| 2 | `filepath` | varchar(200) | yes |  |  |
| 3 | `licweb` | varchar(200) | yes |  |  |
| 4 | `dbbackup` | varchar(200) | yes |  |  |
| 5 | `sqlname` | varchar(100) | yes |  |  |
| 6 | `eserver` | varchar(100) | yes |  |  |
| 7 | `euser` | varchar(100) | yes |  |  |
| 8 | `epassword` | varchar(100) | yes |  |  |
| 9 | `eaddress` | varchar(100) | yes |  |  |
| 10 | `eserverport` | varchar(10) | yes |  |  |
| 11 | `jpgpath` | varchar(200) | yes |  |  |
| 12 | `newwebpen` | varchar(200) | yes |  |  |
| 13 | `newwebpentime` | varchar(200) | yes |  |  |
| 14 | `webuser` | varchar(50) | yes |  |  |
| 15 | `webpassword` | varchar(50) | yes |  |  |
| 16 | `quatationpath` | varchar(200) | yes |  |  |
| 17 | `droppath` | varchar(200) | yes |  |  |
| 18 | `droppath2` | varchar(200) | yes |  |  |
| 19 | `udform` | varchar(200) | yes |  |  |
| 20 | `screenw` | varchar(50) | yes |  |  |
| 21 | `screenh` | varchar(50) | yes |  |  |
| 22 | `tagurl` | varchar(200) | yes |  |  |
| 23 | `jlono` | int | yes |  | ('0') |
| 24 | `emailssl` | int | yes |  | ('0') |

### dbo.rejectedlist

Rows: 0

Primary key: `rlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rlkey` | int | no | 1, 1 |  |
| 2 | `rejectlist` | varchar(500) | yes |  |  |
| 3 | `rejtype` | tinyint | no |  | ('0') |

### dbo.rpuserpostcode

Rows: 0

Primary key: `rpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rpkey` | int | no | 1, 1 |  |
| 2 | `pcode` | varchar(50) | yes |  |  |
| 3 | `scode` | varchar(20) | yes |  |  |
| 4 | `des` | varchar(200) | yes |  |  |
| 5 | `rpuser` | varchar(100) | yes |  |  |

### dbo.sagecodelist

Rows: 0

Primary key: `sckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sckey` | int | no | 1, 1 |  |
| 2 | `ndesc` | varchar(250) | yes |  |  |
| 3 | `sncode` | varchar(250) | yes |  |  |
| 4 | `snctype` | tinyint | yes |  | ('0') |

### dbo.sagesectioncode

Rows: 0

Primary key: `ssckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ssckey` | int | no | 1, 1 |  |
| 2 | `clientcode` | varchar(50) | yes |  |  |
| 3 | `sscode` | varchar(20) | yes |  |  |
| 4 | `matcode` | varchar(50) | yes |  |  |
| 5 | `labcode` | varchar(50) | yes |  |  |
| 6 | `matcodem` | varchar(50) | yes |  |  |
| 7 | `labcodem` | varchar(50) | yes |  |  |
| 8 | `salecode` | varchar(50) | yes |  |  |
| 9 | `salecodem` | varchar(50) | yes |  |  |

### dbo.sagesetup200

Rows: 0

Primary key: `sskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sskey` | int | no | 1, 1 |  |
| 2 | `jobtype` | int | yes |  |  |
| 3 | `snominal` | varchar(50) | yes |  |  |
| 4 | `scostcen` | varchar(50) | yes |  |  |
| 5 | `sdept` | varchar(50) | yes |  |  |
| 6 | `pnominal` | varchar(50) | yes |  |  |
| 7 | `pcostcen` | varchar(50) | yes |  |  |
| 8 | `pdept` | varchar(50) | yes |  |  |
| 9 | `ppartnc` | varchar(50) | yes |  |  |
| 10 | `scpartnc` | varchar(50) | yes |  |  |
| 11 | `hpartnc` | varchar(50) | yes |  |  |
| 12 | `sclabnc` | varchar(50) | yes |  |  |
| 13 | `cisscpartnc` | varchar(50) | yes |  |  |
| 14 | `cissclabnc` | varchar(50) | yes |  |  |

### dbo.sealparts

Rows: 0

Primary key: `fspkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fspkey` | int | no | 1, 1 |  |
| 2 | `sealno` | varchar(100) | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `qty` | decimal(16,2) | yes |  |  |
| 5 | `price` | decimal(16,2) | yes |  |  |
| 6 | `lprice` | decimal(16,2) | yes |  |  |
| 7 | `pwid` | decimal(16,2) | yes |  |  |
| 8 | `plen` | decimal(16,2) | yes |  |  |
| 9 | `pdep` | decimal(16,2) | yes |  |  |
| 10 | `pdim` | decimal(16,2) | yes |  |  |
| 11 | `wdes` | varchar(500) | yes |  |  |
| 12 | `inputtype` | int | yes |  | ('0') |
| 13 | `path` | varchar(100) | yes |  |  |
| 14 | `photo1` | varchar(100) | yes |  |  |
| 15 | `cdate` | datetime | yes |  |  |
| 16 | `exzone` | int | yes |  | ('0') |

### dbo.section

Rows: 14

Primary key: `scode` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `scode` | varchar(20) | no |  |  |
| 2 | `sname` | varchar(50) | yes |  |  |
| 3 | `pncode` | varchar(50) | yes |  |  |
| 4 | `sncode` | varchar(50) | yes |  |  |
| 5 | `dpno` | varchar(10) | yes |  |  |
| 6 | `jlono` | int | yes |  | ('0') |
| 7 | `hidethis` | int | yes |  | ('0') |
| 8 | `pncodeh` | varchar(50) | yes |  |  |

### dbo.selectsection

Rows: 15828

Primary key: `sskey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__selectsec__jobno__778AC167`)
- `sscode` to `subsection.sscode` (`FK__selectsec__sscod__787EE5A0`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sskey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `sscode` | varchar(20) | yes |  |  |
| 4 | `jdone` | tinyint | yes |  | ('0') |
| 5 | `qdone` | tinyint | yes |  | ('0') |
| 6 | `ja` | tinyint | yes |  | ('0') |
| 7 | `jnote` | text | yes |  |  |

### dbo.sfolder

Rows: 6

Primary key: `fno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fno` | int | no | 1, 1 |  |
| 2 | `fname` | varchar(250) | yes |  |  |
| 3 | `jlono` | int | yes |  | ('0') |

### dbo.shiftroom

Rows: 1

Primary key: `skey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `skey` | int | no | 1, 1 |  |
| 2 | `noshift` | int | yes |  |  |
| 3 | `noroom` | int | yes |  |  |

### dbo.siteactionlist

Rows: 0

Primary key: `ckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ckey` | int | no | 1, 1 |  |
| 2 | `alkey` | int | yes |  |  |
| 3 | `sitecode` | varchar(50) | yes |  |  |
| 4 | `actype` | int | yes |  | ('0') |

### dbo.sitera

Rows: 0

Primary key: `rskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rskey` | int | no | 1, 1 |  |
| 2 | `satype` | varchar(100) | yes |  |  |
| 3 | `rapriority` | varchar(50) | yes |  |  |
| 4 | `rareq` | varchar(500) | yes |  |  |
| 5 | `rarecdate` | datetime | yes |  |  |
| 6 | `raexp` | datetime | yes |  |  |
| 7 | `rsresolved` | tinyint | yes |  | ('0') |
| 8 | `racomments` | varchar(500) | yes |  |  |
| 9 | `raresdate` | datetime | yes |  |  |
| 10 | `nextredate` | datetime | yes |  |  |
| 11 | `sitecode` | varchar(50) | yes |  |  |

### dbo.sjobfile

Rows: 1

Primary key: `jfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jfkey` | int | no | 1, 1 |  |
| 2 | `sitecode` | varchar(50) | yes |  |  |
| 3 | `cptime` | datetime | yes |  |  |
| 4 | `fpath` | varchar(100) | yes |  |  |
| 5 | `fname` | varchar(100) | yes |  |  |
| 6 | `fno` | int | yes |  |  |

### dbo.status

Rows: 14731

Primary key: `jobno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `jobno` | int | no |  |  |
| 2 | `date` | datetime | yes |  |  |
| 3 | `jobcheck` | tinyint | yes |  | ('0') |
| 4 | `jobmfm` | tinyint | yes |  | ('0') |
| 5 | `jobc` | datetime | yes |  |  |
| 6 | `info` | varchar(50) | yes |  |  |
| 7 | `whomv` | varchar(100) | yes |  |  |
| 8 | `whomvdate` | varchar(100) | yes |  |  |

### dbo.stockrefrigerant

Rows: 0

Primary key: `srefkey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__stockrefri__idno__3CBF0154`)

Referenced by:

- `userefrigerant.srefkey` (`FK__userefrig__srefk__473C8FC7`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `srefkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `refrigsno` | varchar(250) | yes |  |  |
| 4 | `refrigtype` | varchar(250) | yes |  |  |
| 5 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 6 | `adddate` | datetime | yes |  |  |
| 7 | `vehreg` | varchar(50) | yes |  |  |
| 8 | `ourpno` | varchar(50) | yes |  |  |
| 9 | `jobno` | int | yes |  |  |
| 10 | `rfkey` | int | yes |  |  |

### dbo.subasset

Rows: 0

Primary key: `subassetc` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `subassetc` | int | no | 1, 1 |  |
| 2 | `subassetid` | varchar(50) | yes |  |  |
| 3 | `sitecode` | varchar(50) | yes |  |  |
| 4 | `subassetno` | varchar(50) | yes |  |  |
| 5 | `equipment` | varchar(250) | yes |  |  |
| 6 | `manufacturer` | varchar(250) | yes |  |  |
| 7 | `model` | varchar(250) | yes |  |  |
| 8 | `serialno` | varchar(100) | yes |  |  |
| 9 | `yearmf` | datetime | yes |  |  |
| 10 | `instdate` | datetime | yes |  |  |
| 11 | `location` | varchar(200) | yes |  |  |
| 12 | `condition` | int | yes |  |  |
| 13 | `refrigerant` | varchar(250) | yes |  |  |
| 14 | `comment` | text | yes |  |  |
| 15 | `path` | varchar(100) | yes |  |  |
| 16 | `fpath` | varchar(100) | yes |  |  |
| 17 | `cin` | tinyint | yes |  | ('1') |
| 18 | `wflag` | tinyint | yes |  | ('0') |
| 19 | `wedate` | datetime | yes |  |  |
| 20 | `assetc` | int | yes |  |  |

### dbo.subsection

Rows: 19

Primary key: `sscode` (clustered).

Referenced by:

- `operative.sscode` (`FK__operative__sscod__72C60C4A`)
- `selectsection.sscode` (`FK__selectsec__sscod__787EE5A0`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sscode` | varchar(20) | no |  |  |
| 2 | `scode` | varchar(20) | yes |  |  |
| 3 | `ssname` | varchar(50) | yes |  |  |
| 4 | `jlono` | int | yes |  | ('0') |
| 5 | `hidethis` | int | yes |  | ('0') |
| 6 | `noallocation` | int | yes |  | ('0') |

### dbo.supcatlink

Rows: 0

Primary key: `stkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `stkey` | int | no | 1, 1 |  |
| 2 | `sno` | int | yes |  |  |
| 3 | `supcat` | varchar(200) | yes |  |  |
| 4 | `cattag` | varchar(200) | yes |  |  |

### dbo.supcatlist

Rows: 0

Primary key: `sckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sckey` | int | no | 1, 1 |  |
| 2 | `supcat` | varchar(200) | yes |  |  |

### dbo.supexpdate

Rows: 0

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `alkey` | int | yes |  |  |
| 3 | `sno` | int | yes |  |  |
| 4 | `expdate` | datetime | yes |  |  |
| 5 | `senddate` | datetime | yes |  |  |
| 6 | `plivalue` | decimal(16,2) | yes |  |  |
| 7 | `actype` | int | yes |  | ('0') |

### dbo.supexplog

Rows: 0

Primary key: `ekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ekey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `sno` | int | yes |  |  |
| 4 | `name` | varchar(500) | yes |  |  |
| 5 | `des` | varchar(500) | yes |  |  |

### dbo.supmanager

Rows: 0

Primary key: `manid` (clustered).

Referenced by:

- `managereng.manid` (`FK__manageren__manid__68687968`)
- `managersite.manid` (`FK__managersi__manid__61BB7BD9`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `manid` | int | no | 1, 1 |  |
| 2 | `mname` | varchar(200) | yes |  |  |
| 3 | `tel` | varchar(100) | yes |  |  |
| 4 | `pass` | varchar(20) | yes |  |  |

### dbo.suppliers

Rows: 332

Primary key: `sno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `sno` | int | no | 1, 1 |  |
| 2 | `name` | varchar(100) | yes |  |  |
| 3 | `address` | varchar(100) | yes |  |  |
| 4 | `town` | varchar(100) | yes |  |  |
| 5 | `county` | varchar(100) | yes |  |  |
| 6 | `postcode` | varchar(50) | yes |  |  |
| 7 | `tel` | varchar(50) | yes |  |  |
| 8 | `fax` | varchar(50) | yes |  |  |
| 9 | `accno` | varchar(50) | yes |  |  |
| 10 | `sagesc` | varchar(50) | yes |  |  |
| 11 | `semail` | varchar(100) | yes |  |  |
| 12 | `info` | text | yes |  |  |
| 13 | `jlono` | int | yes |  | ('0') |
| 14 | `sdesc` | text | yes |  |  |
| 15 | `supstatus` | varchar(250) | yes |  |  |
| 16 | `okcis` | tinyint | yes |  | ('0') |
| 17 | `cisval` | varchar(50) | yes |  |  |
| 18 | `iscis` | tinyint | yes |  | ('0') |
| 19 | `dissupp` | tinyint | yes |  | ('0') |
| 20 | `adduname` | varchar(50) | yes |  |  |
| 21 | `updateuname` | varchar(50) | yes |  |  |
| 22 | `nonconf` | tinyint | yes |  | ('0') |
| 23 | `supflag` | tinyint | yes |  | ('0') |
| 24 | `pqqdate` | datetime | yes |  |  |
| 25 | `supcat` | varchar(200) | yes |  |  |
| 26 | `insexp` | datetime | yes |  |  |
| 27 | `wclexp` | datetime | yes |  |  |
| 28 | `evscore` | varchar(20) | yes |  |  |
| 29 | `payterms` | int | yes |  |  |
| 30 | `tescoapp` | tinyint | yes |  | ('0') |
| 31 | `engorder` | tinyint | yes |  | ('0') |

### dbo.supsubcatlist

Rows: 0

Primary key: `ssckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ssckey` | int | no | 1, 1 |  |
| 2 | `sckey` | int | yes |  |  |
| 3 | `supsubcat` | varchar(200) | yes |  |  |

### dbo.suritempre

Rows: 0

Primary key: `vlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vlkey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `idno` | int | yes |  |  |
| 5 | `ourpno` | varchar(50) | yes |  |  |
| 6 | `qty` | decimal(16,2) | yes |  |  |
| 7 | `price` | decimal(16,2) | yes |  |  |
| 8 | `lprice` | decimal(16,2) | yes |  |  |
| 9 | `pwid` | decimal(16,2) | yes |  |  |
| 10 | `plen` | decimal(16,2) | yes |  |  |
| 11 | `pdep` | decimal(16,2) | yes |  |  |
| 12 | `pdim` | decimal(16,2) | yes |  |  |
| 13 | `wdes` | varchar(500) | yes |  |  |
| 14 | `sealno` | varchar(100) | yes |  |  |
| 15 | `inputtype` | int | yes |  | ('0') |
| 16 | `path` | varchar(100) | yes |  |  |
| 17 | `photo1` | varchar(100) | yes |  |  |
| 18 | `exzone` | int | yes |  | ('0') |
| 19 | `vskey` | int | yes |  |  |
| 20 | `alleng` | varchar(1000) | yes |  |  |
| 21 | `stime` | datetime | yes |  |  |
| 22 | `etime` | datetime | yes |  |  |
| 23 | `tonsite` | int | yes |  | ('0') |
| 24 | `wcoper` | tinyint | yes |  | ('0') |
| 25 | `wcass` | tinyint | yes |  | ('0') |
| 26 | `idrill` | tinyint | yes |  | ('0') |
| 27 | `adrill` | tinyint | yes |  | ('0') |
| 28 | `aswrig` | tinyint | yes |  | ('0') |
| 29 | `hlwork` | tinyint | yes |  | ('0') |
| 30 | `dustcon` | tinyint | yes |  | ('0') |
| 31 | `pstype` | int | yes |  | ('0') |
| 32 | `loc` | varchar(100) | yes |  |  |
| 33 | `imat` | varchar(100) | yes |  |  |
| 34 | `rrprate` | decimal(16,2) | yes |  |  |
| 35 | `bitbroken` | tinyint | yes |  | ('0') |
| 36 | `mmused` | decimal(16,2) | yes |  |  |
| 37 | `mmusedper` | varchar(100) | yes |  |  |
| 38 | `aekey` | int | yes |  |  |
| 39 | `mesment` | varchar(50) | yes |  |  |
| 40 | `repair` | tinyint | yes |  | ('0') |
| 41 | `fpdetail` | varchar(100) | yes |  |  |
| 42 | `cref` | varchar(50) | yes |  |  |
| 43 | `frating` | varchar(10) | yes |  |  |
| 44 | `acrating` | varchar(10) | yes |  |  |
| 45 | `fpother1` | varchar(100) | yes |  |  |
| 46 | `fpother2` | varchar(100) | yes |  |  |
| 47 | `addfinish` | tinyint | yes |  | ('0') |

### dbo.tablelock

Rows: 1

Primary key: `tlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tlkey` | int | no | 1, 1 |  |
| 2 | `tname` | varchar(50) | yes |  |  |
| 3 | `tuser` | varchar(50) | yes |  |  |
| 4 | `computer` | varchar(50) | yes |  |  |
| 5 | `flag` | tinyint | yes |  | ('0') |

### dbo.tbappuserlic

Rows: 1

Primary key: `appkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `appkey` | int | no | 1, 1 |  |
| 2 | `utkey` | varchar(500) | yes |  |  |
| 3 | `alluser` | text | yes |  |  |
| 4 | `jlono` | int | yes |  | ('0') |

### dbo.team

Rows: 0

Primary key: `tid` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__team__idno__592635D8`)

Referenced by:

- `managersite.tid` (`FK__managersite__tid__63A3C44B`)
- `teameng.tid` (`FK__teameng__tid__6E2152BE`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tid` | int | no | 1, 1 |  |
| 2 | `tname` | varchar(200) | yes |  |  |
| 3 | `idno` | int | yes |  |  |

### dbo.teameng

Rows: 0

Primary key: `tekey` (clustered).

Foreign keys out:

- `idno` to `employee.idno` (`FK__teameng__idno__6F1576F7`)
- `tid` to `team.tid` (`FK__teameng__tid__6E2152BE`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tekey` | int | no | 1, 1 |  |
| 2 | `tid` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |

### dbo.timesheet

Rows: 941

Primary key: `keyno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `keyno` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `arrivaltime` | datetime | yes |  |  |
| 5 | `departuretime` | datetime | yes |  |  |
| 6 | `path` | varchar(100) | yes |  |  |
| 7 | `fpath` | varchar(100) | yes |  |  |
| 8 | `parking` | decimal(16,2) | yes |  |  |
| 9 | `oexp` | decimal(16,2) | yes |  |  |
| 10 | `mileage` | decimal(16,2) | yes |  |  |
| 11 | `sb` | tinyint | yes |  | ('0') |

### dbo.tomtomorder

Rows: 0

Primary key: `ttono` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__tomtomord__jobno__603D47BB`)
- `idno` to `employee.idno` (`FK__tomtomorde__idno__61316BF4`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ttono` | int | no | 1, 1 |  |
| 2 | `ocdate` | datetime | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `idno` | int | yes |  |  |
| 5 | `odate` | datetime | yes |  |  |
| 6 | `odone` | tinyint | yes |  | ('0') |
| 7 | `ostate` | varchar(250) | yes |  |  |
| 8 | `ostatetime` | datetime | yes |  |  |
| 9 | `opostext` | varchar(250) | yes |  |  |
| 10 | `parrivaltime` | datetime | yes |  |  |
| 11 | `earrivaltime` | datetime | yes |  |  |
| 12 | `tscode` | int | yes |  |  |

### dbo.tomtomsetup

Rows: 1

Primary key: `bkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `bkey` | int | no | 1, 1 |  |
| 2 | `url` | varchar(250) | yes |  |  |
| 3 | `taccount` | varchar(250) | yes |  |  |
| 4 | `tuser` | varchar(250) | yes |  |  |
| 5 | `tpassword` | varchar(250) | yes |  |  |
| 6 | `apikey` | varchar(250) | yes |  |  |
| 7 | `jlono` | int | yes |  | ('0') |

### dbo.tradelist

Rows: 0

Primary key: `tkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tkey` | int | no | 1, 1 |  |
| 2 | `trade` | varchar(200) | yes |  |  |

### dbo.trainingcardtype

Rows: 0

Primary key: `tckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tckey` | int | no | 1, 1 |  |
| 2 | `tctype` | varchar(200) | yes |  |  |

### dbo.tvboardextra

Rows: 0

Primary key: `tvekey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tvekey` | int | no | 1, 1 |  |
| 2 | `achivedone` | tinyint | yes |  | ('0') |
| 3 | `thistype` | tinyint | yes |  | ('0') |
| 4 | `jobno` | int | yes |  |  |
| 5 | `idno` | int | yes |  |  |
| 6 | `thisdate` | datetime | yes |  |  |
| 7 | `stime` | datetime | yes |  |  |
| 8 | `ftime` | datetime | yes |  |  |
| 9 | `reportby` | varchar(100) | yes |  |  |
| 10 | `note` | varchar(500) | yes |  |  |
| 11 | `agrtime` | datetime | yes |  |  |
| 12 | `bretime` | datetime | yes |  |  |
| 13 | `worktype` | varchar(100) | yes |  |  |
| 14 | `supsor` | varchar(100) | yes |  |  |
| 15 | `labtime` | varchar(20) | yes |  |  |
| 16 | `durtime` | varchar(20) | yes |  |  |
| 17 | `readyclient` | tinyint | yes |  | ('0') |
| 18 | `rclientpath` | varchar(500) | yes |  |  |

### dbo.typecharging

Rows: 0

Primary key: `tckey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `tckey` | int | no | 1, 1 |  |
| 2 | `tcname` | varchar(200) | yes |  |  |

### dbo.unitmeasure

Rows: 0

Primary key: `umkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `umkey` | int | no | 1, 1 |  |
| 2 | `umname` | varchar(200) | yes |  |  |

### dbo.upgradeinfo

Rows: 1

Primary key: `ugno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ugno` | int | no | 1, 1 |  |
| 2 | `pdate` | datetime | yes |  |  |
| 3 | `sdate` | datetime | yes |  |  |
| 4 | `newfdate` | datetime | yes |  |  |

### dbo.usejobrefrigerant

Rows: 0

Primary key: `cfkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `cfkey` | int | no | 1, 1 |  |
| 2 | `adddate` | datetime | yes |  |  |
| 3 | `refrigtype` | varchar(250) | yes |  |  |
| 4 | `refrigsno` | varchar(250) | yes |  |  |
| 5 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 6 | `ourpno` | varchar(50) | yes |  |  |
| 7 | `jobno` | int | yes |  |  |
| 8 | `rfkey` | int | yes |  |  |

### dbo.usemainstock

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `ptype` | varchar(5) | yes |  |  |
| 4 | `ourpno` | varchar(50) | yes |  |  |
| 5 | `sname` | varchar(100) | yes |  |  |
| 6 | `pname` | text | yes |  |  |
| 7 | `qty` | int | yes |  |  |
| 8 | `price` | decimal(16,2) | yes |  |  |
| 9 | `lprice` | decimal(16,2) | yes |  |  |
| 10 | `cdate` | datetime | yes |  |  |
| 11 | `cslno` | int | yes |  | ('0') |
| 12 | `rrpval` | decimal(16,2) | yes |  |  |
| 13 | `csckey` | int | yes |  |  |

### dbo.usematerials

Rows: 0

Primary key: `umkey` (clustered).

Foreign keys out:

- `jobno` to `orders.jobno` (`FK__usemateri__jobno__2739D489`)
- `ourpno` to `materials.ourpno` (`FK__usemateri__ourpn__282DF8C2`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `umkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `quantity` | int | yes |  |  |
| 5 | `price` | decimal(16,2) | yes |  |  |
| 6 | `lprice` | decimal(16,2) | yes |  |  |
| 7 | `idno` | int | yes |  |  |
| 8 | `cdate` | datetime | yes |  |  |
| 9 | `pncode` | varchar(50) | yes |  |  |
| 10 | `pwid` | decimal(16,2) | yes |  |  |
| 11 | `plen` | decimal(16,2) | yes |  |  |
| 12 | `pdep` | decimal(16,2) | yes |  |  |
| 13 | `pdim` | decimal(16,2) | yes |  |  |
| 14 | `wdes` | varchar(500) | yes |  |  |
| 15 | `sealno` | varchar(100) | yes |  |  |
| 16 | `inputtype` | int | yes |  | ('0') |
| 17 | `path` | varchar(100) | yes |  |  |
| 18 | `photo1` | varchar(100) | yes |  |  |
| 19 | `exzone` | int | yes |  | ('0') |
| 20 | `vskey` | int | yes |  |  |
| 21 | `alleng` | varchar(1000) | yes |  |  |
| 22 | `stime` | datetime | yes |  |  |
| 23 | `etime` | datetime | yes |  |  |
| 24 | `tonsite` | int | yes |  | ('0') |
| 25 | `wcoper` | tinyint | yes |  | ('0') |
| 26 | `wcass` | tinyint | yes |  | ('0') |
| 27 | `idrill` | tinyint | yes |  | ('0') |
| 28 | `adrill` | tinyint | yes |  | ('0') |
| 29 | `aswrig` | tinyint | yes |  | ('0') |
| 30 | `hlwork` | tinyint | yes |  | ('0') |
| 31 | `pstype` | int | yes |  | ('0') |
| 32 | `loc` | varchar(100) | yes |  |  |
| 33 | `imat` | varchar(100) | yes |  |  |
| 34 | `rrprate` | decimal(16,2) | yes |  |  |
| 35 | `bitbroken` | tinyint | yes |  | ('0') |
| 36 | `mmused` | decimal(16,2) | yes |  |  |
| 37 | `dustcon` | tinyint | yes |  | ('0') |
| 38 | `mmusedper` | varchar(100) | yes |  |  |
| 39 | `mesment` | varchar(50) | yes |  |  |
| 40 | `repair` | tinyint | yes |  | ('0') |
| 41 | `fpdetail` | varchar(100) | yes |  |  |
| 42 | `cref` | varchar(50) | yes |  |  |
| 43 | `frating` | varchar(10) | yes |  |  |
| 44 | `acrating` | varchar(10) | yes |  |  |
| 45 | `fpother1` | varchar(100) | yes |  |  |
| 46 | `fpother2` | varchar(100) | yes |  |  |

### dbo.usereflog

Rows: 0

Primary key: `rkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rkey` | int | no | 1, 1 |  |
| 2 | `adddate` | datetime | yes |  |  |
| 3 | `refrigtype` | varchar(250) | yes |  |  |
| 4 | `refrigsno` | varchar(250) | yes |  |  |
| 5 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 6 | `jobno` | int | yes |  |  |
| 7 | `gasin` | tinyint | yes |  | ('0') |
| 8 | `gfrom` | varchar(100) | yes |  |  |
| 9 | `ourpno` | varchar(50) | yes |  |  |
| 10 | `gmtype` | tinyint | yes |  | ('0') |

### dbo.userefrigerant

Rows: 0

Primary key: `rkey` (clustered).

Foreign keys out:

- `cfkey` to `centralrefrigerant.cfkey` (`FK__userefrig__cfkey__4830B400`)
- `jobno` to `orders.jobno` (`FK__userefrig__jobno__46486B8E`)
- `srefkey` to `stockrefrigerant.srefkey` (`FK__userefrig__srefk__473C8FC7`)
- `idno` to `employee.idno` (`FK__userefrige__idno__45544755`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `rkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `srefkey` | int | yes |  |  |
| 5 | `refrigkgs` | decimal(16,2) | yes |  |  |
| 6 | `udate` | datetime | yes |  |  |
| 7 | `vehreg` | varchar(50) | yes |  |  |
| 8 | `cfkey` | int | yes |  |  |
| 9 | `ourpno` | varchar(50) | yes |  |  |
| 10 | `otype` | tinyint | yes |  | ('1') |
| 11 | `cslno` | int | yes |  |  |

### dbo.userferror

Rows: 0

Primary key: `fkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `fkey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `luser` | varchar(100) | yes |  |  |
| 5 | `msgerror` | text | yes |  |  |

### dbo.userlog

Rows: 197711

Primary key: `ulkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `ulkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `formname` | varchar(250) | yes |  |  |
| 4 | `luser` | varchar(100) | yes |  |  |
| 5 | `ldate` | datetime | yes |  |  |
| 6 | `cname` | varchar(250) | yes |  |  |

### dbo.usertypelimit

Rows: 0

Primary key: `utkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `utkey` | int | no | 1, 1 |  |
| 2 | `utname` | varchar(50) | yes |  |  |
| 3 | `pohlimit` | tinyint | yes |  | ('0') |
| 4 | `polimit` | decimal(16,2) | yes |  |  |
| 5 | `jlono` | int | yes |  | ('0') |

### dbo.usevanlog

Rows: 0

Primary key: `vlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vlkey` | int | no | 1, 1 |  |
| 2 | `cdate` | datetime | yes |  |  |
| 3 | `jobno` | int | yes |  |  |
| 4 | `idno` | int | yes |  |  |
| 5 | `ourpno` | varchar(50) | yes |  |  |
| 6 | `qty` | decimal(16,2) | yes |  |  |
| 7 | `price` | decimal(16,2) | yes |  |  |
| 8 | `lprice` | decimal(16,2) | yes |  |  |
| 9 | `pwid` | decimal(16,2) | yes |  |  |
| 10 | `plen` | decimal(16,2) | yes |  |  |
| 11 | `pdep` | decimal(16,2) | yes |  |  |
| 12 | `pdim` | decimal(16,2) | yes |  |  |
| 13 | `wdes` | varchar(500) | yes |  |  |
| 14 | `sealno` | varchar(100) | yes |  |  |
| 15 | `inputtype` | int | yes |  | ('0') |
| 16 | `path` | varchar(100) | yes |  |  |
| 17 | `photo1` | varchar(100) | yes |  |  |
| 18 | `exzone` | int | yes |  | ('0') |
| 19 | `vskey` | int | yes |  |  |
| 20 | `alleng` | varchar(1000) | yes |  |  |
| 21 | `stime` | datetime | yes |  |  |
| 22 | `etime` | datetime | yes |  |  |
| 23 | `tonsite` | int | yes |  | ('0') |
| 24 | `wcoper` | tinyint | yes |  | ('0') |
| 25 | `wcass` | tinyint | yes |  | ('0') |
| 26 | `idrill` | tinyint | yes |  | ('0') |
| 27 | `adrill` | tinyint | yes |  | ('0') |
| 28 | `aswrig` | tinyint | yes |  | ('0') |
| 29 | `hlwork` | tinyint | yes |  | ('0') |
| 30 | `pstype` | int | yes |  | ('0') |
| 31 | `loc` | varchar(100) | yes |  |  |
| 32 | `imat` | varchar(100) | yes |  |  |
| 33 | `rrprate` | decimal(16,2) | yes |  |  |
| 34 | `bitbroken` | tinyint | yes |  | ('0') |
| 35 | `mmused` | decimal(16,2) | yes |  |  |
| 36 | `dustcon` | tinyint | yes |  | ('0') |
| 37 | `mmusedper` | varchar(100) | yes |  |  |
| 38 | `aekey` | int | yes |  |  |
| 39 | `mesment` | varchar(50) | yes |  |  |
| 40 | `repair` | tinyint | yes |  | ('0') |
| 41 | `fpdetail` | varchar(100) | yes |  |  |
| 42 | `cref` | varchar(50) | yes |  |  |
| 43 | `frating` | varchar(10) | yes |  |  |
| 44 | `acrating` | varchar(10) | yes |  |  |
| 45 | `fpother1` | varchar(100) | yes |  |  |
| 46 | `fpother2` | varchar(100) | yes |  |  |

### dbo.vanpartsinout

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `ourpno` | varchar(50) | yes |  |  |
| 3 | `vehreg` | varchar(100) | yes |  |  |
| 4 | `qty` | int | yes |  |  |
| 5 | `pdate` | datetime | yes |  |  |
| 6 | `partsin` | int | yes |  | ('0') |
| 7 | `pfrom` | varchar(100) | yes |  |  |
| 8 | `pctype` | int | yes |  | ('0') |
| 9 | `comment` | text | yes |  |  |

### dbo.vanpartsorder

Rows: 0

Primary key: `vorderno` (clustered).

Referenced by:

- `cenpartsorderall.vorderno` (`FK__cenpartso__vorde__1881A0DE`)
- `invoicevan.vorderno` (`FK__invoiceva__vorde__370627FE`)
- `vanpartsorderall.vorderno` (`FK__vanpartso__vorde__7CD98669`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vorderno` | int | no | 1, 1 |  |
| 2 | `odate` | datetime | yes |  |  |
| 3 | `sname` | varchar(100) | yes |  |  |
| 4 | `otype` | varchar(10) | yes |  |  |
| 5 | `oby` | varchar(50) | yes |  |  |

### dbo.vanpartsorderall

Rows: 0

Primary key: `pkey` (clustered).

Foreign keys out:

- `vorderno` to `vanpartsorder.vorderno` (`FK__vanpartso__vorde__7CD98669`)

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `vorderno` | int | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `vehreg` | varchar(100) | yes |  |  |
| 5 | `qty` | int | yes |  |  |
| 6 | `odate` | datetime | yes |  |  |
| 7 | `rdate` | datetime | yes |  |  |
| 8 | `scollected` | int | yes |  | ('0') |
| 9 | `gto` | varchar(200) | yes |  |  |
| 10 | `pcode` | decimal(16,4) | yes |  |  |
| 11 | `pcost` | decimal(16,4) | yes |  |  |
| 12 | `comcodename` | varchar(200) | yes |  |  |
| 13 | `appdet` | tinyint | yes |  | ('0') |
| 14 | `appexport` | tinyint | yes |  | ('0') |
| 15 | `appdetuser` | varchar(100) | yes |  |  |
| 16 | `appdetdate` | datetime | yes |  |  |
| 17 | `appexportuser` | varchar(100) | yes |  |  |
| 18 | `appexportdate` | datetime | yes |  |  |
| 19 | `uploadexport` | tinyint | yes |  | ('0') |
| 20 | `uploadexportdate` | datetime | yes |  |  |
| 21 | `cancelpo` | tinyint | yes |  | ('0') |
| 22 | `deliveryparts` | tinyint | yes |  | ('0') |
| 23 | `epdone` | tinyint | yes |  | ('0') |
| 24 | `epdate` | datetime | yes |  |  |
| 25 | `deliverydate` | datetime | yes |  |  |

### dbo.vatcodetable

Rows: 3

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `vcode` | varchar(50) | yes |  |  |
| 3 | `vatamt` | decimal(16,2) | yes |  |  |
| 4 | `drcharge` | int | yes |  | ('0') |
| 5 | `drctext` | varchar(100) | yes |  |  |
| 6 | `xerotext` | varchar(100) | yes |  |  |
| 7 | `drvatamt` | decimal(16,2) | yes |  |  |
| 8 | `dodramt` | int | yes |  | ('0') |
| 9 | `xerotextpi` | varchar(100) | yes |  |  |
| 10 | `xerotextp` | varchar(100) | yes |  |  |
| 11 | `vatdefault` | int | yes |  | ('0') |

### dbo.vehengrecord

Rows: 2

Primary key: `vrkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vrkey` | int | no | 1, 1 |  |
| 2 | `idno` | int | yes |  |  |
| 3 | `vehreg` | varchar(100) | yes |  |  |
| 4 | `cdate` | datetime | yes |  |  |
| 5 | `enddate` | datetime | yes |  |  |

### dbo.vehicles

Rows: 10

Primary key: `vehreg` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vehreg` | varchar(20) | no |  |  |
| 2 | `make` | varchar(100) | yes |  |  |
| 3 | `motexp` | datetime | yes |  |  |
| 4 | `tomid` | varchar(100) | yes |  |  |
| 5 | `serint` | decimal(16,2) | yes |  |  |
| 6 | `mileage` | decimal(16,2) | yes |  |  |
| 7 | `lserdate` | datetime | yes |  |  |
| 8 | `jlono` | int | yes |  |  |
| 9 | `rtax` | datetime | yes |  |  |
| 10 | `ainfo` | text | yes |  |  |
| 11 | `csn` | int | yes |  | ('0') |
| 12 | `vh` | tinyint | yes |  | ('0') |

### dbo.vehiclesstock

Rows: 0

Primary key: `vskey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vskey` | int | no | 1, 1 |  |
| 2 | `vehreg` | varchar(50) | yes |  |  |
| 3 | `ourpno` | varchar(50) | yes |  |  |
| 4 | `quantity` | int | yes |  |  |
| 5 | `vsadd` | datetime | yes |  |  |
| 6 | `vsluse` | datetime | yes |  |  |
| 7 | `rsl` | int | yes |  |  |

### dbo.visitphoto

Rows: 0

Primary key: `vpkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `vpkey` | int | no | 1, 1 |  |
| 2 | `jobno` | int | yes |  |  |
| 3 | `idno` | int | yes |  |  |
| 4 | `jpkey` | varchar(50) | yes |  |  |
| 5 | `path` | varchar(50) | yes |  |  |
| 6 | `filename` | varchar(250) | yes |  |  |
| 7 | `photoname` | varchar(100) | yes |  |  |

### dbo.weblog

Rows: 0

Primary key: `pkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `pkey` | int | no | 1, 1 |  |
| 2 | `rip` | varchar(100) | yes |  |  |
| 3 | `stime` | datetime | yes |  |  |
| 4 | `etime` | datetime | yes |  |  |
| 5 | `luser` | varchar(50) | yes |  |  |

### dbo.webpenerrorj

Rows: 0

Primary key: `errorno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `errorno` | int | no | 1, 1 |  |
| 2 | `errortype` | int | yes |  |  |
| 3 | `errordes` | text | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `idno` | int | yes |  |  |
| 6 | `path` | varchar(250) | yes |  |  |
| 7 | `csvfile` | varchar(250) | yes |  |  |
| 8 | `cpath1` | varchar(250) | yes |  |  |
| 9 | `cpath2` | varchar(250) | yes |  |  |
| 10 | `fpath` | varchar(250) | yes |  |  |
| 11 | `addtime` | datetime | yes |  |  |
| 12 | `jelock` | tinyint | yes |  | ('0') |
| 13 | `jeuser` | varchar(50) | yes |  |  |
| 14 | `cpath3` | varchar(250) | yes |  |  |
| 15 | `cpath4` | varchar(250) | yes |  |  |
| 16 | `cpath5` | varchar(250) | yes |  |  |
| 17 | `cpath6` | varchar(250) | yes |  |  |
| 18 | `cpath7` | varchar(250) | yes |  |  |
| 19 | `cpath8` | varchar(250) | yes |  |  |
| 20 | `cpath9` | varchar(250) | yes |  |  |
| 21 | `cpath10` | varchar(250) | yes |  |  |

### dbo.webpenerrorjmb

Rows: 0

Primary key: `errorno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `errorno` | int | no | 1, 1 |  |
| 2 | `errortype` | int | yes |  |  |
| 3 | `errordes` | text | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `idno` | int | yes |  |  |
| 6 | `path` | varchar(250) | yes |  |  |
| 7 | `csvfile` | varchar(250) | yes |  |  |
| 8 | `cpath1` | varchar(250) | yes |  |  |
| 9 | `cpath2` | varchar(250) | yes |  |  |
| 10 | `fpath` | varchar(250) | yes |  |  |
| 11 | `addtime` | datetime | yes |  |  |
| 12 | `jelock` | tinyint | yes |  | ('0') |
| 13 | `jeuser` | varchar(50) | yes |  |  |

### dbo.webpenerrorws

Rows: 0

Primary key: `errorno` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `errorno` | int | no | 1, 1 |  |
| 2 | `errortype` | int | yes |  |  |
| 3 | `errordes` | text | yes |  |  |
| 4 | `jobno` | int | yes |  |  |
| 5 | `idno` | int | yes |  |  |
| 6 | `path` | varchar(250) | yes |  |  |
| 7 | `csvfile` | varchar(250) | yes |  |  |
| 8 | `cpath1` | varchar(250) | yes |  |  |
| 9 | `cpath2` | varchar(250) | yes |  |  |
| 10 | `cpath3` | varchar(250) | yes |  |  |
| 11 | `cpath4` | varchar(250) | yes |  |  |
| 12 | `cpath5` | varchar(250) | yes |  |  |
| 13 | `cpath6` | varchar(250) | yes |  |  |
| 14 | `cpath7` | varchar(250) | yes |  |  |
| 15 | `cpath8` | varchar(250) | yes |  |  |
| 16 | `cpath9` | varchar(250) | yes |  |  |
| 17 | `cpath10` | varchar(250) | yes |  |  |
| 18 | `fpath` | varchar(250) | yes |  |  |
| 19 | `addtime` | datetime | yes |  |  |
| 20 | `jelock` | tinyint | yes |  | ('0') |
| 21 | `jeuser` | varchar(50) | yes |  |  |

### dbo.weekinfo

Rows: 1

Primary key: none. Heap, no indexes.

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `wstart` | int | yes |  |  |
| 2 | `dayname` | varchar(20) | yes |  |  |
| 3 | `ivfoot` | text | yes |  |  |
| 4 | `ivfoot2` | text | yes |  |  |
| 5 | `ivfoot3` | text | yes |  |  |
| 6 | `quotetxt` | text | yes |  |  |
| 7 | `jlono` | int | yes |  | ('0') |
| 8 | `ivfoot4` | text | yes |  |  |
| 9 | `imgurl` | varchar(100) | yes |  |  |
| 10 | `quotetxttwo` | text | yes |  |  |
| 11 | `quotetxtthree` | text | yes |  |  |
| 12 | `subcon` | text | yes |  |  |
| 13 | `oparttxt` | text | yes |  |  |
| 14 | `ghostid` | varchar(10) | yes |  |  |

### dbo.workgroup

Rows: 0

Primary key: `wgkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `wgkey` | int | no | 1, 1 |  |
| 2 | `wgname` | varchar(100) | yes |  |  |

### dbo.worktype

Rows: 0

Primary key: `wtkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `wtkey` | int | no | 1, 1 |  |
| 2 | `wtname` | varchar(200) | yes |  |  |

### dbo.xmlweblist

Rows: 0

Primary key: `xwlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `xwlkey` | int | no | 1, 1 |  |
| 2 | `keyval` | varchar(50) | yes |  |  |
| 3 | `name` | varchar(500) | yes |  |  |
| 4 | `dform` | tinyint | yes |  | ('0') |
| 5 | `xpos` | int | yes |  |  |
| 6 | `ypos` | int | yes |  |  |

### dbo.xmlweblist2

Rows: 0

Primary key: `xwlkey` (clustered).

| # | Column | Type | Null | Identity | Default |
|---|---|---|---|---|---|
| 1 | `xwlkey` | int | no | 1, 1 |  |
| 2 | `keyval` | varchar(50) | yes |  |  |
| 3 | `name` | varchar(500) | yes |  |  |
| 4 | `dform` | tinyint | yes |  | ('0') |
| 5 | `xpos` | int | yes |  |  |
| 6 | `ypos` | int | yes |  |  |

