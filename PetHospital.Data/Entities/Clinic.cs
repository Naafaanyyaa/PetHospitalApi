using PetHospital.Data.Entities.Abstract;
using PetHospital.Data.Entities.Identity;
using SavePets.Data.Entities;

namespace PetHospital.Data.Entities;

public class Clinic : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsBanned { get; set; } = false;
    public virtual List<UserClinic> UserClinic { get; set; } = new List<UserClinic>();
}