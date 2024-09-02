CREATE TABLE [dbo].[Clients] (
    [Id]           INT        IDENTITY (1, 1) NOT NULL,
    [departametId] NCHAR (10) NOT NULL,
    [surname]      NCHAR (25) NOT NULL,
    [name]         NCHAR (25) NOT NULL,
    [middlename]   NCHAR (25) NULL,
    [phone]        NCHAR (11) NULL,
    [pasportData]  NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);