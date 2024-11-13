﻿using erp.Models;
using erp.Repositories;
using erp.Services;
using Microsoft.AspNetCore.Mvc;

namespace erp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase {
  private ILoggerService _logger;
  private IAuthenticationService _authenticator;
  private readonly StockRepository _stockRepo;

  public StockController(ILoggerService logger, IAuthenticationService authenticator, StockRepository stockRepo) {
    _logger = logger;
    _authenticator = authenticator;
    _stockRepo = stockRepo;
  }

  [Route("poststockupdates")]
  [HttpPost]
  public async Task<IActionResult> PostStockUpdates([FromBody] List<SizeModel> sizeModels) {
    string? recievedUsername = Request.Headers["username"];
    string? recievedPassword = Request.Headers["password"];

    if (string.IsNullOrWhiteSpace(recievedUsername) || string.IsNullOrWhiteSpace(recievedPassword)) {
      return BadRequest("Missing login credentials");
    }

    if (!_authenticator.Authenticate(recievedUsername, recievedPassword)) {
      return Unauthorized("Invalid login credentials");
    }

    bool successful = await _stockRepo.UpdateStockAmountsAsync(sizeModels);
    if (successful) {
      return Ok($"Successfully updated {sizeModels.Count} stock amount(s)");
    } else {
      return StatusCode(500, "Something went wrong.");
    }
  }
}