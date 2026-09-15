using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        private ClaimsPrincipal User => 
            _httpContextAccessor.HttpContext?.User
            ?? throw new InvalidOperationException("No HttpContext available.");

        public Guid UserId =>
            Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException("User ID not found.")
            );

        public string Email => 
            User.FindFirstValue(ClaimTypes.Email) 
            ?? throw new InvalidOperationException("Email not found.");

        public string Role => 
            User.FindFirstValue(ClaimTypes.Role)
            ?? throw new InvalidOperationException("Role not found.");
    }
}
