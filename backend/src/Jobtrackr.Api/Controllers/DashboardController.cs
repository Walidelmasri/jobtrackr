using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace Jobtrackr.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IJobApplicationService _service;

    public DashboardController(
        IJobApplicationService service)
    {
        _service = service;
    }

    [HttpGet("stats")]
    public async Task<ActionResult<DashboardStatsResponse>>
        GetStats(CancellationToken ct)
    {
        var stats = await _service.GetDashboardStatsAsync(ct);

        return Ok(stats);
    }
}