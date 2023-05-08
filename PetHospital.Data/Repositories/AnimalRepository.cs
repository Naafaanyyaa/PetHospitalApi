using AutoMapper;
using PetHospital.Data.Entities;
using PetHospital.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace PetHospital.Data.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly ILogger<Animal> _logger;
        private readonly IMapper _mapper;
        public AnimalRepository(ApplicationDbContext db, ILogger<Animal> logger, IMapper mapper) : base(db)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<Animal> AddAsync(Animal entity)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while creating a new animal: {EMessage}", e.Message);
            }

            return null;
        }

        public override Task<Animal?> GetByIdAsync(string id)
        {
            return _db.Animal.AsNoTracking()
                .Include(x => x.Photos)
                .Include(x => x.User)
                .Include(x => x.AnimalClinic)
                .ThenInclude(x => x.Clinic)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task DeleteById(string id)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var animal = _db.Animal.Single(x => x.Id == id);
                _db.Remove(animal);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }

        public override async Task DeleteAsync(Animal animal)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Remove(animal);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }

        public override async Task UpdateAsync(Animal entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Animal.Update(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                _db.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }
        public async Task UpdateAsync(Animal animal, Clinic clinic)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                AnimalClinic animalClinic = new AnimalClinic()
                {
                    ClinicId = clinic.Id,
                    AnimalId = animal.Id,
                    CreatedDate = DateTime.Now
                };
                animal.AnimalClinic = new List<AnimalClinic> { animalClinic };

                await _db.AddAsync(animalClinic);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a new animal: {EMessage}", e.Message);
            }
        }
    }
}
