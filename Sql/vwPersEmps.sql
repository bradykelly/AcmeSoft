CREATE VIEW vwPersEmps
AS
SELECT 
	  p.PersonId
	, p.BirthDate
	, p.FirstName
	, p.LastName
	, e.EmployeeId
	, e.EmployedDate
	, e.EmployeeNum
	, e.TerminatedDate
FROM 
	Employee e
	INNER JOIN Person p on e.PersonId = p.PersonId
GO


