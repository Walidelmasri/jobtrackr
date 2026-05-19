using Jobtrackr.Application.DTOs;

namespace Jobtrackr.Application.Services;

public interface IJobApplicationService
{
    Task<PagedResponse<JobApplicationListItemResponse>> GetAllAsync(
    JobApplicationQueryParameters query,
    CancellationToken ct = default);

    Task<JobApplicationResponse?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default);

    Task<JobApplicationResponse> CreateAsync(
        CreateJobApplicationRequest request,
        CancellationToken ct = default);

    Task<JobApplicationResponse?> UpdateAsync(
        Guid id,
        UpdateJobApplicationRequest request,
        CancellationToken ct = default);

    Task<JobApplicationResponse?> UpdateStatusAsync(
        Guid id,
        UpdateApplicationStatusRequest request,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken ct = default);
}