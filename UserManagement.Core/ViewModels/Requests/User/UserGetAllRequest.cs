using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.ViewModels.Requests.User
{
    public class UserGetAllRequest
    {
        public string SearchQuery { get; set; } = string.Empty;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
