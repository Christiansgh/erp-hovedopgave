using erp.Models;
using erp.Repositories;
using erp.Services;
using Microsoft.AspNetCore.Mvc;

namespace erp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeriesController : ControllerBase {
    private readonly ILoggerService _logger;
    private readonly IAuthenticationService<KeyValuePair<string, string>> _authenticator;
    private readonly SeriesRepository _seriesRepo;

    public SeriesController(ILoggerService logger, IAuthenticationService<KeyValuePair<string, string>> authenticator, SeriesRepository seriesRepo) {
        _logger = logger;
        _authenticator = authenticator;
        _seriesRepo = seriesRepo;
    }

    [Route("getallseries")]
    [HttpGet]
    public async Task<IActionResult> GetAllSeries() {
        string? recievedUsername = Request.Headers["username"];
        string? recievedPassword = Request.Headers["password"];

        if (string.IsNullOrWhiteSpace(recievedUsername) || string.IsNullOrWhiteSpace(recievedPassword)) {
            return BadRequest("Missing login credentials");
        }

        if (!_authenticator.Authenticate(new KeyValuePair<string, string>(recievedUsername, recievedPassword))) {
            return Unauthorized("Invalid login credentials");
        }

        try {
            List<SeriesModel> allSeries = await _seriesRepo.ReadAllSeriesAsync();
            return new JsonResult(allSeries);
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return StatusCode(500, $"exception catched: {ex.Message}");
        }
    }
}
