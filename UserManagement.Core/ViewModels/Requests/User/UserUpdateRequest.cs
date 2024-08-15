using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserManagement.Core.ViewModels.Requests.User
{
    public class UserUpdateRequest : UserRequest
    {
        public string Id { get; set; }
        [JsonIgnore]
        override public string Password {get; set; } = string.Empty;
    }
}
