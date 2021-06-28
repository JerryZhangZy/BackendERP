CREATE FUNCTION [dbo].[VariationProductAttribute$Select]
(	@centralProductNum int
)
RETURNS TABLE 
AS
RETURN 
(
	select	AttributeNum,AttributeValue 
	from	dbo.ProductAttributeRelationship 
	where	CentralProductNum =@centralProductNum
	union	all
	select	AttributeNum,AttributeValue 
	from	dbo.ProductAttributeRelationship 
	where	CentralProductNum = (select CentralProductNum from dbo.ProductBasic where CentralProductNum = @centralProductNum and (CentralProductNum != VariationParentSKU))
)
