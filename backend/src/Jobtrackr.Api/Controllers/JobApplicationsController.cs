using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jobtrackr.Api.Controllers;

[ApiController]
[Route("api/job-applications")]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _service;

    public JobApplicationsController(
        IJobApplicationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<JobApplicationListItemResponse>>>
        GetAll(CancellationToken ct)
    {
        var jobs = await _service.GetAllAsync(ct);

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
}