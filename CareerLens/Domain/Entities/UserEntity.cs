using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Domain.Entities
{
    [Table("GS_USERS")]
    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        [Column("NAME")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        [Column("EMAIL")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters")]
        [Column("PASSWORD")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
