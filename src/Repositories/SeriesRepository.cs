using System.Data.SqlClient;
using erp.Models;
using erp.Services;

namespace erp.Repositories;

public class SeriesRepository {
    private readonly DataAccessLayer _dataAccessLayer;
    private readonly ILoggerService _logger;

    public SeriesRepository(DataAccessLayer dataAccessLayer, ILoggerService logger) {
        _dataAccessLayer = dataAccessLayer;
        _logger = logger;
    }

    public async Task<List<SeriesModel>> ReadAllSeriesAsync() {
        var allSeries = new List<SeriesModel>();

        string query = """
SELECT series.id, series.name, series.brand, series.price, shoes.sku, shoes.size, shoes.stock
FROM series
FULL OUTER JOIN shoes ON series.ID = shoes.series_id;
""";

        try {
            using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync(query)) {
                var currentSeries = new SeriesModel(Name: "", Brand: "", Price: -1, Sizes: new List<SizeModel>(), Id: -1); //store a pointer to the series so we can append sizes to its collection.

                while (await result.ReadAsync()) { //Everything is stored in memory ~ 1k rows
                    int id = result.GetInt32(result.GetOrdinal("id"));

                    if (!allSeries.Any(entry => entry.Id == id)) { // No collection for the series id exists.
                        var newSeries = new SeriesModel(
                          Name: result.GetString(result.GetOrdinal("name")),
                          Brand: result.GetString(result.GetOrdinal("brand")),
                          Price: result.GetDecimal(result.GetOrdinal("price")),
                          Sizes: new List<SizeModel>(),
                          Id: id
                        );

                        allSeries.Add(newSeries);
                        currentSeries = newSeries;
                    }

                    var size = new SizeModel( //map current row's size to a seriesmodel.
                      Size: result.GetInt32(result.GetOrdinal("size")),
                      SKU: result.GetString(result.GetOrdinal("sku")),
                      Stock: result.GetInt32(result.GetOrdinal("stock"))
                    );

                    currentSeries.Sizes.Add(size);
                }
            }

            return allSeries;
        } catch (Exception ex) {
            _logger.LogError($"SeriesRepository.ReadAllSeriesAsync() - {ex.GetType()}");
            return [];
        }
    }
}
