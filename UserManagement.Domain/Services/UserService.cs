using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;
using UserManagement.Core.Extensions;
using UserManagement.Core.Repositories;
using UserManagement.Core.Services;
using UserManagement.Core.ViewModels.Requests.User;
using UserManagement.Core.ViewModels.Responses;
using static UserManagement.Core.Extensions.PagingExtension;

namespace UserManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetUserByIdAsync(request.Id) ?? throw new InvalidOperationException("User does not exist!");
            if (!user.Password.VerifyPassword(request.OldPassword, user.Id))
            {
                throw new Exception("Old password is incorrect.");
            }

            if (user.Password.VerifyPassword(request.NewPassword, user.Id))
            {
                throw new Exception("New password is the same as Old password.");
            }

            user.LastUpdatedAt = DateTime.UtcNow;
            user.Password = request.NewPassword.HashPassword(user.Id);
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id) ?? throw new InvalidOperationException("User does not exist!");

            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<UserResponse> EditUserAsync(UserUpdateRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetUserByIdAsync(request.Id) ?? throw new InvalidOperationException("User does not exist!");
            _mapper.Map(request, user);
            await _userRepository.UpdateUserAsync(user);

            return _mapper.Map<UserResponse>(user); ;
        }

        public async Task<PagedResult<UserResponse>> ListUsersAsync(UserGetAllRequest request)
        {
            var query = _userRepository.GetAllUsers();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(u => EF.Functions.Like(u.Username, $"%{request.SearchQuery}%")
                                    || EF.Functions.Like(u.Fullname, $"%{request.SearchQuery}%")
                                    || EF.Functions.Like(u.PhoneNumber, $"%{request.SearchQuery}%")
                                    || EF.Functions.Like(u.Email, $"%{request.SearchQuery}%")
                                    );
            }

            var pagedResult = await query.ToPagedResult(request.PageIndex, request.PageSize);

            var mappedItems = _mapper.Map<List<UserResponse>>(pagedResult.Items);

            return new PagedResult<UserResponse>
            {
                Items = mappedItems,
                Total = pagedResult.Total,
                PageSize = pagedResult.PageSize,
                Skipped = pagedResult.Skipped,
            };
        }

        public async Task<UserResponse> LoginUserAsync(UserLoginRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetUserByUsernameAsync(request.UserName) ?? throw new InvalidOperationException("User does not exist!");
            if (!user.Password.VerifyPassword(request.Password, user.Id))
            {
                throw new Exception("User password is incorrect.");
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> RegisterUserAsync(UserRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var userExisted = _userRepository.GetAllUsers()
                                .FirstOrDefault(x =>
                                    x.Username == request.Username ||
                                    x.PhoneNumber == request.PhoneNumber ||
                                    x.Email == request.Email
            );
            if (userExisted != null) throw new Exception("User existed!");

            var userId = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = userId,
                Username = request.Username,
                Email = request.Email,
                Password = PasswordExtension.HashPassword(request.Password, userId),
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,
                Fullname = request.Fullname,
                PhoneNumber = request.PhoneNumber,
            };

            await _userRepository.AddUserAsync(user);

            return _mapper.Map<UserResponse>(user);
        }
    }
}
