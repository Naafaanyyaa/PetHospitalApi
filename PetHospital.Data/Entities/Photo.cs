using PetHospital.Data.Entities.Abstract;


namespace PetHospital.Data.Entities;

public class Photo : BaseEntity
{
    public string AnimalId { get; set; } = string.Empty;
    public virtual Animal Animal { get; set; } = new Animal();
    public string PhotoPath { get; set; } = string.Empty;
}