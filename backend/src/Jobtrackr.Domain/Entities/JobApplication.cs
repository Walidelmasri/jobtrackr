using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Domain.Entities;

public class JobApplication
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public Company Company { get; private set; } = null!;
    public List<ApplicationNote> Notes { get; private set; } = [];

    public List<ApplicationStatusHistory> StatusHistory { get; private set; } = [];
    public List<ApplicationTask> Tasks { get; private set; } = [];

    public string RoleTitle { get; private set; } = string.Empty;

    public string? JobUrl { get; private set; }

    public decimal? SalaryMin { get; private set; }

    public decimal? SalaryMax { get; private set; }

    public WorkMode WorkMode { get; private set; }

    public EmploymentType EmploymentType { get; private set; }

    public ApplicationStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? AppliedAt { get; private set; }

    private JobApplication()
    {
    }

    public JobApplication(
        Guid companyId,
        string roleTitle,
        WorkMode workMode,
        EmploymentType employmentType)
    {
        Id = Guid.NewGuid();

        CompanyId = companyId;

        SetRoleTitle(roleTitle);

        WorkMode = workMode;

        EmploymentType = employmentType;

        Status = ApplicationStatus.Saved;

        CreatedAt = DateTime.UtcNow;
    }

    public void SetRoleTitle(string roleTitle)
    {
        if (string.IsNullOrWhiteSpace(roleTitle))
        {
            throw new ArgumentException("Role title is required.");
        }

        RoleTitle = roleTitle.Trim();
    }

    public void UpdateSalary(decimal? salaryMin, decimal? salaryMax)
    {
        if (salaryMin > salaryMax)
        {
            throw new ArgumentException(
                "Minimum salary cannot exceed maximum salary.");
        }

        SalaryMin = salaryMin;
        SalaryMax = salaryMax;
    }

    public void ChangeStatus(ApplicationStatus status)
    {
        Status = status;

        if (status == ApplicationStatus.Applied)
        {
            AppliedAt ??= DateTime.UtcNow;
        }
    }
}