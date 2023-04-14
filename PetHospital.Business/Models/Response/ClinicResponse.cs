using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Business.Models.Response
{
    public class ClinicResponse : BaseResponse.BaseResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsBanned { get; set; } = false;
    }
}
