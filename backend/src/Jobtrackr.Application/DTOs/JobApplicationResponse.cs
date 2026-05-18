using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Application.DTOs;

public class JobApplicationResponse
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string? CompanyWebsite { get; set; }

    public bool SponsorsVisa { get; set; }

    public string RoleTitle { get; set; } = string.Empty;

    public string? JobUrl { get; set; }

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    public WorkMode WorkMode { get; set; }

    public EmploymentType EmploymentType { get; set; }

    public ApplicationStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? AppliedAt { get; set; }
}