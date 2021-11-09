select count(1) from SalesOrderHeader where ISNUMERIC(OrderNumber) = 1

--SELECT TOP 1 * FROM (
--    SELECT t1.OrderNumber+1 AS number
--    FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t1
--    WHERE NOT EXISTS(
--		SELECT * 
--		FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t2 
--		WHERE t2.OrderNumber = t1.OrderNumber + 1
--	) 
--    UNION 
--    SELECT 1 AS number
--    WHERE NOT EXISTS (
--		SELECT * 
--		FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t3 
--		WHERE t3.OrderNumber = 1
--	)
--) ot
--ORDER BY 1

-- select min number from table
SELECT TOP 1 * FROM (
--SELECT * FROM (
    SELECT t1.OrderNumber+1 AS number
    FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t1
    WHERE NOT EXISTS(
		SELECT * 
		FROM (SELECT CAST(OrderNumber AS bigint) AS OrderNumber FROM SalesOrderHeader WHERE LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1) t2 
		WHERE t2.OrderNumber = t1.OrderNumber + 1
	) 
) ot
WHERE ot.number > (SELECT ini.number FROM InitNumbers ini WHERE ini.type = 'SalesOrder' )
ORDER BY ot.number


-- select first not exist number from table
SELECT COALESCE(
	(SELECT '2021091412334082315' WHERE NOT EXISTS(SELECT * FROM SalesOrderHeader t2 WHERE t2.OrderNumber = '2021091412334082315')),
    (SELECT '2021091412334082315' WHERE NOT EXISTS(SELECT * FROM SalesOrderHeader t2 WHERE t2.OrderNumber = '2021091412334082315')),
	(SELECT '2021101311321438203' WHERE NOT EXISTS(SELECT * FROM SalesOrderHeader t2 WHERE t2.OrderNumber = '2021101311321438203')),
	(SELECT '2021101311323270999' WHERE NOT EXISTS(SELECT * FROM SalesOrderHeader t2 WHERE t2.OrderNumber = '2021101311323270999')),
	(SELECT '2021101311323850367' WHERE NOT EXISTS(SELECT * FROM SalesOrderHeader t2 WHERE t2.OrderNumber = '2021101311323850367')),
	(SELECT '2021101311324292794' WHERE NOT EXISTS(SELECT * FROM SalesOrderHeader t2 WHERE t2.OrderNumber = '2021101311324292794')),
	''
)


SELECT OrderNumber FROM SalesOrderHeader WHERE LEN(OrderNumber) < 20 AND ISNUMERIC(OrderNumber) = 1