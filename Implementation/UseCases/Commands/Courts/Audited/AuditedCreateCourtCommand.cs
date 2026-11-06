using Application.Commands.Center;
using Application.Commands.Court;
using Application.DTO.Center;
using Application.DTO.Courts;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Courts.Audited
{
    public class AuditedCreateCourtCommand : ICreateCourtCommand
    {
        private readonly ICreateCourtCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedCreateCourtCommand(CreateCourtCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
            _currentUserService = currentUserService;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(CreateCourtDTO request)
        {
            _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
