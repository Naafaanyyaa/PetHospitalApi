using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Data.Entities.Abstract;

namespace PetHospital.Data.Entities
{
    public class AnimalClinic : BaseEntity
    {
        public string AnimalId { get; set; } = string.Empty;
        public virtual Animal Animal { get; set; } = new Animal();
        public string ClinicId { get; set; } = string.Empty;
        public virtual Clinic Clinic { get; set; } = new Clinic();

    }
}
