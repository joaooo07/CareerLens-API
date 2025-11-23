namespace CareerLens.Application.Dtos.JobAnalyses
{
    public record JobAnalysisResponseDto
    {
        public int Id { get; init; }
        public string JobTitle { get; init; } = string.Empty;
        public string JobDescription { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public int? UserId { get; init; }
        public int? ResumeId { get; init; }
    }
}
