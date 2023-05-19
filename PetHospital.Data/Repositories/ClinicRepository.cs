using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHospital.Data.Entities;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Interfaces;

namespace PetHospital.Data.Repositories
{
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        private readonly ILogger<ClinicRepository> _logger;
        public ClinicRepository(ApplicationDbContext db, ILogger<ClinicRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<Clinic> AddAsync(Clinic entity)
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
                _logger.LogError("Error while creating a new clinic: {EMessage}", e.Message);
                throw new Exception($"Error while creating a new clinic: {e.Message}");
            }
        }

        public async Task<Clinic> AddAsync(Clinic entity, User user)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var userClinic = new UserClinic();
                userClinic.ClinicId = entity.Id;
                userClinic.UserId = user.Id;
                userClinic.CreatedDate = DateTime.Now;
                userClinic.User = user;
                userClinic.IsCreator = true;
                
                entity.UserClinic = new List<UserClinic> { userClinic };
           
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while creating a new clinic: {EMessage}", e.Message);
                throw new Exception($"Error while creating a new clinic: {e.Message}");
            }
        }


        public override Task<Clinic?> GetByIdAsync(string id)
        {
            return _db.Clinic.AsNoTracking()
                .Include(x => x.UserClinic)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.UserRoles.Where(o => o.Role.Name.Equals("Doctor")))
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task DeleteById(string id)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var clinic = _db.Clinic.Single(x => x.Id == id);
                _db.Remove(clinic);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            } 
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a clinic: {EMessage}", e.Message);
            }
        }

        public override async Task DeleteAsync(Clinic clinic)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Remove(clinic);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a clinic: {EMessage}", e.Message);
            }
        }
        public async Task<bool> IsUserCreatorOfClinicAsync(string userId, string hospitalId)
        {
            return await _db.Clinic.AsNoTracking()
                .AnyAsync(x => x.Id == hospitalId && x.UserClinic.Any(uc => uc.UserId == userId && uc.IsCreator));
        }
        public override async Task UpdateAsync(Clinic entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Clinic.Update(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                _db.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting a clinic: {EMessage}", e.Message);
            }
        }
    }
}
