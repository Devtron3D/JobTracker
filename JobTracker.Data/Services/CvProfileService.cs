using JobTracker.Core.Interfaces;
using JobTracker.Core.Models;
using JobTracker.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Data.Services;

public class CvProfileService : ICvProfileService
{
    private readonly JobTrackerDbContext _context;

    public CvProfileService(JobTrackerDbContext context)
    {
        _context = context;
    }

    public async Task<List<CvProfile>> GetAllAsync()
    {
        return await _context.CvProfiles
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<CvProfile?> GetByIdAsync(int id)
    {
        return await _context.CvProfiles.FindAsync(id);
    }

    public async Task<CvProfile> CreateAsync(CvProfile cv)
    {
        _context.CvProfiles.Add(cv);
        await _context.SaveChangesAsync();
        return cv;
    }

    public async Task DeleteAsync(int id)
    {
        var cv = await _context.CvProfiles.FindAsync(id);
        if (cv != null)
        {
            _context.CvProfiles.Remove(cv);
            await _context.SaveChangesAsync();
        }
    }
}