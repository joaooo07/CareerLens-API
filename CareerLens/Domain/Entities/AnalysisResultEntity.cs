using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLens.Domain.Entities
{
    [Table("GS_ANALYSES_RESULTS")]
    public class AnalysisResultEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(50)]
        [Column("STATUS")]
        public string Status { get; set; } = string.Empty; // "Match" ou "Gap"

        [Required]
        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional FK para Skill
        [Column("SKILL_ID")]
        public int? SkillId { get; set; }
        public SkillEntity? Skill { get; set; }

        // Optional FK para JobAnalysis
        [Column("JOB_ANALYSIS_ID")]
        public int? JobAnalysisId { get; set; }
        public JobAnalysisEntity? JobAnalysis { get; set; }
    }
}
