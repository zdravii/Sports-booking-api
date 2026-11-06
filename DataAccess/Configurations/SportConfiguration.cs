using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class SportConfiguration : EntityConfiguration<Sport>
    {
        public override void Configure(EntityTypeBuilder<Sport> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Description).IsRequired().HasMaxLength(500);

            builder.HasIndex(s => s.Name).IsUnique();
        }
    }
}
