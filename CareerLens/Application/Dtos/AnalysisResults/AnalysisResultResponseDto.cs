namespace CareerLens.Application.Dtos.AnalysisResults
{
    public record AnalysisResultResponseDto(
    int Id,
    string Status,
    DateTime CreatedAt,
    int? SkillId,
    int? JobAnalysisId
    );

}
