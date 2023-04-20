using PetHospital.Data.Entities.Abstract;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Enums;

namespace PetHospital.Data.Entities;

public class Animal : BaseEntity
{
    public string AnimalName { get; set; } = string.Empty;
    public string? AnimalDescription { get; set; } 
    public AnimalType AnimalType { get; set; }
    public string UserId { get; set; } = string.Empty;
    public virtual List<Photo> Photos { get; set; } = new List<Photo>();
    public virtual List<Diseases> Diseases {get; set; } = new List<Diseases>();
    public virtual User User { get; set; } = new User();
    public virtual List<AnimalClinic> AnimalClinic { get; set; } = new List<AnimalClinic>();
}