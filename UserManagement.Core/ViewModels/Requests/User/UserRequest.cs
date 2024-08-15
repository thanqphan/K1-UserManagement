using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserManagement.Core.ViewModels.Requests.User
{
    public class UserRequest
    {
        public string Username { get; set; } = string.Empty;
        public virtual string Password { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }
}
