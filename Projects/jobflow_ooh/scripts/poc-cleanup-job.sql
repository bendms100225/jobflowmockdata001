/*
    Removes one proof-of-concept job from the standalone JobFlowDemo database.

    Restoring the backup in docs/poc-database-restore.md is the preferred reset.
    This script is not called by the web application.

    It rolls back unless @Commit is set to 1.
    Set @JobNo to the JobFlowDemo job number before running.
*/
DECLARE @JobNo int = NULL;
DECLARE @Commit bit = 0;

IF @JobNo IS NULL
BEGIN
    RAISERROR('Set @JobNo to the POC job number.', 16, 1);
    RETURN;
END;

IF DB_NAME() <> N'JobFlowDemo'
BEGIN
    RAISERROR('This script refuses to run unless the database is JobFlowDemo.', 16, 1);
    RETURN;
END;

BEGIN TRANSACTION;

DECLARE @Description varchar(4000) = (
    SELECT CAST(description AS varchar(4000))
    FROM dbo.orders
    WHERE jobno = @JobNo
);

IF @Description IS NULL OR LEFT(@Description, 8) <> N'POC-OOH-'
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR('Refused: the job is missing or its description does not start with POC-OOH-.', 16, 1);
    RETURN;
END;

SELECT jobno, clientcode, sitecode, orderno, [date], deadline, categorycode,
       CAST(description AS varchar(4000)) AS description
FROM dbo.orders
WHERE jobno = @JobNo;

SELECT sskey, jobno, sscode
FROM dbo.selectsection
WHERE jobno = @JobNo;

SELECT ulkey, jobno, formname, luser, ldate
FROM dbo.userlog
WHERE jobno = @JobNo;

IF EXISTS (SELECT 1 FROM dbo.status WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.jobplan WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.allocation WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.assetfault WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.emaildone WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.engcale WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.gencojobtracker WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.hireplant WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.jobfile WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.jobform WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.jobitem WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.jobworksheet WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.measurecalc WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.measurefile WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.mslive WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.mssign WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.orderparts WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.otherform WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.paymentvalue WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.qdmeasure WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.tomtomorder WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.usematerials WHERE jobno = @JobNo)
    OR EXISTS (SELECT 1 FROM dbo.userefrigerant WHERE jobno = @JobNo)
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR('Refused: the job has rows outside orders, selectsection, and userlog.', 16, 1);
    RETURN;
END;

DELETE FROM dbo.userlog WHERE jobno = @JobNo;
DELETE FROM dbo.selectsection WHERE jobno = @JobNo;
DELETE FROM dbo.orders
WHERE jobno = @JobNo
  AND LEFT(CAST(description AS varchar(4000)), 8) = N'POC-OOH-';

IF @@ROWCOUNT <> 1
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR('Refused: the orders delete did not affect exactly one POC row.', 16, 1);
    RETURN;
END;

IF @Commit = 1
BEGIN
    COMMIT TRANSACTION;
    PRINT 'Committed the POC job delete.';
END
ELSE
BEGIN
    ROLLBACK TRANSACTION;
    PRINT 'Rolled back. Set @Commit = 1 to delete this POC job.';
END;
