using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class AuditLogConfiguration : EntityConfiguration<AuditLog>
    {
        public override void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            base.Configure(builder);

            builder.Property(al => al.Action).IsRequired().HasMaxLength(100);
            builder.Property(al => al.Description).HasMaxLength(300);

            builder.HasOne(al => al.User)
                   .WithMany(u => u.Logs)
                   .HasForeignKey(al => al.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
