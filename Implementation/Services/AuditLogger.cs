using Application.Services;
using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Services
{
    public class AuditLogger : IAuditLogger
    {
        private readonly DataBaseContext _db;

        public AuditLogger(DataBaseContext db)
        {
            _db = db;
        }

        public void Log(string action, string name, object? requestData, int userId)
        {
            try
            {
                string description = requestData != null ? $"{name} - {System.Text.Json.JsonSerializer.Serialize(requestData)}" : name;

                AuditLog auditLogToAdd = new AuditLog
                {
                    Action = action,
                    Description = description,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                _db.AuditLogs.Add(auditLogToAdd);
                _db.SaveChanges();
            }
            catch 
            {

            }
        }
    }
}
