-- SQLite
SELECT *
FROM orders;

alter table orders add column datetime varchar;

alter table orders drop column datetime;