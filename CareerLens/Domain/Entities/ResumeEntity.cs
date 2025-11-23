using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLens.Domain.Entities
{
    [Table("GS_RESUMES")]
    public class ResumeEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("TITLE")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        [Column("DESCRIPTION")]
        public string Description { get; set; } = string.Empty;

        // FK obrigatória
        [Required]
        [Column("USER_ID")]
        public int UserId { get; set; }

        // Navigation property obrigatória
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; } = default!;
    }
}
