using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLens.Domain.Entities
{
    [Table("GS_LEARNING_RESOURCES")]
    public class LearningResourceEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        [Column("TITLE")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "URL is required")]
        [StringLength(500)]
        [Column("URL")]
        public string Url { get; set; } = string.Empty;

        [Required(ErrorMessage = "Resource type is required")]
        [StringLength(50)]
        [Column("RESOURCE_TYPE")]
        public string ResourceType { get; set; } = string.Empty;

        // Optional FK para Skill
        [Column("SKILL_ID")]
        public int? SkillId { get; set; }

        public SkillEntity? Skill { get; set; }
    }
}
