using JobTracker.Core.Models;

namespace JobTracker.Core.Interfaces;

public interface IJobApplicationService
{
    Task<List<JobApplication>> GetAllAsync(string? search, string? category, string? status, int? cvProfileId);
    Task<JobApplication?> GetByIdAsync(int id);
    Task<JobApplication> CreateAsync(JobApplication job);
    Task<JobApplication> UpdateAsync(JobApplication job);
    Task DeleteAsync(int id);
    Task<List<string>> GetCategoriesAsync();
}