using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetHospital.Business.Interfaces;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Business.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IClinicService _clinicService;
        public StatisticService(IClinicService clinicService, UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _clinicService = clinicService;
        }
        public Task<StatisticResponse> GetClinicStatistic(string clinicId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
