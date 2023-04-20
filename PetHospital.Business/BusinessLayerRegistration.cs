using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Services;
using PetHospital.Data;
using System.Reflection;
using PetHospital.Business.Infrastructure;

namespace PetHospital.Business
{
    public static class BusinessLayerRegistration
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration);
            services.AddScoped<IRegistrationService, RegistrationService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IRoleInitializer, RoleInitializer>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IClinicService, ClinicService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<JwtHandler>();

            return services;
        }
    }
}
