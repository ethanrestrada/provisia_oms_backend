using Application.Common.Interfaces.Services;

namespace Infrastructure.Services
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string providedPassword, string hashedPassword) => 
            BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
