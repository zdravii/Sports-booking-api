using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataBaseContext : DbContext
    {
        
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<CenterSport> CenterSports { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<CourtImage> CourtImages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles{ get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
    }
}
