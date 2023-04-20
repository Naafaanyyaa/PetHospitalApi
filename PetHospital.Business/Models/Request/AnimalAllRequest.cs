using PetHospital.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Request
{
    public class AnimalAllRequest
    {
        public string? SearchString { get; set; }
        public AnimalType? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
