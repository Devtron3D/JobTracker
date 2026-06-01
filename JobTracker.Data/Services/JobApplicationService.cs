using JobTracker.Core.Interfaces;
using JobTracker.Core.Models;
using JobTracker.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Data.Services;

public class JobApplicationService : IJobApplicationService
{
    private readonly JobTrackerDbContext _context;

    public JobApplicationService(JobTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobApplication>> GetAllAsync(
        string? search, string? category, string? status, int? cvProfileId)
    {
        var query = _context.JobApplications
            .Include(j => j.CvProfile)
            .AsQueryable();

        // Search by title or company
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(j =>
                j.Title.ToLower().Contains(search.ToLower()) ||
                j.Company.ToLower().Contains(search.ToLower()));

        // Filter by category
        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(j => j.Category == category);

        // Filter by status
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(j => j.Status == status);

        // Filter by CV used
        if (cvProfileId.HasValue)
            query = query.Where(j => j.CvProfileId == cvProfileId);

        return await query
            .OrderByDescending(j => j.DateApplied)
            .ToListAsync();
    }

    public async Task<JobApplication?> GetByIdAsync(int id)
    {
        return await _context.JobApplications
            .Include(j => j.CvProfile)
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<JobApplication> CreateAsync(JobApplication job)
    {
        _context.JobApplications.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<JobApplication> UpdateAsync(JobApplication job)
    {
        _context.JobApplications.Update(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task DeleteAsync(int id)
    {
        var job = await _context.JobApplications.FindAsync(id);
        if (job != null)
        {
            _context.JobApplications.Remove(job);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        return await _context.JobApplications
            .Select(j => j.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }
}