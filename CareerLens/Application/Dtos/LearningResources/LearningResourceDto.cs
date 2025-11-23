namespace CareerLens.Application.Dtos.LearningResources
{
    public record LearningResourceDto
    {
        public string Title { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
        public string ResourceType { get; init; } = string.Empty;
        public int? SkillId { get; init; }
    }
}
