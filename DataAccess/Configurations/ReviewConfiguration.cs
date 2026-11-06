using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class ReviewConfiguration : EntityConfiguration<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Comment).IsRequired().HasMaxLength(300);

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Court)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.CourtId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
