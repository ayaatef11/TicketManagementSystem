create procedure [dbo].[SelectIssueType]
as
begin transaction
begin try
select * from IssueTypes

commit transaction
end try
begin catch
rollback transaction
SELECT @@ROWCOUNT
END CATCH
