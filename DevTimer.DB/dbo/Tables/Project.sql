CREATE TABLE [dbo].[Project] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [ClientID]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Project_Client] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Client] ([ID])
);

