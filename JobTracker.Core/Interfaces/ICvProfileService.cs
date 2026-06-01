using JobTracker.Core.Models;

namespace JobTracker.Core.Interfaces;

public interface ICvProfileService
{
    Task<List<CvProfile>> GetAllAsync();
    Task<CvProfile?> GetByIdAsync(int id);
    Task<CvProfile> CreateAsync(CvProfile cv);
    Task DeleteAsync(int id);
}