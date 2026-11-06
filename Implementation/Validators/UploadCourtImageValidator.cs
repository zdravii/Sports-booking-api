using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class UploadCourtImageValidator : AbstractValidator<UploadCourtImageDTO>
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".png", ".jpeg", ".webp" };
        private const long MaxFileSize = 5 * 1024 * 1204;
        public UploadCourtImageValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CourtId).GreaterThan(0)
                                   .WithMessage("Court id is required");

            RuleFor(x => x.File).NotNull()
                                .WithMessage("File is required");

            RuleFor(x => x.File).Must(f => f.Length > 0)
                                .WithMessage("File cannot be empty")
                                .Must(f => f.Length < MaxFileSize)
                                .WithMessage("File cannot be larger than 5MB")
                                .Must(f => AllowedExtensions.Contains(Path.GetExtension(f.FileName).ToLowerInvariant()))
                                .WithMessage("Only .jpg, .jpeg, .png and .webp extensions are allowed")
                                .When(x => x.File != null);
        }
    }
}
