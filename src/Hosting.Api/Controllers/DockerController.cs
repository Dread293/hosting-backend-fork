using Hosting.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hosting.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DockerController : ControllerBase
{
    private readonly DockerService _dockerService;

    public DockerController(DockerService dockerService)
    {
        _dockerService = dockerService;
    }

    [HttpGet("contenedores")]
    public async Task<IActionResult> ObtenerContenedores()
    {
        var contenedores = await _dockerService.ObtenerContenedoresAsync();

        return Ok(contenedores);
    }
}