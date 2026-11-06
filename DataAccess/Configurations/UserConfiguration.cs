using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(300);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(300);
            builder.Property(u => u.IsActive).HasDefaultValue(true);
            builder.Property(u => u.RoleId).HasDefaultValue(1);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
