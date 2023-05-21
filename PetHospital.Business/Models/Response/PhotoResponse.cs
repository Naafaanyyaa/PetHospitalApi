
namespace PetHospital.Business.Models.Response
{
    public class PhotoResponse : BaseResponse.BaseResponse
    {
        public string AnimalId { get; set; } = null!;
        public string PhotoPath { get; set; }
    }
}
