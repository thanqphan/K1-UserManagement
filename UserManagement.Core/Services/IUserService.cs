using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Core.ViewModels.Requests.User;
using UserManagement.Core.ViewModels.Responses;
using static UserManagement.Core.Extensions.PagingExtension;

namespace UserManagement.Core.Services
{
    public interface IUserService
    {
        Task<UserResponse> RegisterUserAsync(UserRequest request);
        Task<UserResponse> LoginUserAsync(UserLoginRequest request);
        Task ChangePasswordAsync(ChangePasswordRequest request);
        Task DeleteUserAsync(string id);
        Task<UserResponse> EditUserAsync(UserUpdateRequest request);
        Task<PagedResult<UserResponse>> ListUsersAsync(UserGetAllRequest request);
    }
}
