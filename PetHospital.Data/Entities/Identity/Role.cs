using Microsoft.AspNetCore.Identity;

namespace PetHospital.Data.Entities.Identity;

public class Role : IdentityRole
{
    public Role() : base() { } 

    public Role(string roleName) : base(roleName) { }

    public virtual List<UserRole> UserRoles { get; set; }
}