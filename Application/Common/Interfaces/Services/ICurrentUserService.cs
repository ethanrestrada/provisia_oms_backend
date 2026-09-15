namespace Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Email { get; }
        string Role { get; }
    }
}
