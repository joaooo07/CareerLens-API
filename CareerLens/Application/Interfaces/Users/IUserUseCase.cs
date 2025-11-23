using CareerLens.Application.Dtos.Users;
using CareerLens.Application.Models;

namespace CareerLens.Application.Interfaces.Users
{
    public interface IUserUseCase
    {
        Task<OperationResult<UserResponseDto?>> AddUserAsync(UserDto dto);
        Task<OperationResult<UserResponseDto?>> UpdateUserAsync(int id, UserDto dto);
        Task<OperationResult<bool>> DeleteUserAsync(int id);
        Task<OperationResult<PageResultModel<IEnumerable<UserResponseDto>>>> GetAllUsersAsync(int page = 1, int pageSize = 10);
        Task<OperationResult<UserResponseDto?>> GetUserByIdAsync(int id);

        // para login/autenticação — mantém retorno da entidade
        Task<CareerLens.Domain.Entities.UserEntity?> GetUserByEmailAsync(string email);
    }
}
