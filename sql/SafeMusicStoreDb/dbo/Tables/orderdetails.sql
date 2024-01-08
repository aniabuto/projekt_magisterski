CREATE TABLE [dbo].[orderdetails] (
    [id]        INT             IDENTITY (1, 1) NOT NULL,
    [orderid]   INT             NOT NULL,
    [albumid]   INT             NOT NULL,
    [quantity]  INT             NOT NULL,
    [unitprice] NUMERIC (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [orderdetails_albums_null_fk] FOREIGN KEY ([albumid]) REFERENCES [dbo].[albums] ([id]),
    CONSTRAINT [orderdetails_orders_null_fk] FOREIGN KEY ([orderid]) REFERENCES [dbo].[orders] ([id])
);
GO

