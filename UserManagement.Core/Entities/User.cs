using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Entities
{
    public class User
    {
        [MaxLength(36)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(200)]
        public string Fullname { get; set; }
        public string Password { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
