using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace projekatASP.API.BackgroundServices
{
    public class ReservationStatusUpdaterService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationStatusUpdaterService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(5);

        public ReservationStatusUpdaterService(IServiceProvider serviceProvider,ILogger<ReservationStatusUpdaterService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await UpdateExpiredReservation(stoppingToken);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error while updating reservation statuses");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task UpdateExpiredReservation(CancellationToken stoppingToken) 
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

            DateTime cutoff = DateTime.UtcNow.AddHours(-1);

            var reservationToUpdate = await db.Reservations.Where(r => r.Status == Domain.Enums.ReservationStatus.Confirmed && r.StartTime <= cutoff)
                                                           .ToListAsync(stoppingToken);

            if (reservationToUpdate.Count() == 0)
                return;

            foreach (var reservation in reservationToUpdate)
            {
                reservation.Status = Domain.Enums.ReservationStatus.Completed;
                reservation.UpdatedAt = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();

            _logger.LogInformation("Marked {Count} reservation(s) as Completed", reservationToUpdate.Count());
        }
    }
}
