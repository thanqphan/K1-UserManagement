using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
        IQueryable<User> GetAllUsers();
    }
}
