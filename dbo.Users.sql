CREATE TABLE [dbo].[Users] (
    [Id]         INT           NOT NULL IDENTITY,
    [Login]      NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (50) NOT NULL,
    [WorkerType] NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

