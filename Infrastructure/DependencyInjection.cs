using Application.Common.Interfaces.Services;
using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
                        .UseSnakeCaseNamingConvention()
            );

            services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
