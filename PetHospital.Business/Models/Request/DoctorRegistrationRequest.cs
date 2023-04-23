using System.ComponentModel.DataAnnotations;

namespace PetHospital.Business.Models.Request
{
    public class DoctorRegistrationRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Firstname { set; get; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Lastname { set; get; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 8)]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { set; get; } = string.Empty;
        [Required]
        public string Password { set; get; } = string.Empty;
    }
}
