using Application.Commands.Court;
using Application.DTO.Courts;
using Application.Services;
using Implementation.UseCases.Commands.Court;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Courts.Audited
{
    public class AuditedDeleteCourtCommand : IDeleteCourtCommand
    {
        private readonly IDeleteCourtCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedDeleteCourtCommand(DeleteCourtCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
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
