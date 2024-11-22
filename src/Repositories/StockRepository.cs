using System.Data.SqlClient;
using System.Text;
using erp.Models;
using erp.Services;

namespace erp.Repositories;

public class StockRepository {
    private readonly DataAccessLayer _dataAccessLayer;
    private readonly ILoggerService _logger;

    public StockRepository(DataAccessLayer dataAccessLayer, ILoggerService logger) {
        _dataAccessLayer = dataAccessLayer;
        _logger = logger;
    }

    public async Task<bool> UpdateStockAmountsAsync(List<SizeModel> sizeModels) {
        int affectedAmount = -1;
        int passedAmount = -1;

        try {
            string query = BuildUpdateQuery(sizeModels);

            using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync(query)) {
                affectedAmount = result.RecordsAffected;
                passedAmount = sizeModels.Count;
                return affectedAmount == passedAmount;
            }
        } catch {
            _logger.LogError($"StockRepository.UpdateStockAmountsAsync() - Passed amount: {passedAmount}, Affected amount: {affectedAmount}");
            return false;
        }
    }

    private static string BuildUpdateQuery(List<SizeModel> sizeModels) {
        var queryBuilder = new StringBuilder();
        queryBuilder.AppendLine("UPDATE shoes");
        queryBuilder.AppendLine("SET stock = CASE sku");

        foreach (SizeModel entry in sizeModels) {
            queryBuilder.AppendLine($"WHEN '{entry.SKU}' THEN {entry.Stock}");
        }

        queryBuilder.AppendLine("END");

        string allSKUs = string.Join(", ", sizeModels.Select(entry => $"'{entry.SKU}'"));
        queryBuilder.AppendLine($"WHERE sku IN ({allSKUs});");

        return queryBuilder.ToString();
    }
}
