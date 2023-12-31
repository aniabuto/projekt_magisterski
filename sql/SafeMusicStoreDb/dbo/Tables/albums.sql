CREATE TABLE [dbo].[albums] (
    [id]        INT             IDENTITY (1, 1) NOT NULL,
    [genreid]   INT             NOT NULL,
    [artistid]  INT             NOT NULL,
    [title]     VARCHAR (160)   NOT NULL,
    [price]     NUMERIC (10, 2) NOT NULL,
    [thumbnail] VARCHAR (1024)  CONSTRAINT [DF_Album_Thumbnail] DEFAULT ('/placeholder.gif') NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [albums_genres_fk] FOREIGN KEY ([genreid]) REFERENCES [dbo].[genres] ([id]),
    CONSTRAINT [albums_artists_fk] FOREIGN KEY ([artistid]) REFERENCES [dbo].[artists] ([id])
);


GO


