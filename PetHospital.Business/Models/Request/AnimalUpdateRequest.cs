using PetHospital.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Request
{
    public class AnimalUpdateRequest
    {
        public string? AnimalName { get; set; }
        public string? AnimalDescription { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
