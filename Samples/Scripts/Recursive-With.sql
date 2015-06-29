WITH tmpUnion AS 
	(SELECT *, 'AI' AS 'Type' FROM [dbo].[intern_Tasks] 
		UNION ALL 
		SELECT *, 'BR' 
		FROM [dbo].[role_Tasks] 
		UNION ALL 
		SELECT *, 'OE'
		FROM [dbo].[oe_Tasks] 
		UNION ALL 
		SELECT * , 'OG'
		FROM [dbo].[og_Tasks]), 
	tmpTable AS (
		SELECT ID, CallingTask, [Status], Operation, execution_time, [Type], 1 AS NUM 
		FROM tmpUnion
		UNION ALL
		SELECT tmpUnion.ID, tmpUnion.CallingTask, tmpUnion.[Status], tmpUnion.Operation, tmpUnion.execution_time, tmpUnion.[Type], NUM + 1 
		FROM tmpTable 
		JOIN tmpUnion
		ON tmpTable.CallingTask LIKE '%' + CONVERT(NVARCHAR(50), tmpUnion.ID))
SELECT * FROM tmpTable
ORDER BY execution_time