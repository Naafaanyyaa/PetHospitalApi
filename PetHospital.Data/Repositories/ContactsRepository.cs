using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHospital.Data.Entities;
using PetHospital.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHospital.Data.Repositories
{
    public class ContactsRepository : Repository<Contacts>, IContactsRepository
    {
        private readonly ILogger<ContactsRepository> _logger;

        public ContactsRepository(ApplicationDbContext db, ILogger<ContactsRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public override async Task<Contacts> AddAsync(Contacts entity)
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
                _logger.LogError("Error while creating contacts: {EMessage}", e.Message);
            }

            return null;
        }

        public override Task<Contacts?> GetByIdAsync(string id)
        {
            return _db.Contacts.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task UpdateAsync(Contacts entity)
        {

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                _db.Contacts.Update(entity);
                _db.Entry(entity).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                _db.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while deleting contacts: {EMessage}", e.Message);
            }
        }
    }
}
