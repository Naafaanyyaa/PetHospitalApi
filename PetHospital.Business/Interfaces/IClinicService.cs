﻿using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;


namespace PetHospital.Business.Interfaces
{
    public interface IClinicService
    {
        Task<List<ClinicResponse>> GetAllClinicsByRequest(ClinicAllRequest request);
        Task<ClinicResponse> GetClinicById(string requestId, string userId);
        Task<ClinicResponse> CreateAsync(ClinicRequest request, string userId);
        Task DeleteByIdAsync(string userId, string requestId);
        Task<ClinicResponse> UpdateByIdAsync(string userId, string hospitalId, ClinicRequest request);
        Task<UserResponse>  RegisterDoctor(string userId, string clinicId, DoctorRegistrationRequest userRequest);
    }
}
