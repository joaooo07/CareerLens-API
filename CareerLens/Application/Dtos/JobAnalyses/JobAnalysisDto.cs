namespace CareerLens.Application.Dtos.JobAnalyses
{
    public record JobAnalysisDto
    {
        public string JobTitle { get; init; } = string.Empty;
        public string JobDescription { get; init; } = string.Empty;
        public int? UserId { get; init; }
        public int? ResumeId { get; init; }
    }
}
