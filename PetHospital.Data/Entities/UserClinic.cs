using PetHospital.Data.Entities.Abstract;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Data.Entities
{
    public class UserClinic : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string ClinicId { get; set; } = string.Empty;
        public virtual Clinic Clinic { get; set; } = new Clinic();
        public virtual User User { get; set; } = new User();
    }
}
