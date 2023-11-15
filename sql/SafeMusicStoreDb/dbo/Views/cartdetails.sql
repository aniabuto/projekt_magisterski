CREATE VIEW "cartdetails"
as
select c."cartid", c."count", a."title" as "albumtitle", a."id" as "albumid", a."price"
from "carts" c
join "albums" a on c."albumid" = a."id";
GO

