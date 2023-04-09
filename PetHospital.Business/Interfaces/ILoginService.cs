using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface ILoginService
    {
        Task<AuthorizeResponse> Login(AuthenticateRequest request);
    }
}
