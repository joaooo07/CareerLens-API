namespace CareerLens.Application.Dtos.Users
{
    public record UserResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
