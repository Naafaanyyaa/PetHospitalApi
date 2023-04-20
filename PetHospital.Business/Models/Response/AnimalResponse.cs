using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Entities;
using PetHospital.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Response
{
    public class AnimalResponse
    {
        public string Id { get; set; } = string.Empty;
        public string AnimalName { get; set; } = string.Empty;
        public string? AnimalDescription { get; set; }
        public AnimalType AnimalType { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual List<PhotoResponse> Photos { get; set; } = new List<PhotoResponse>();
        public virtual List<DiseasesResponse> Diseases { get; set; } = new List<DiseasesResponse>();
        public virtual UserResponse User { get; set; } = new UserResponse();
    }
}
