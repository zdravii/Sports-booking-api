using Application.Commands;
using Application.DTO;
using Application.Services;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators;
using Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class UploadCourtImageCommand : UseCase, IUploadCourtImageCommand
    {
        private readonly UploadCourtImageValidator _uploadCourtImageValidator;
        private readonly IFileStorageService _fileStorageService;

        public UploadCourtImageCommand(DataBaseContext db,UploadCourtImageValidator uploadCourtImageValidator,IFileStorageService fileStorageService) : base(db)
        {
            _uploadCourtImageValidator = uploadCourtImageValidator;
            _fileStorageService = fileStorageService;
        }

        public string Id => "upload-image";

        public string Name => "Upload image";

        public void Execute(UploadCourtImageDTO request)
        {
            _uploadCourtImageValidator.ValidateAndThrow(request);

            var court = _db.Courts.FirstOrDefault(c => c.Id == request.CourtId);

            if (court == null)
                throw new NotFoundException("Court does not exist");

            string relativeFilePath = _fileStorageService.SaveFile(request.File, "uploads/court");

            var courtImage = new CourtImage
            {
                Path = relativeFilePath,
                CourtId = request.CourtId,
                Alt = request.Alt ?? court.Label
            };

            _db.CourtImages.Add(courtImage);
            _db.SaveChanges();
        }
    }
}
