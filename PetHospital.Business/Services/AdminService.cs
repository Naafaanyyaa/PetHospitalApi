using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Enums;
using PetHospital.Data.Interfaces;

namespace PetHospital.Business.Services
{
    public class AdminService : IAdminService
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IClinicRepository _clinicRepository;

        public AdminService(UserManager<User> userManager, IMapper mapper, IClinicRepository clinicRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _clinicRepository = clinicRepository;
        }

        public async Task<UserResponse> BanUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user.IsBanned = !user.IsBanned;

            await _userManager.UpdateAsync(user);

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }

        public async Task<ClinicResponse> BanClinic(string clinicId)
        {
            var clinic = await _clinicRepository.GetByIdAsync(clinicId);

            if (clinic == null)
            {
                throw new NotFoundException("Clinic is not found");
            }

            clinic.IsBanned = !clinic.IsBanned;

            await _clinicRepository.UpdateAsync(clinic);

            var result = _mapper.Map<Clinic, ClinicResponse>(clinic);

            return result;
        }

        public async Task<UserResponse> ChangeRole(string userId, RoleEnum role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var usersWithRole = await _userManager.GetUsersInRoleAsync(role switch
            {
                RoleEnum.Admin => CustomRoles.AdminRole,
                RoleEnum.Doctor => CustomRoles.DoctorRole,
                RoleEnum.HospitalHost => CustomRoles.HospitalHost,
                RoleEnum.User => CustomRoles.UserRole,
                _ => CustomRoles.UserRole
            });

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (usersWithRole.Contains(user))
            {
                throw new ValidationException($"User with such id has already been {role}");
            }

            await _userManager.AddToRolesAsync(user, new List<string>
            {
                role switch
                {
                    RoleEnum.Admin => CustomRoles.AdminRole,
                    RoleEnum.Doctor => CustomRoles.DoctorRole
                }
            });

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }
        public async Task<List<UserResponse>> GetUserList(UserAllRequest request)
        {
            var userList = await _userManager.Users.Where(x => (x.UserName.Contains(request.UserName) && x.Email.Contains(request.Email))).ToListAsync();

            var result = _mapper.Map<List<User>, List<UserResponse>>(userList);

            return result;
        }

        public async Task<UserResponse> GetUserByUserName(string name)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == name);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = _mapper.Map<User, UserResponse>(user);

            return result;
        }
    }
}
