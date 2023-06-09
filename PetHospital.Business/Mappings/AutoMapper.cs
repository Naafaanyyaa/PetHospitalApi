﻿using AutoMapper;
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
            CreateMap<User, UserResponse>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id));
            CreateMap<UserRequest, User>();
            CreateMap<Animal, AnimalResponse>();
            CreateMap<AnimalRequest, Animal>();
            CreateMap<Diseases, DiseaseResponse>();
            CreateMap<Photo, PhotoResponse>();
            CreateMap<DoctorRegistrationRequest, User>();
            CreateMap<DiseaseRequest, Diseases>();
            CreateMap<AnimalUpdateRequest, Animal>();
            CreateMap<ClinicUpdateRequest, Clinic>();
            CreateMap<DiseaseIoTRequest, Diseases>();
        }
    }
}
