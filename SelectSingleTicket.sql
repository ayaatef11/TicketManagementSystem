create procedure [dbo].[SelectSingleTicket] @ID int
as 
begin transaction
begin try
select * from CustomerTickets where Id=@ID
commit transaction
end try
begin catch
rollback transaction
end catch
