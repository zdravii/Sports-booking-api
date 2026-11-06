using Application.Commands.Reservation;
using Application.DTO.Reservation;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Reservations.Audited
{
    public class AuditedDeleteReservationCommand : IDeleteReservationCommand
    {
        private readonly IDeleteReservationCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedDeleteReservationCommand(DeleteReservationCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
            _currentUserService = currentUserService;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(int request)
        {
            _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
