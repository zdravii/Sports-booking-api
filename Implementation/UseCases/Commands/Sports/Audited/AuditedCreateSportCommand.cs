using Application.Commands.Reservation;
using Application.Commands.Sport;
using Application.DTO.Sport;
using Application.Services;
using Implementation.UseCases.Commands.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Sports.Audited
{
    public class AuditedCreateSportCommand : ICreateSportCommand
    {
        private readonly ICreateSportCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedCreateSportCommand(CreateSportCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
            _currentUserService = currentUserService;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(CreateSportDTO request)
        {
            _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
