namespace CareerLens.Application.Dtos.Skills
{
    public record SkillResponseDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
    }
}
