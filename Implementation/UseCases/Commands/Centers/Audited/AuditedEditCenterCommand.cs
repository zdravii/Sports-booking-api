using Application.Commands.Center;
using Application.DTO.Center;
using Application.Services;
using Implementation.UseCases.Commands.Center;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands.Centers.Audited
{
    public class AuditedEditCenterCommand : IEditCenterCommand
    {
        private readonly IEditCenterCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedEditCenterCommand(EditCenterCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
            _currentUserService = currentUserService;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(EditCenterDTO request)
        {
            _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
