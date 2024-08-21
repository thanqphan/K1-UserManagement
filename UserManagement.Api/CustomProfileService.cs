using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserManagement.Core.Entities;

namespace UserManagement.Api
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public CustomProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            // Add roles to the claims
            var roles = await _userManager.GetRolesAsync(user);
            var claims = roles.Select(role => new Claim(JwtClaimTypes.Role, role));

            context.AddRequestedClaims(claims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }

}
