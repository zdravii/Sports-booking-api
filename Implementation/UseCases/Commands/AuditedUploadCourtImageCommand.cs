using Application.Commands;
using Application.DTO;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class AuditedUploadCourtImageCommand : IUploadCourtImageCommand
    {
        private readonly IUploadCourtImageCommand _innerCommand;
        private readonly IAuditLogger _logger;
        private readonly ICurrentUserService _currentUserService;
        public AuditedUploadCourtImageCommand(UploadCourtImageCommand innercommand,IAuditLogger logger,ICurrentUserService currentUserService)
        {
            _innerCommand = innercommand;
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(UploadCourtImageDTO request)
        {
            _innerCommand.Execute(request);
            _logger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
