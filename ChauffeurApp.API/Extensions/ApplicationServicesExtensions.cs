using ChauffeurApp.Application.Services.IServices;
using ChauffeurApp.Application.Services;
using ChauffeurApp.DataAccess.Repositories.IRepositories;
using ChauffeurApp.DataAccess.Repositories;

namespace ChauffeurApp.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
