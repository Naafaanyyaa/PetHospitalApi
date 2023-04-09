using System.ComponentModel.DataAnnotations;

namespace PetHospital.Business.Models.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { set; get; } = string.Empty;
        [Required]
        public string Password { set; get; } = string.Empty;
    }
}
