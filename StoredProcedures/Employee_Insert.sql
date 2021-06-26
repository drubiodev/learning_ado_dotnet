CREATE PROCEDURE dbo.Employee_Insert
    @FirstName VARCHAR(100),
    @LastName VARCHAR(100),
    @Email VARCHAR(100),
    @EmployeeId INT OUTPUT
AS

INSERT INTO dbo.Employees
    (FirstName, LastName, EmailAddress, PayRate, BillRate)
VALUES
    (@FirstName, @LastName, @Email, '120', DEFAULT)

SELECT @EmployeeId = SCOPE_IDENTITY();