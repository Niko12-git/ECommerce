using ECommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _service;

    public DashboardController(DashboardService service)
    {
        _service = service;
    }

    // CORRECCIÓN: Cambiamos "{id}" por "stats"
    [HttpGet("stats")]
    public async Task<IActionResult> Get()
    {
        var dashboard = await _service.GetDashboardAsync();
        return Ok(dashboard);
    }
}