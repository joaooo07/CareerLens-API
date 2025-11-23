namespace CareerLens.Application.Dtos.Skills
{
    public record SkillDto
    {
        public string Name { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
    }
}
