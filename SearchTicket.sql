create procedure [dbo].[SearchTicket] @name nvarchar(30)
as 
begin
select * from CustomerTickets where FullName =@name
end