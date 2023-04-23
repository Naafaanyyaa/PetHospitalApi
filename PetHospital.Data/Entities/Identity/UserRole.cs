using Microsoft.AspNetCore.Identity;

namespace PetHospital.Data.Entities.Identity;

public class UserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; } = new User();

    public virtual Role Role { get; set; } = new Role();
}

