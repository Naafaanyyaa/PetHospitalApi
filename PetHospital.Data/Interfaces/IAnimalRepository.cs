using PetHospital.Data.Entities;

namespace PetHospital.Data.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Task UpdateAsync(Animal animal, Clinic clinic);
    }
}
