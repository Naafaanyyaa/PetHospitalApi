using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Request
{
    public class HealthRequest
    {
        public int HeartRate { get; set; }
        public double Temperature { get; set; }
        public int Pressure { get; set; }
        public int BloodOxygen { get; set; }
    }
}
