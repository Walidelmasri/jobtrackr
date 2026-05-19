using Jobtrackr.Domain.Enums;

namespace Jobtrackr.Application.DTOs;

public class JobApplicationQueryParameters
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public ApplicationStatus? Status { get; set; }

    public string? CompanyName { get; set; }
}