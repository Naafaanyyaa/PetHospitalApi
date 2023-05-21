using PetHospital.Data.Enums;

namespace PetHospital.Business.Models.Request
{
    public class AnimalAllRequest
    {
        public string? UserId { get; set; }
        public string? SearchString { get; set; }
        public AnimalType? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
