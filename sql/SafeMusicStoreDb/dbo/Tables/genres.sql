CREATE TABLE [dbo].[genres] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (120)  NULL,
    [description] VARCHAR (4000) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO

