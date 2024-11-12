using erp.Models;
using System.Data.SqlClient;
using System.Text;

namespace erp.Repositories;

public class StockRepository {
  private readonly DataAccessLayer _dataAccessLayer;

  public StockRepository(DataAccessLayer dataAccessLayer) {
    _dataAccessLayer = dataAccessLayer;
  }

  public async Task<bool> UpdateStockAmountsAsync(List<SizeModel> sizeModels) {
    try {
      string query = BuildUpdateQuery(sizeModels);

      using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync(query)) {
        return result.RecordsAffected == sizeModels.Count;
      }
    } catch {
      return false;
    }
  }


  private string BuildUpdateQuery(List<SizeModel> sizeModels) {
    var queryBuilder = new StringBuilder();
    queryBuilder.AppendLine("UPDATE shoes");
    queryBuilder.AppendLine("SET stock = CASE sku");

    foreach (SizeModel entry in sizeModels) {
      queryBuilder.AppendLine($"    WHEN '{entry.SKU}' THEN {entry.Stock}");
    }

    queryBuilder.AppendLine("END");

    var allSKUs = string.Join(", ", sizeModels.Select(entry => $"'{entry.SKU}'"));
    queryBuilder.AppendLine($"WHERE sku IN ({allSKUs});");

    return queryBuilder.ToString();
  }
}
