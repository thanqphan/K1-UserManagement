using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.ViewModels.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
