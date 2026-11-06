using Application.Commands;
using Application.Commands.Sport;
using Application.DTO;
using Application.DTO.Sport;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class AuditedCreateReviewCommand : ICreateReviewCommand
    {
        private readonly ICreateReviewCommand _innerCommand;
        private readonly IAuditLogger _auditLogger;
        private readonly ICurrentUserService _currentUserService;

        public AuditedCreateReviewCommand(CreateReviewCommand innerCommand, IAuditLogger auditLogger, ICurrentUserService currentUserService)
        {
            _innerCommand = innerCommand;
            _auditLogger = auditLogger;
            _currentUserService = currentUserService;
        }

        public string Id => _innerCommand.Id;

        public string Name => _innerCommand.Name;

        public void Execute(CreateReviewDTO request)
        {
            _innerCommand.Execute(request);
            _auditLogger.Log(_innerCommand.Id, _innerCommand.Name, request, _currentUserService.UserId);
        }
    }
}
