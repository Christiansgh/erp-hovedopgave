using erp.Models;
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
  private readonly SeriesRepository _seriesRepo;

  public StockController(ILoggerService logger, IAuthenticationService authenticator, StockRepository stockRepo) {
    _logger = logger;
    _authenticator = authenticator;
    _stockRepo = stockRepo;
  }

  [Route("poststockupdates")]
  [HttpPost]
  public async Task<IActionResult> PostStockUpdates([FromBody] List<SizeModel> sizeModels) {
    //Then we need to use the StockRepository to build a query in the database.
    //If we successfully execute the query, then we return 200
    //We also need to handle the cases where we fail the query.
    //How do we know that the query failed?
    bool successful = await _stockRepo.UpdateStockAmountsAsync(sizeModels);
    if (successful) {
      return Ok("Wow it worked!!");
    } else {
      return StatusCode(500, "Request failed :(");
    }
  }
}
