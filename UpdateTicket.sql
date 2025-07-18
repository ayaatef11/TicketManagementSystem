create procedure [dbo].[UpdateTicket]
@ID int,@fullName nvarchar(30),@MobileNumber nvarchar(30),@Email nvarchar(30),@Description nvarchar(30),
@Priority nvarchar(30),@CreatedDate datetime,@IssueTypeID int,@Status nvarchar(30)
as 
begin transaction
begin try
update CustomerTickets
set FullName=@fullName,
MobileNumber=@MobileNumber,
Email=@Email,
[Description]=@Description,
[Priority]=@Priority,
CreatedDate=@CreatedDate,
IssueTypeID=@IssueTypeID,
[Status]=@Status
where Id=@ID
commit transaction
end try
begin catch
rollback transaction
end catch

