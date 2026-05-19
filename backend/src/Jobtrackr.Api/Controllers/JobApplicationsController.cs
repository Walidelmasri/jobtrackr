using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Jobtrackr.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/job-applications")]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _service;

    public JobApplicationsController(
        IJobApplicationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<JobApplicationListItemResponse>>>
    GetAll(
        [FromQuery] JobApplicationQueryParameters query,
        CancellationToken ct)
    {
        var jobs = await _service.GetAllAsync(query, ct);

        return Ok(jobs);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JobApplicationResponse>>
        GetById(Guid id, CancellationToken ct)
    {
        var job = await _service.GetByIdAsync(id, ct);

        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpPost]
    public async Task<ActionResult<JobApplicationResponse>>
        Create(
            CreateJobApplicationRequest request,
            CancellationToken ct)
    {
        var job = await _service.CreateAsync(request, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = job.Id },
            job);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<JobApplicationResponse>>
        Update(
            Guid id,
            UpdateJobApplicationRequest request,
            CancellationToken ct)
    {
        var job = await _service.UpdateAsync(id, request, ct);

        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<JobApplicationResponse>>
        UpdateStatus(
            Guid id,
            UpdateApplicationStatusRequest request,
            CancellationToken ct)
    {
        var job = await _service.UpdateStatusAsync(id, request, ct);

        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }
    [HttpGet("{id:guid}/history")]
    public async Task<ActionResult<IReadOnlyList<ApplicationStatusHistoryResponse>>>
        GetStatusHistory(Guid id, CancellationToken ct)
    {
        var history = await _service.GetStatusHistoryAsync(id, ct);

        return Ok(history);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult>
        Delete(Guid id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(id, ct);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    [HttpGet("{id:guid}/notes")]
    public async Task<ActionResult<IReadOnlyList<ApplicationNoteResponse>>>
    GetNotes(Guid id, CancellationToken ct)
    {
        var notes = await _service.GetNotesAsync(id, ct);

        return Ok(notes);
    }

    [HttpPost("{id:guid}/notes")]
    public async Task<ActionResult<ApplicationNoteResponse>>
        AddNote(
            Guid id,
            CreateApplicationNoteRequest request,
            CancellationToken ct)
    {
        var note = await _service.AddNoteAsync(id, request, ct);

        if (note is null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    [HttpPut("/api/notes/{noteId:guid}")]
    public async Task<ActionResult<ApplicationNoteResponse>>
        UpdateNote(
            Guid noteId,
            UpdateApplicationNoteRequest request,
            CancellationToken ct)
    {
        var note = await _service.UpdateNoteAsync(noteId, request, ct);

        if (note is null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    [HttpDelete("/api/notes/{noteId:guid}")]
    public async Task<IActionResult>
        DeleteNote(Guid noteId, CancellationToken ct)
    {
        var deleted = await _service.DeleteNoteAsync(noteId, ct);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}