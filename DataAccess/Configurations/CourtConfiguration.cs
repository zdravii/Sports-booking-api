using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class CourtConfiguration : EntityConfiguration<Court>
    {
        public override void Configure(EntityTypeBuilder<Court> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Label).IsRequired().HasMaxLength(50);
            builder.Property(c => c.isActive).HasDefaultValue(true);

            builder.HasIndex(c => new
            {
                c.Label,
                c.CenterId
            }).IsUnique();


            builder.HasOne(c => c.Center)
                   .WithMany(ce => ce.Courts)
                   .HasForeignKey(c => c.CenterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Sport)
                   .WithMany(s => s.Courts)
                   .HasForeignKey(c => c.SportId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
