using System.Net;
using CareerLens.Application.Dtos.Users;
using CareerLens.Application.Interfaces.Users;
using CareerLens.Application.Mapper;
using CareerLens.Application.Models;
using CareerLens.Domain.Entities;
using CareerLens.Domain.Interfaces;

namespace CareerLens.Application.UseCases.Users
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _repo;

        public UserUseCase(IUserRepository repo)
        {
            _repo = repo;
        }

        // CREATE
        public async Task<OperationResult<UserResponseDto?>> AddUserAsync(UserDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var created = await _repo.AddAsync(entity);

                return OperationResult<UserResponseDto?>.Success(created.ToResponse());
            }
            catch
            {
                return OperationResult<UserResponseDto?>.Failure(
                    "Unable to save user",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        // UPDATE
        public async Task<OperationResult<UserResponseDto?>> UpdateUserAsync(int id, UserDto dto)
        {
            try
            {
                var entity = dto.ToEntity();
                var updated = await _repo.UpdateAsync(id, entity);

                if (updated is null)
                    return OperationResult<UserResponseDto?>.Failure(
                        "User not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<UserResponseDto?>.Success(updated.ToResponse());
            }
            catch
            {
                return OperationResult<UserResponseDto?>.Failure(
                    "Unable to update user",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        // DELETE
        public async Task<OperationResult<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var deleted = await _repo.DeleteAsync(id);

                if (!deleted)
                    return OperationResult<bool>.Failure(
                        "User not found",
                        (int)HttpStatusCode.NotFound
                    );

                return OperationResult<bool>.Success(true);
            }
            catch
            {
                return OperationResult<bool>.Failure(
                    "Unable to delete user",
                    (int)HttpStatusCode.BadRequest
                );
            }
        }

        // PAGINATED LIST
        public async Task<OperationResult<PageResultModel<IEnumerable<UserResponseDto>>>> GetAllUsersAsync(int page = 1, int pageSize = 10)
        {
            var result = await _repo.GetAllAsync(page, pageSize);

            var mapped = new PageResultModel<IEnumerable<UserResponseDto>>(
                result.Data.Select(u => u.ToResponse()),
                result.Page,
                result.PageSize,
                result.TotalItems
            );

            return OperationResult<PageResultModel<IEnumerable<UserResponseDto>>>.Success(mapped);
        }

        // GET BY ID
        public async Task<OperationResult<UserResponseDto?>> GetUserByIdAsync(int id)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result is null)
                return OperationResult<UserResponseDto?>.Failure(
                    "User not found",
                    (int)HttpStatusCode.NotFound
                );

            return OperationResult<UserResponseDto?>.Success(result.ToResponse());
        }

        // INTERNAL USE (LOGIN, EMAIL CHECK, ETC.)
        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            return await _repo.GetByEmailAsync(email);
        }
    }
}
