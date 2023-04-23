using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;

namespace PetHospital.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserInfoAsync(string userId);
        Task<UserResponse> UpdateAsync(string userId, UserRequest userRequest);
        Task DeleteAsync(string userId);
        Task<UserResponse> ChangePasswordAsync(string userId, ChangePasswordRequest passwordRequest);
  
    }
}
