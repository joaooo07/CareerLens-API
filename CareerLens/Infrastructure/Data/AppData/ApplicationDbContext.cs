using CareerLens.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerLens.Infrastructure.Data.AppData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ResumeEntity> Resumes { get; set; }
        public DbSet<SkillEntity> Skills { get; set; }
        public DbSet<LearningResourceEntity> LearningResources { get; set; }
        public DbSet<ResumeSkillEntity> ResumeSkills { get; set; }
        public DbSet<JobAnalysisEntity> JobAnalyses { get; set; }
        public DbSet<AnalysisResultEntity> AnalysisResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite Key for many-to-many table
            modelBuilder.Entity<ResumeSkillEntity>()
                .HasKey(rs => new { rs.ResumeId, rs.SkillId });
        }
    }
}
