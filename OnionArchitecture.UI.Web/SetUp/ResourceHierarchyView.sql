IF EXISTS(select * FROM sys.views where name = 'VwResourceHierarchy')
BEGIN
	DROP VIEW VwResourceHierarchy
END

GO

CREATE VIEW VwResourceHierarchy
AS
  WITH ResourceCTE(Id, Name, Description, ParentId) AS (
  SELECT Id, Name, Description, ParentId
  FROM Resources
  WHERE ParentId IS NULL
	
  UNION ALL
  
  SELECT r.Id, r.Name, r.Description, r.ParentId
  FROM ResourceCTE AS cte
	INNER JOIN Resources AS r
	  ON cte.Id = r.ParentId
)
SELECT * FROM ResourceCTE
GO