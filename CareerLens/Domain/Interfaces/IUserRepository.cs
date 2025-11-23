using CareerLens.Domain.Entities;
using CareerLens.Application.Models;

namespace CareerLens.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<PageResultModel<IEnumerable<UserEntity>>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<UserEntity?> GetByIdAsync(int id);
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity?> AddAsync(UserEntity entity);
        Task<UserEntity?> UpdateAsync(int id, UserEntity entity);
        Task<bool> DeleteAsync(int id);
    }
}
