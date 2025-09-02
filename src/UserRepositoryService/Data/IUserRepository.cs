using System;
using System.Threading.Tasks;
using UserRepositoryService.Models;

namespace UserRepositoryService.Data
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task <List<User>> GetAll();
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
