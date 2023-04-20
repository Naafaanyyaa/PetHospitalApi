using PetHospital.Data.Entities.Abstract;


namespace PetHospital.Data.Entities;

public class Diseases : BaseEntity
{
    public string NameOfDisease { get; set; }
    public string DiseaseDescription { get; set;}
    public string AnimalId { get; set; }
    public virtual Animal Animal { get; set; }
}