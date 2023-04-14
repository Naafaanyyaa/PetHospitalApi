using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
