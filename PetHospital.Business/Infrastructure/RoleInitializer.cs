using Microsoft.AspNetCore.Identity;
using PetHospital.Business.Interfaces;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Business.Infrastructure
{
    public class RoleInitializer : IRoleInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleInitializer(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeIdentityData()
        {
            RegisterRoleAsync(CustomRoles.AdminRole).Wait();
            RegisterRoleAsync(CustomRoles.UserRole).Wait();
            RegisterRoleAsync(CustomRoles.DoctorRole).Wait();
            RegisterRoleAsync(CustomRoles.HospitalHost).Wait();
        }

        private async Task<Role> RegisterRoleAsync(string roleName)
        {

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                return role;
            }

            role = new Role(roleName);
            await _roleManager.CreateAsync(role);

            return role;
        }
    }
}
