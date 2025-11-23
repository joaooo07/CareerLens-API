using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLens.Domain.Entities
{
    [Table("GS_JOB_ANALYSES")]
    public class JobAnalysisEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(200)]
        [Column("JOB_TITLE")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job description is required")]
        [StringLength(3000)]
        [Column("JOB_DESCRIPTION")]
        public string JobDescription { get; set; } = string.Empty;

        [Required]
        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional FK para User
        [Column("USER_ID")]
        public int? UserId { get; set; }
        public UserEntity? User { get; set; }

        // Optional FK para Resume
        [Column("RESUME_ID")]
        public int? ResumeId { get; set; }
        public ResumeEntity? Resume { get; set; }
    }
}
