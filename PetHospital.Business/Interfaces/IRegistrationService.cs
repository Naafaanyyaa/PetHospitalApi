using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponse> Registration(RegistrationRequest request);
    }
}
