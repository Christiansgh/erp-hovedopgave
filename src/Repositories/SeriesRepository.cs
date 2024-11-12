using erp.Models;
using System.Data.SqlClient;

namespace erp.Repositories;

public class SeriesRepository {
  private DataAccessLayer _dataAccessLayer;

  public SeriesRepository(DataAccessLayer dataAccessLayer) {
    _dataAccessLayer = dataAccessLayer;
  }

  //TODO: Exception handling
  public async Task<List<ModelRecords>> ReadAllSeriesAsync() {
    var allSeries = new List<ModelRecords>();

    string query = """
      SELECT series.id, series.name, series.brand, series.price, shoes.sku, shoes.size, shoes.stock
      FROM series
      FULL OUTER JOIN shoes ON series.ID = shoes.series_id;
    """;

    using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync(query)) {
      var currentSeries = new ModelRecords(Name: "", Brand: "", Price: -1, Sizes: null, Id: -1); //store a pointer to the series so we can append sizes to its collection.

      while (await result.ReadAsync()) { //Everything is stored in memory ~ 1k rows
        int id = result.GetInt32(result.GetOrdinal("id"));

        if (!allSeries.Any(entry => entry.Id == id)) { // No collection for the series id exists.
          var newSeries = new ModelRecords(
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
  }
}
