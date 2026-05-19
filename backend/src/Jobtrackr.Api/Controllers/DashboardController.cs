using Jobtrackr.Application.DTOs;
using Jobtrackr.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jobtrackr.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
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