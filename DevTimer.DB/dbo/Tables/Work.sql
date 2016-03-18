CREATE TABLE [dbo].[Work] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [ProjectID]   INT            NOT NULL,
    [WorkTypeID]  INT            NOT NULL,
    [UserID]      NVARCHAR (128) NOT NULL,
    [Description] VARCHAR (MAX)  NULL,
    [StartTime]   SMALLDATETIME  NULL,
    [EndTime]     SMALLDATETIME  NULL,
    [Hours]       FLOAT (53)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Work_AspNetUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Work_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ID]),
    CONSTRAINT [FK_Work_WorkType] FOREIGN KEY ([WorkTypeID]) REFERENCES [dbo].[WorkType] ([ID])
);

