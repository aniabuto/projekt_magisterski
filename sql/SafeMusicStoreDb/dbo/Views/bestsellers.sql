CREATE VIEW "bestsellers"
as
select top 5 a."id", a."title", a."thumbnail", Count(*) as "count"
from "albums" as a
inner join "orderdetails" as o on a."id" = o."albumid"
group by a."id", a."title", a."thumbnail"
order by "count" DESC;
GO

