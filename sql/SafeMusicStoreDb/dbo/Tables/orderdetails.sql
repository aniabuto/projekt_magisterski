CREATE TABLE [dbo].[orderdetails] (
    [id]        INT             IDENTITY (1, 1) NOT NULL,
    [orderid]   INT             NOT NULL,
    [albumid]   INT             NOT NULL,
    [quantity]  INT             NOT NULL,
    [unitprice] NUMERIC (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

