using PetHospital.Data.Entities.Abstract;

namespace PetHospital.Data.Entities
{
    public class AnimalClinic : BaseEntity
    {
        public string AnimalId { get; set; } = string.Empty;
        public virtual Animal Animal { get; set; }
        public string ClinicId { get; set; } = string.Empty;
        public virtual Clinic Clinic { get; set; }

    }
}
