using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class CourtImageConfiguration : EntityConfiguration<CourtImage>
    {
        public override void Configure(EntityTypeBuilder<CourtImage> builder)
        {
            base.Configure(builder);

            builder.Property(ci => ci.Path).IsRequired().HasMaxLength(200);
            builder.Property(ci => ci.Alt).IsRequired().HasMaxLength(300);

            builder.HasIndex(ci => ci.Path).IsUnique();

            builder.HasOne(ci => ci.Court)
                   .WithMany(c => c.Images)
                   .HasForeignKey(ci => ci.CourtId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
