
namespace PetHospital.Business.Models.Response
{
    public class AuthorizeResponse
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
