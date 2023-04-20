using PetHospital.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
