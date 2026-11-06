using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IFileStorageService
    {
        string SaveFile(IFormFile file, string subFolder);
    }
}
