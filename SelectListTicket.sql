create procedure [dbo].[SelectListTicket]
as 
begin transaction
begin try
select * from CustomerTickets
commit transaction
end try
begin catch
rollback transaction
end catch
execute SelectListTicket