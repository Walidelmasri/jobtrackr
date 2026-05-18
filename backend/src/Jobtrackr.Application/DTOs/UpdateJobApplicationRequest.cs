using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Application.DTOs;

public class UpdateJobApplicationRequest
{
    public string RoleTitle { get; set; } = string.Empty;

    public string? JobUrl { get; set; }

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    public WorkMode WorkMode { get; set; }

    public EmploymentType EmploymentType { get; set; }
}