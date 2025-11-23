using CareerLens.Application.Dtos.Users;
using CareerLens.Domain.Entities;

namespace CareerLens.Application.Mapper
{
    public static class UserMapper
    {

        public static UserEntity ToEntity(this UserDto dto)
        {
            return new UserEntity
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                CreatedAt = DateTime.UtcNow 
            };
        }


        public static UserResponseDto ToResponse(this UserEntity entity)
        {
            return new UserResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
