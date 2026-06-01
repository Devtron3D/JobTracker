namespace JobTracker.Core.Models;

public class JobApplication
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string CvUsed { get; set; } = string.Empty;
    public string Status { get; set; } = "Applied";
    public string? Notes { get; set; }
    public string? JobUrl { get; set; }
    public DateTime DateApplied { get; set; } = DateTime.UtcNow;

    // Link to CvProfile
    public int? CvProfileId { get; set; }
    public CvProfile? CvProfile { get; set; }
}