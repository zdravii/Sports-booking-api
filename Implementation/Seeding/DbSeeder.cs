using System.Security.Cryptography;
using System.Text;
using DataAccess; // adjust namespace to match your actual DataBaseContext location
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Seeding
{
    public static class DbSeeder
    {
        public static void Seed(DataBaseContext db)
        {
            // Guard: only seed if the database is completely empty.
            // Prevents duplicate data on every app restart.
            if (db.Users.Any())
                return;

            var now = DateTime.UtcNow;

            // ---------- Roles ----------
            var adminRole = new Role { Name = "Admin", CreatedAt = now, UpdatedAt = now };
            var userRole = new Role { Name = "User", CreatedAt = now, UpdatedAt = now };

            db.Roles.AddRange(adminRole, userRole);
            db.SaveChanges(); // save now so Role.Id is generated and usable below

            // ---------- Users ----------
            var adminUser = new User
            {
                FirstName = "Marko",
                LastName = "Markovic",
                Email = "admin@gmail.com",
                PasswordHash = HashPassword("Admin123"),
                IsActive = true,
                RoleId = adminRole.Id,
                CreatedAt = now,
                UpdatedAt = now
            };

            var normalUser = new User
            {
                FirstName = "Ana",
                LastName = "Anic",
                Email = "user@gmail.com",
                PasswordHash = HashPassword("User123"),
                IsActive = true,
                RoleId = userRole.Id,
                CreatedAt = now,
                UpdatedAt = now
            };

            db.Users.AddRange(adminUser, normalUser);
            db.SaveChanges();

            // ---------- Sports ----------
            var football = new Sport { Name = "Football", Description = "5-a-side football", CreatedAt = now, UpdatedAt = now };
            var basketball = new Sport { Name = "Basketball", Description = "Indoor/outdoor basketball", CreatedAt = now, UpdatedAt = now };
            var tennis = new Sport { Name = "Tennis", Description = "Singles and doubles tennis", CreatedAt = now, UpdatedAt = now };

            db.Sports.AddRange(football, basketball, tennis);
            db.SaveChanges();

            // ---------- Centers ----------
            var centerOne = new Center { Name = "Invictus Sports Center", Address = "Ruzice Milanovic 38", CreatedAt = now, UpdatedAt = now };
            var centerTwo = new Center { Name = "Vojvodina Arena", Address = "Bulevar Oslobodjenja 10", CreatedAt = now, UpdatedAt = now };

            db.Centers.AddRange(centerOne, centerTwo);
            db.SaveChanges();

            // ---------- CenterSports (pivot) ----------
            var centerSports = new List<CenterSport>
            {
                new CenterSport { CenterId = centerOne.Id, SportId = football.Id },
                new CenterSport { CenterId = centerOne.Id, SportId = basketball.Id },
                new CenterSport { CenterId = centerTwo.Id, SportId = tennis.Id },
                new CenterSport { CenterId = centerTwo.Id, SportId = football.Id },
            };

            db.CenterSports.AddRange(centerSports);
            db.SaveChanges();

            // ---------- Courts ----------
            var courtOne = new Court
            {
                Label = "Court A - Football",
                isActive = true,
                SportId = football.Id,
                CenterId = centerOne.Id,
                CreatedAt = now,
                UpdatedAt = now
            };

            var courtTwo = new Court
            {
                Label = "Court B - Basketball",
                isActive = true,
                SportId = basketball.Id,
                CenterId = centerOne.Id,
                CreatedAt = now,
                UpdatedAt = now
            };

            var courtThree = new Court
            {
                Label = "Court C - Tennis",
                isActive = true,
                SportId = tennis.Id,
                CenterId = centerTwo.Id,
                CreatedAt = now,
                UpdatedAt = now
            };

            db.Courts.AddRange(courtOne, courtTwo, courtThree);
            db.SaveChanges();

            // ---------- Reservations ----------
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    StartTime = now.AddDays(1).Date.AddHours(10), // tomorrow 10:00
                    Status = ReservationStatus.Confirmed,
                    UserId = normalUser.Id,
                    CourtId = courtOne.Id,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Reservation
                {
                    StartTime = now.AddDays(2).Date.AddHours(14), // in 2 days, 14:00
                    Status = ReservationStatus.Confirmed,
                    UserId = normalUser.Id,
                    CourtId = courtTwo.Id,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Reservation
                {
                    StartTime = now.AddDays(-3).Date.AddHours(18), // 3 days ago, 18:00 (completed/past)
                    Status = ReservationStatus.Completed,
                    UserId = normalUser.Id,
                    CourtId = courtThree.Id,
                    CreatedAt = now.AddDays(-3),
                    UpdatedAt = now.AddDays(-3)
                },
                new Reservation
                {
                    StartTime = now.AddDays(5).Date.AddHours(9),
                    Status = ReservationStatus.Cancelled,
                    UserId = adminUser.Id,
                    CourtId = courtOne.Id,
                    CreatedAt = now,
                    UpdatedAt = now
                },
            };

            db.Reservations.AddRange(reservations);
            db.SaveChanges();

            // ---------- Reviews ----------
            var reviews = new List<Review>
            {
                new Review
                {
                    Rating = 5,
                    Comment = "Great pitch, well maintained.",
                    UserId = normalUser.Id,
                    CourtId = courtOne.Id,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Review
                {
                    Rating = 4,
                    Comment = "Good court but lighting could be better in the evening.",
                    UserId = normalUser.Id,
                    CourtId = courtTwo.Id,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Review
                {
                    Rating = 3,
                    Comment = "Decent tennis court, a bit far from parking.",
                    UserId = adminUser.Id,
                    CourtId = courtThree.Id,
                    CreatedAt = now,
                    UpdatedAt = now
                },
            };

            db.Reviews.AddRange(reviews);
            db.SaveChanges();
        }

        /// <summary>
        /// Matches the hashing method already used in your registration flow:
        /// PasswordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.Password)));
        /// </summary>
        private static string HashPassword(string password)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }
    }
}