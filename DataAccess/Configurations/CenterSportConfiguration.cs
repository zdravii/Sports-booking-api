using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configurations
{
    public class CenterSportConfiguration : IEntityTypeConfiguration<CenterSport>
    {
        public void Configure(EntityTypeBuilder<CenterSport> builder)
        {
            builder.HasKey(x => new { x.CenterId, x.SportId });

            builder.HasOne(cs => cs.Center)
                   .WithMany(c => c.Sports)
                   .HasForeignKey(cs => cs.CenterId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.Sport)
                   .WithMany(s => s.Centers)
                   .HasForeignKey(cs => cs.SportId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
