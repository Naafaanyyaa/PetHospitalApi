using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Data.Interfaces
{
    public interface IClinicRepository : IRepository<Clinic>
    {
        Task<Clinic> AddAsync(Clinic entity, User userClinic, bool isUserHost);

        Task<bool> IsUserCreatorOfClinicAsync(string userId, string hospitalId);
    }
}
