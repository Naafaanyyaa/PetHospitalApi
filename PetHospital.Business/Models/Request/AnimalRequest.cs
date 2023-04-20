using PetHospital.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PetHospital.Business.Models.Request
{
    public class AnimalRequest
    {
        [Required]
        public string AnimalName { get; set; } = string.Empty;
        public string? AnimalDescription { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
