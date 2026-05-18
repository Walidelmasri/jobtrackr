using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Application.DTOs;

public class UpdateApplicationStatusRequest
{
    public ApplicationStatus Status { get; set; }
}