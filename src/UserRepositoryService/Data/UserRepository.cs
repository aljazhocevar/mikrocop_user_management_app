using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserRepositoryService.Models;

namespace UserRepositoryService.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) { _db = db; }
        public async Task CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var u = await _db.Users.FindAsync(id);
            if (u != null) { _db.Users.Remove(u); await _db.SaveChangesAsync(); }
        }
        public async Task<List<User>> GetAll()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
