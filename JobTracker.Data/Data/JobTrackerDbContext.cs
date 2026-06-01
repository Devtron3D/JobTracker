using JobTracker.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Data.Data;

public class JobTrackerDbContext : DbContext
{
    public JobTrackerDbContext(DbContextOptions<JobTrackerDbContext> options)
        : base(options)
    {
    }

    // Database tables
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<CvProfile> CvProfiles => Set<CvProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Job table configuration
        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Company).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.JobUrl).HasMaxLength(500);

            // A job can have one CV, a CV can have many jobs
            entity.HasOne(e => e.CvProfile)
                  .WithMany()
                  .HasForeignKey(e => e.CvProfileId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // CV table configuration
        modelBuilder.Entity<CvProfile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
        });
    }
}