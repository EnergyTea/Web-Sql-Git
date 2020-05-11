CREATE TABLE [dbo].[Categories] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (256) NULL,
    [Deleted] BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Scripts] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [CategoryId]       INT      NOT NULL,
    [CreationDataTime] DATETIME NOT NULL,
    [Deleted]          BIT      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ScriptsHistory] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ScriptId]         INT            NOT NULL,
    [Name]             NVARCHAR (256) NOT NULL,
    [Body]             NVARCHAR (MAX) NOT NULL,
    [Author]           NVARCHAR (256) NOT NULL,
    [CategoryId]       INT            NOT NULL,
    [Version]          INT            NOT NULL,
    [UpdateDataTime]   DATETIME       NOT NULL,
    [IsLastVersion]    BIT     NOT NULL,
    [CreationDataTime] DATETIME       NOT NULL,
    [Deleted]          BIT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);