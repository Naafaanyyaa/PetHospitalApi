using System.ComponentModel.DataAnnotations;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Models.Request
{
    public class RegistrationRequest
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
        public RoleEnum Role { set; get; }
    }
}
