using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Interfaces;
using PetHospital.Data.Repositories;

namespace PetHospital.Data
{
    public static class DataLayerRegistration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>()
                .AddUserStore<UserStore<User, Role, ApplicationDbContext, string, IdentityUserClaim<string>, UserRole,
                    IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
                .AddRoleStore<RoleStore<Role, ApplicationDbContext, string, UserRole, IdentityRoleClaim<string>>>()
                .AddSignInManager<SignInManager<User>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IClinicRepository, ClinicRepository>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();

            return services;
        }
    }
}
