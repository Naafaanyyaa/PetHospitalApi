using NetTopologySuite.Geometries;
using PetHospital.Data.Entities.Abstract;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Entities;
using PetHospital.Data.Enums;


namespace SavePets.Data.Entities;

public class Animal : BaseEntity
{
    public string AnimalName { get; set; }
    public string AnimalDescription { get; set; }
    public AnimalType AnimalType { get; set; }
    public string UserId { get; set; }
    public virtual List<Photo> Photos { get; set; }
    public virtual List<Diseases> Diseases {get; set; }
    public virtual User User { get; set; }
}