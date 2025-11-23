namespace CareerLens.Application.Dtos.AnalysisResults
{
    public class AnalysisResultDto
    {
        public string Status { get; init; } = string.Empty;   // "Match" | "Gap"
        public int? SkillId { get; init; }
        public int? JobAnalysisId { get; init; }
    }
}
