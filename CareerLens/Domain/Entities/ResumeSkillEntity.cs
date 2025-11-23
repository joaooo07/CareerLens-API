using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLens.Domain.Entities
{
    [Table("GS_RESUMES_SKILLS")]
    public class ResumeSkillEntity
    {
        [Column("RESUME_ID")]
        public int? ResumeId { get; set; }

        [Column("SKILL_ID")]
        public int? SkillId { get; set; }

        // Navigation properties
        public ResumeEntity? Resume { get; set; }
        public SkillEntity? Skill { get; set; }
    }
}
