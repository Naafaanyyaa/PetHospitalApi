using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHospital.Data.Entities;
using PetHospital.Data.Interfaces;

namespace PetHospital.Data.Repositories
{
    public class DiseasesRepository : Repository<Diseases>, IDiseasesRepository
    {
        private readonly ILogger<DiseasesRepository> _logger;
        public DiseasesRepository(ApplicationDbContext db, ILogger<DiseasesRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<Diseases> AddAsync(Diseases entity)
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
                _logger.LogError("Error while creating disease: {EMessage}", e.Message);
            }

            return null;
        }

        public override Task<Diseases?> GetByIdAsync(string id)
        {
            return _db.Diseases.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task UpdateAsync(Diseases entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Diseases.Update(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                _db.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while updating disease: {EMessage}", e.Message);
            }
        }
    }
}
