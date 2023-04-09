using Microsoft.AspNetCore.Identity;
using SavePets.Data.Entities;

namespace PetHospital.Data.Entities.Identity;

public class User : IdentityUser
{
    public bool IsBanned { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? SubscriptionId { get; set; }
    public virtual List<Contacts>? Contacts { get; set; } = new List<Contacts>();
    public virtual Subscription? Subscription { get; set; }
    public virtual List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual List<Animal>? Animals { get; set; } = new List<Animal>();
    public virtual List<Clinic>? Clinic { get; set; } = new List<Clinic>();
}