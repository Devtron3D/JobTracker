using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JobTracker.Data.Data;

public class JobTrackerDbContextFactory : IDesignTimeDbContextFactory<JobTrackerDbContext>
{
    public JobTrackerDbContext CreateDbContext(string[] args)
    {
        // Walk up from JobTracker.Data to find JobTracker.Web's config
        var basePath = Path.Combine(Directory.GetCurrentDirectory(),
            "../JobTracker.Web");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<JobTrackerDbContext>();
        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection"));

        return new JobTrackerDbContext(optionsBuilder.Options);
    }
}