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

    Task<IReadOnlyList<ApplicationStatusHistoryResponse>> GetStatusHistoryAsync(
        Guid jobApplicationId,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken ct = default);

    Task<IReadOnlyList<ApplicationNoteResponse>> GetNotesAsync(
        Guid jobApplicationId,
        CancellationToken ct = default);

    Task<ApplicationNoteResponse?> AddNoteAsync(
        Guid jobApplicationId,
        CreateApplicationNoteRequest request,
        CancellationToken ct = default);

    Task<ApplicationNoteResponse?> UpdateNoteAsync(
        Guid noteId,
        UpdateApplicationNoteRequest request,
        CancellationToken ct = default);

    Task<bool> DeleteNoteAsync(
        Guid noteId,
        CancellationToken ct = default);

    Task<DashboardStatsResponse> GetDashboardStatsAsync(
        CancellationToken ct = default);
    Task<IReadOnlyList<ApplicationTaskResponse>>
    GetTasksAsync(
        Guid jobApplicationId,
        CancellationToken ct = default);

    Task<ApplicationTaskResponse?>
        AddTaskAsync(
            Guid jobApplicationId,
            CreateApplicationTaskRequest request,
            CancellationToken ct = default);

    Task<bool>
        CompleteTaskAsync(
            Guid taskId,
            CancellationToken ct = default);
}