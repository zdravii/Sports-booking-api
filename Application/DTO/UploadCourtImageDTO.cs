using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UploadCourtImageDTO
    {
        public int CourtId { get; set; }
        public IFormFile File { get; set; }
        public string? Alt { get; set; }
    }
}
