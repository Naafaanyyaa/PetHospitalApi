
using PetHospital.Business.Models.Request;

namespace PetHospital.Business.Models.Response
{
    public class DiseaseResponse : BaseResponse.BaseResponse
    {
        public string NameOfDisease { get; set; } = string.Empty;
        public string DiseaseDescription { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public string AnimalId { get; set; } = string.Empty;
    }
}
