using PetHospital.Data.Entities.Abstract;


namespace PetHospital.Data.Entities;

public class Diseases : BaseEntity
{
    public string NameOfDisease { get; set; } = string.Empty;
    public string DiseaseDescription { get; set;} = string.Empty;
    public string AnimalId { get; set; } = string.Empty;
    public virtual Animal Animal { get; set; } = new Animal();
}