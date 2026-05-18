using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Domain.Entities;

public class ApplicationStatusHistory
{
    public Guid Id { get; private set; }

    public Guid JobApplicationId { get; private set; }

    public JobApplication JobApplication { get; private set; } = null!;

    public ApplicationStatus FromStatus { get; private set; }

    public ApplicationStatus ToStatus { get; private set; }

    public DateTime ChangedAt { get; private set; }

    private ApplicationStatusHistory()
    {
    }

    public ApplicationStatusHistory(
        Guid jobApplicationId,
        ApplicationStatus fromStatus,
        ApplicationStatus toStatus)
    {
        Id = Guid.NewGuid();

        JobApplicationId = jobApplicationId;

        FromStatus = fromStatus;

        ToStatus = toStatus;

        ChangedAt = DateTime.UtcNow;
    }
}