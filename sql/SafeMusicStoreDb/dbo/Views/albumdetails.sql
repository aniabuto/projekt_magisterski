CREATE VIEW "albumdetails"
as
select a."id", a."thumbnail", a."price", a."title", at."name" as "artist", g."name" as "genre"
from "albums" a
join "artists" at on at."id" = a."artistid"
join "genres" g on g."id" = a."genreid";
GO

