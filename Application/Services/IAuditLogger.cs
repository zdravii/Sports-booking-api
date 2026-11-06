using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IAuditLogger
    {
        void Log(string action, string name, object? requestData,int userId);
    }
}
