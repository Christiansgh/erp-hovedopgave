using erp.Models;
using erp.Repositories;
using erp.Services;
using Microsoft.AspNetCore.Mvc;

namespace erp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoeController : ControllerBase {
  private ILoggerService _logger;
  private IAuthenticationService _authenticator;
  private ShoeRepository _shoeRepo;

  public ShoeController(ILoggerService logger, IAuthenticationService authenticator, ShoeRepository shoeRepo) {
    _logger = logger;
    _authenticator = authenticator;
    _shoeRepo = shoeRepo;
  }

  [Route("getallshoes")]
  [HttpGet]
  //TODO: Apikey validation
  public async Task<IActionResult> GetAllShoes() {
    string? recievedUsername = Request.Headers["username"];
    string? recievedPassword = Request.Headers["password"];

    if (string.IsNullOrWhiteSpace(recievedUsername) || string.IsNullOrWhiteSpace(recievedPassword)) {
      return BadRequest("Missing login credentials");
    }

    if (!_authenticator.Authenticate(recievedUsername, recievedPassword)) {
      return Unauthorized("Invalid login credentials");
    }

    try {
      List<Shoe> allShoes = await _shoeRepo.ReadAllAsync();
      return new JsonResult(allShoes);
    } catch (Exception ex) {
      _logger.LogError(ex.ToString());
      return StatusCode(500, "exception catched");
    }
  }
}
