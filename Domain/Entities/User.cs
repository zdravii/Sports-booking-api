using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
        public virtual IEnumerable<Review> Reviews { get; set; } = new List<Review>();
        public virtual IEnumerable<AuditLog> Logs { get; set; } = new List<AuditLog>();
     }
}
