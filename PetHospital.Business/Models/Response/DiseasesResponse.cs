using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Business.Models.Response
{
    public class DiseasesResponse : BaseResponse.BaseResponse
    {
        public string NameOfDisease { get; set; } = string.Empty;
        public string DiseaseDescription { get; set; } = string.Empty;
        public string AnimalId { get; set; } = string.Empty;
    }
}
