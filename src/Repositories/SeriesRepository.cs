using erp.Models;
using System.Data.SqlClient;

namespace erp.Repositories;

public class SeriesRepository : IRepository<SeriesModel, string, bool> {
  private DataAccessLayer _dataAccessLayer;

  public SeriesRepository(DataAccessLayer dataAccessLayer) {
    _dataAccessLayer = dataAccessLayer;
  }

  //TODO: Exception handling
  public async Task<bool> CreateAsync(SeriesModel model) {
    //string query = "INSERT INTO shoes (Brand, Color, Size, Stock) VALUES (@Brand, @Color, @Size, @Stock)";
    //var parameters = new Dictionary<string, string>() {
    //  { "@Brand", model.Brand },
    //  { "@Color", model.Color },
    //  { "@Size", model.Size.ToString()},
    //  {"@Stock", model.StockAmount.ToString() }
    //};

    //using (SqlDataReader result = await _dataAccessLayer.ExecuteParameterizedQueryAsync(query, parameters)) {

    //}
    throw new NotImplementedException();
  }

  //TODO: Exception handling
  public Task<bool> DeleteAsync(SeriesModel model) {
    throw new NotImplementedException();
  }

  //TODO: Exception handling
  public async Task<List<SeriesModel>> ReadAllAsync() {
    var allSeries = new List<SeriesModel>();

    string query = """
      SELECT series.id, series.name, series.brand, series.price, shoes.sku, shoes.size, shoes.stock
      FROM series
      FULL OUTER JOIN shoes ON series.ID = shoes.series_id;
    """;

    using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync(query)) {
      var currentSeries = new SeriesModel(Name: "", Brand: "", Price: -1, Sizes: null, Id: -1); //store a pointer to the series so we can append sizes to its collection.

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
  }

  public Task<SeriesModel> ReadByIdAsync(string id) {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(SeriesModel model) {
    throw new NotImplementedException();
  }
}
