using AutoMapper;
using PetHospital.Business.Models.Request;
using PetHospital.Business.Models.Response;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Business.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegistrationRequest, User>()
                .MaxDepth(1);
            CreateMap<User, RegistrationResponse>();
            CreateMap<ClinicRequest, Clinic>();
            CreateMap<Clinic, ClinicResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
        }
    }
}
