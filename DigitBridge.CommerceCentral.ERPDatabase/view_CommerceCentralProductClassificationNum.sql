CREATE VIEW [dbo].[view_CommerceCentralProductClassificationNum]
	as


select convert(varchar(10), DatabaseNum) +'-'+ Convert(varchar(10),ClassificationNum) 'ClassificationId',
MasterAccountNum, 
ProfileNum, 
ClassificationName, 
Description,
CreatedOn, 
LastUpdated
from Classification

go
