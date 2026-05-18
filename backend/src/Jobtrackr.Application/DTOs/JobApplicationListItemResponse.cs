using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Application.DTOs;

public class JobApplicationListItemResponse
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string RoleTitle { get; set; } = string.Empty;

    public ApplicationStatus Status { get; set; }

    public WorkMode WorkMode { get; set; }

    public EmploymentType EmploymentType { get; set; }

    public bool SponsorsVisa { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? AppliedAt { get; set; }
}