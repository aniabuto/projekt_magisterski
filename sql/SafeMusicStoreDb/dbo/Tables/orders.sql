CREATE TABLE [dbo].[orders] (
    [id]         INT             IDENTITY (1, 1) NOT NULL,
    [orderdate]  DATETIME        NOT NULL,
    [username]   VARCHAR (256)   NULL,
    [firstname]  VARCHAR (160)   NULL,
    [lastname]   VARCHAR (160)   NULL,
    [address]    VARCHAR (70)    NULL,
    [city]       VARCHAR (40)    NULL,
    [state]      VARCHAR (40)    NULL,
    [postalcode] VARCHAR (10)    NULL,
    [country]    VARCHAR (40)    NULL,
    [phone]      VARCHAR (24)    NULL,
    [email]      VARCHAR (160)   NULL,
    [total]      NUMERIC (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

