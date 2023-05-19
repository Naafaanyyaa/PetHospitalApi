namespace PetHospital.Business.Models.Response
{
    public class ClinicResponse : BaseResponse.BaseResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsBanned { get; set; } = false;
        public bool IsCreatedByUser { get; set; } = false;
    }
}
