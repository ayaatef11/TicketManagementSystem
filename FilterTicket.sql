
create procedure [dbo].[FilterTicket]
 @Priority nvarchar(30), @IssueTypeId int
as 
begin  
select * from CustomerTickets 
where(@Priority is not null and [Priority] = @Priority  )or (@IssueTypeId is not null and IssueTypeID = @IssueTypeId ); 
end 

