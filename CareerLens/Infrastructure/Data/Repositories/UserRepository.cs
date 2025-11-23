using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;
using CareerLens.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> AddAsync(UserEntity entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Users.FindAsync(id);

            if (result is null)
                return false;

            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserEntity?> UpdateAsync(int id, UserEntity entity)
        {
            var result = await _context
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                return null;

            result.Name = entity.Name;
            result.Email = entity.Email;
            result.Password = entity.Password;
            result.CreatedAt = entity.CreatedAt;

            _context.Users.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<PageResultModel<IEnumerable<UserEntity>>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var totalItems = await _context.Users.CountAsync();

            var data = await _context
                .Users
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultModel<IEnumerable<UserEntity>>(
                data,
                totalItems,
                page,
                pageSize
            );
        }

        public async Task<UserEntity?> GetByIdAsync(int id)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
        {
            return await _context
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
