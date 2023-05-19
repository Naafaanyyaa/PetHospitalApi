using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface IIoTDiseaseService
    {
        Task AddDiseaseFromIot(DiseaseIoTRequest diseaseRequest);
    }
}
