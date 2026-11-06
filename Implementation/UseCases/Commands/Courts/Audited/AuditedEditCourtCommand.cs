using Application.Commands.Court;
using Application.DTO.Court;
using Application.Services;
using Implementation.UseCases.Commands.Court;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Courts.Audited
{
    public class AuditedEditCourtCommand : IEditCourtCommand
    {
        private readonly IEditCourtCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedEditCourtCommand(EditCourtCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
            _currentUserService = currentUserService;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(EditCourtDTO request)
        {
            _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
