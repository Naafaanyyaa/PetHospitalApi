using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Interfaces
{
    public interface IAdminService
    {
        Task<UserResponse> BanUser(string userId);
        Task<ClinicResponse> BanClinic(string clinicId);
        Task<UserResponse> ChangeRole(string userId, RoleEnum role);
        Task<List<UserResponse>> GetUserList(UserAllRequest request);
        Task<UserResponse> GetUserByUserName(string userName);
    }
}
