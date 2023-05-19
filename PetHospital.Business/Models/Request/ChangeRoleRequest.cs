using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Models.Request
{
    public class ChangeRoleRequest
    {
        public string userId { get; set; }
        public RoleEnum Role { get; set; }
    }
}
