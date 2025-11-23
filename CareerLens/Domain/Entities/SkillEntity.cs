using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Domain.Entities
{
    [Table("GS_SKILLS")]
    [Index(nameof(Name), IsUnique = false)]
    public class SkillEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        [Column("CATEGORY")]
        public string Category { get; set; } = string.Empty;
    }
}
