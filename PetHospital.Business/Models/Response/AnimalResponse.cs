using PetHospital.Data.Enums;

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
        public virtual List<DiseaseResponse> Diseases { get; set; } = new List<DiseaseResponse>();
        public virtual UserResponse User { get; set; } = new UserResponse();
    }
}
