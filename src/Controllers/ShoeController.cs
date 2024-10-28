using erp.Services;
using Microsoft.AspNetCore.Mvc;

namespace erp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoeController : ControllerBase {
  private ILoggerService _logger;
  private IAuthenticationService _authenticator;

  public ShoeController(ILoggerService logger, IAuthenticationService authenticator) {
    _logger = logger;
    _authenticator = authenticator;
  }

  [Route("getallshoes")]
  [HttpGet]
  //TODO: Apikey validation
  public IActionResult GetAllShoes() {
    string? recievedUsername = Request.Headers["username"];
    string? recievedPassword = Request.Headers["password"];

    if (string.IsNullOrWhiteSpace(recievedUsername) || string.IsNullOrWhiteSpace(recievedPassword)) {
      return BadRequest("Missing login credentials");
    }

    if (!_authenticator.Authenticate(recievedUsername, recievedPassword)) {
      return Unauthorized("Invalid login credentials");
    }

    try {
      //Simmulate data here.

      return Ok("Works");
    } catch (Exception ex) {
      _logger.LogError(ex.ToString());
      return StatusCode(500, "test");
    }
  }
}
