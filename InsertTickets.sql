create procedure [dbo].[InsertTickets]
@fullName nvarchar(30),@MobileNumber nvarchar(30),@Email nvarchar(30),@Description nvarchar(30),
@Priority nvarchar(30),@CreatedDate datetime,@IssueTypeId int,@Status nvarchar(30)
as
BEGIN TRANSACTION 
BEGIN TRY  
insert into CustomerTickets(FullName,MobileNumber,Email,[Description],[Priority],CreatedDate,IssueTypeID,[Status])
values (@fullName,@MobileNumber,@Email,@Description,@Priority,@CreatedDate,@IssueTypeId,@Status);

COMMIT TRANSACTION 
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION
SELECT @@ROWCOUNT
END CATCH