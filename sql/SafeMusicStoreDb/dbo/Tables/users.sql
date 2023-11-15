CREATE TABLE [dbo].[users] (
    [id]       INT           IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (200) NOT NULL,
    [email]    VARCHAR (200) NOT NULL,
    [password] VARCHAR (200) NOT NULL,
    [role]     VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

