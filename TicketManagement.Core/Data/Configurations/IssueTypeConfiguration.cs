using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using TicketManagement.DAL.Models;

namespace TicketManagement.DAL.Data.Configurations
{
    internal class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
    {
        public void Configure(EntityTypeBuilder<IssueType> builder)
        {
            builder.Property(p => p.IssueTypeName)
             .HasMaxLength(30);

        }
    }
}
