CREATE TABLE [dbo].[carts] (
    [recordid]    INT          IDENTITY (1, 1) NOT NULL,
    [cartid]      VARCHAR (50) NOT NULL,
    [albumid]     INT          NOT NULL,
    [count]       INT          NOT NULL,
    [datecreated] DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([recordid] ASC)
);
GO

