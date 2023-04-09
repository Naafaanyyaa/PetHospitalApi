using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Request
{
    public class UserRequest
    {
        [Required]
        [StringLength(18, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(18, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
