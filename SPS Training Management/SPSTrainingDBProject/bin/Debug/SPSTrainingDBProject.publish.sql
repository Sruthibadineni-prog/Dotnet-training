﻿/*
Deployment script for SPSTrainingDB-snrao

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SPSTrainingDB-snrao"
:setvar DefaultFilePrefix "SPSTrainingDB-snrao"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
IF EXISTS (SELECT 1
           FROM   [sys].[databases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [sys].[databases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [sys].[databases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [sys].[databases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
    END


GO
IF EXISTS (SELECT 1
           FROM   [sys].[databases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Creating Table [dbo].[Employee]...';


GO
CREATE TABLE [dbo].[Employee] (
    [EmpId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([EmpId] ASC)
);


GO
PRINT N'Creating Table [dbo].[Technology]...';


GO
CREATE TABLE [dbo].[Technology] (
    [TechId] CHAR (4) NOT NULL,
    PRIMARY KEY CLUSTERED ([TechId] ASC)
);


GO
PRINT N'Creating Table [dbo].[Trainee]...';


GO
CREATE TABLE [dbo].[Trainee] (
    [TrainingId]    CHAR (4)     NOT NULL,
    [TraineeId]     INT          NOT NULL,
    [TraineeStatus] CHAR (1)     NULL,
    [Remarks]       VARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([TrainingId] ASC, [TraineeId] ASC)
);


GO
PRINT N'Creating Table [dbo].[Trainer]...';


GO
CREATE TABLE [dbo].[Trainer] (
    [TrainerId] CHAR (4) NOT NULL,
    PRIMARY KEY CLUSTERED ([TrainerId] ASC)
);


GO
PRINT N'Creating Table [dbo].[Training]...';


GO
CREATE TABLE [dbo].[Training] (
    [TrainingId] CHAR (4) NOT NULL,
    [TechId]     CHAR (4) NULL,
    [TrainerId]  CHAR (4) NULL,
    [StartDate]  DATETIME NULL,
    [EndDate]    DATETIME NULL,
    PRIMARY KEY CLUSTERED ([TrainingId] ASC)
);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Trainee]...';


GO
ALTER TABLE [dbo].[Trainee] WITH NOCHECK
    ADD FOREIGN KEY ([TrainingId]) REFERENCES [dbo].[Training] ([TrainingId]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Trainee]...';


GO
ALTER TABLE [dbo].[Trainee] WITH NOCHECK
    ADD FOREIGN KEY ([TraineeId]) REFERENCES [dbo].[Employee] ([EmpId]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Training]...';


GO
ALTER TABLE [dbo].[Training] WITH NOCHECK
    ADD FOREIGN KEY ([TechId]) REFERENCES [dbo].[Technology] ([TechId]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Training]...';


GO
ALTER TABLE [dbo].[Training] WITH NOCHECK
    ADD FOREIGN KEY ([TrainerId]) REFERENCES [dbo].[Trainer] ([TrainerId]);


GO
PRINT N'Creating Check Constraint unnamed constraint on [dbo].[Trainee]...';


GO
ALTER TABLE [dbo].[Trainee] WITH NOCHECK
    ADD CHECK (TraineeStatus IN ('C','N'));


GO
