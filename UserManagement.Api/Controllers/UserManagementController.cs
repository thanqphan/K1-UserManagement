using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Core.Services;
using UserManagement.Core.ViewModels.Requests.User;
using UserManagement.Core.ViewModels.Responses;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                var user = await _userService.LoginUserAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("edit")]
        public async Task<IActionResult> Edit([FromBody] UserUpdateRequest request)
        {
            try
            {
                var user = await _userService.EditUserAsync(request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassWord([FromBody] ChangePasswordRequest request)
        {
            try
            {
                await _userService.ChangePasswordAsync(request);
                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserGetAllRequest request)
        {
            try
            {
                var users = await _userService.ListUsersAsync(request);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
