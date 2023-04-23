namespace PetHospital.Business.Models.Response.BaseResponse
{
    public class BaseResponse
    {
        public string Id { get; set; } = null!;
        public DateTime CreatedDate { get; set; } 
        public DateTime? LastModifiedDate { get; set; }
    }
}
