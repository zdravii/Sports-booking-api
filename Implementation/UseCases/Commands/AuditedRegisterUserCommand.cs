using Application.Commands;
using Application.DTO;
using Application.DTO.Auth;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class AuditedRegisterUserCommand : IRegisterUserCommand
    {
        private readonly IRegisterUserCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;

        public AuditedRegisterUserCommand(RegisterUserCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        int IRegisterUserCommand.Execute(RegisterUserDTO request)
        {
            int newUserId = _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, newUserId);
            return newUserId;
        }
    }
}
