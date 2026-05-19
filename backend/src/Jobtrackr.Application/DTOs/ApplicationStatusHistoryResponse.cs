using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Application.DTOs;

public class ApplicationStatusHistoryResponse
{
    public Guid Id { get; set; }

    public ApplicationStatus FromStatus { get; set; }

    public ApplicationStatus ToStatus { get; set; }

    public DateTime ChangedAt { get; set; }
}