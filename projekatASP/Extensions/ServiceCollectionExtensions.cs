using Application.Commands;
using Application.Commands.Center;
using Application.Commands.Court;
using Application.Commands.Reservation;
using Application.Commands.Sport;
using Application.Jwt;
using Application.Queries;
using Application.Services;
using DataAccess;
using Implementation.Jwt;
using Implementation.Services;
using Implementation.UseCases.Commands;
using Implementation.UseCases.Commands.Center;
using Implementation.UseCases.Commands.Centers;
using Implementation.UseCases.Commands.Centers.Audited;
using Implementation.UseCases.Commands.Court;
using Implementation.UseCases.Commands.Courts;
using Implementation.UseCases.Commands.Courts.Audited;
using Implementation.UseCases.Commands.Reservations;
using Implementation.UseCases.Commands.Reservations.Audited;
using Implementation.UseCases.Commands.Sport;
using Implementation.UseCases.Commands.Sports.Audited;
using Implementation.UseCases.Queries;
using Implementation.UseCases.Queries.Centers;
using Implementation.UseCases.Queries.Reservations;
using Implementation.Validators;
using Implementation.Validators.Auth;
using Implementation.Validators.Centers;
using Implementation.Validators.Courts;
using Implementation.Validators.Sports;
using Microsoft.EntityFrameworkCore;
using projekatASP.API.BackgroundServices;
using projekatASP.API.Services;

namespace projekatASP.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<DataBaseContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(
                           configuration.GetConnectionString("DefaultConnection")
                       ));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddScoped<IJwtHandler, JwtHandler>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IAuditLogger, AuditLogger>();

            services.AddScoped<ISearchAuditLogQuery, SearchAuditLogQuery>();

            services.AddScoped<RegisterUserCommand>();
            services.AddScoped<IRegisterUserCommand, AuditedRegisterUserCommand>();
            services.AddScoped<RegisterUserValidator>();

            services.AddScoped<LoginUserCommand>();
            services.AddScoped<ILoginUserCommand, AuditedLoginUserCommand>();
            services.AddScoped<LoginUserValidator>();

            services.AddScoped<ISearchUserQuery, SearchUserQuery>();

            services.AddScoped<CreateReviewCommand>();
            services.AddScoped<ICreateReviewCommand, AuditedCreateReviewCommand>();
            services.AddScoped<CreateReviewValidator>();

            services.AddAuthorization();

            services.AddHostedService<ReservationStatusUpdaterService>();

            // Center UseCases

            services.AddScoped<CreateCenterCommand>();
            services.AddScoped<ICreateCenterCommand, AuditedCreateCenterCommand>();
            services.AddScoped<CreateCenterValidator>();
            services.AddScoped<ISearchCentersQuery, SearchCentersQuery>();
            services.AddScoped<ICenterDetailsQuery, CenterDetailsQuery>();
            services.AddScoped<EditCenterCommand>();
            services.AddScoped<IEditCenterCommand, AuditedEditCenterCommand>();
            services.AddScoped<EditCenterValidator>();
            services.AddScoped<DeleteCenterCommand>();
            services.AddScoped<IDeleteCenterCommand, AuditedDeleteCenterCommand>();
            services.AddScoped<ToggleCenterSportsCommand>();
            services.AddScoped<IToggleCenterSportsCommand, AuditedToggleCenterSportsCommand>();
            services.AddScoped<ToggleCenterSportsValidator>();

            // Center UseCases

            // Sport UseCases

            services.AddScoped<CreateSportCommand>();
            services.AddScoped<ICreateSportCommand, AuditedCreateSportCommand>();
            services.AddScoped<CreateSportValidator>();
            services.AddScoped<ISearchSportQuery, SearchSportQuery>();
            services.AddScoped<ISportDetailsQuery, SportDetailsQuery>();
            services.AddScoped<EditSportCommand>();
            services.AddScoped<IEditSportCommand, AuditedEditSportCommand>();
            services.AddScoped<EditSportValidator>();
            services.AddScoped<DeleteSportCommand>();
            services.AddScoped<IDeleteSportCommand, AuditedDeleteSportCommand>();

            // Sport UseCases

            // Court UseCases

            services.AddScoped<ISearchCourtQuery, SearchCourtsQuery>();
            services.AddScoped<ICourtDetailsQuery, CourtDetailsQuery>();
            services.AddScoped<CreateCourtCommand>();
            services.AddScoped<ICreateCourtCommand, AuditedCreateCourtCommand>();
            services.AddScoped<CreateCourtValidator>();
            services.AddScoped<EditCourtCommand>();
            services.AddScoped<IEditCourtCommand, AuditedEditCourtCommand>();
            services.AddScoped<EditCourtValidator>();
            services.AddScoped<DeleteCourtCommand>();
            services.AddScoped<IDeleteCourtCommand, AuditedDeleteCourtCommand>();
            services.AddScoped<ICourtReviewsQuery, CourtReviewsQuery>();
            services.AddScoped<UploadCourtImageCommand>();
            services.AddScoped<IUploadCourtImageCommand, AuditedUploadCourtImageCommand>();
            services.AddScoped<UploadCourtImageValidator>();

            // Court UseCases

            // Reservation UseCases

            services.AddScoped<CreateReservationCommand>();
            services.AddScoped<ICreateReservationCommand, AuditedCreateReservationCommand>();
            services.AddScoped<CreateReservationValidator>();
            services.AddScoped<DeleteReservationCommand>();
            services.AddScoped<IDeleteReservationCommand, AuditedDeleteReservationCommand>();
            services.AddScoped<IUserReservationsQuery, UserReservationQuery>();
            services.AddScoped<ISearchReservationsQuery, SearchReservationQuery>();

            // Reservation UseCases

            return services;
        }
    }
}
