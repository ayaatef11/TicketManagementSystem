
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using TicketManagement.DAL.Enums;
using TicketManagement.DAL.Models;

namespace TicketManagement.DAL.Data.Configurations
{
    public class TicketConfigurations : IEntityTypeConfiguration<CustomerTicket>
    {
        public void Configure(EntityTypeBuilder<CustomerTicket> builder)
        {
            builder.Property(p => p.Id)
             .UseIdentityColumn();

            builder.Property(t => t.FullName).IsRequired().HasMaxLength(30);
            builder.Property(t => t.MobileNumber).IsRequired().HasMaxLength(30);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(30);

            builder.Property(t => t.Description).IsRequired().HasMaxLength(250);

            builder.Property(t => t.Priority)
                 .HasConversion(
                 OPriority => OPriority.ToString(),
                 OPriority => (Priority)Enum.Parse(typeof(Priority), OPriority)
                 ).HasMaxLength(30);
            builder.Property(t => t.Status)
              .HasConversion(
              OStatus => OStatus.ToString(),
              OStatus => (Status)Enum.Parse(typeof(Status), OStatus)
              ).HasMaxLength(30);
        }
    }
}
