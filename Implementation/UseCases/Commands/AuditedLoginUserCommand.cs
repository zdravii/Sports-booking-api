using Application.Commands;
using Application.DTO.Auth;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class AuditedLoginUserCommand : ILoginUserCommand
    {
        private readonly ILoginUserCommand _innerCommand;
        private readonly IAuditLogger _logger;
        public AuditedLoginUserCommand(LoginUserCommand innerCommand,IAuditLogger logger)
        {
            _innerCommand = innerCommand;
            _logger = logger;
        }
        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public LoginResponseDTO Execute(LoginUserDTO request)
        {
            var response = _innerCommand.Execute(request);
            _logger.Log(_innerCommand.Id, _innerCommand.Name, request, response.UserId);
            return response;
        }
    }
}
