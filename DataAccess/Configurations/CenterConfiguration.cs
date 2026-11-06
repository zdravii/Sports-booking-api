using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class CenterConfiguration : EntityConfiguration<Center>
    {
        public override void Configure(EntityTypeBuilder<Center> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(300);

            builder.HasIndex(c => c.Address).IsUnique();
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
