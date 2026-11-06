using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class AuditLogDTO
    {
        public string AuditLogName { get; set; }
        public string User { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Description { get; set; }
    }
}
