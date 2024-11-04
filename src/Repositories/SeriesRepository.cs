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

    using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync("SELECT * FROM series")) {
      while (await result.ReadAsync()) {
        var series = new SeriesModel(
          Name: result.GetString(result.GetOrdinal("Name")),
          Brand: result.GetString(result.GetOrdinal("Brand")),
          Price: result.GetInt32(result.GetOrdinal("Price")),
          //TODO: Hardcoded for now - Figure out how to read all sizes, their SKU, stock etc.
          Sizes: new List<SizeModel>() {
            { new SizeModel(SKU: "Placeholder SKU", Size: 32, Stock: 26) }
          }
        );

        allSeries.Add(series);
      }
    }

    return allSeries;
  }

  //TODO: Exception handling
  public Task<SeriesModel> ReadByIdAsync(string id) {
    throw new NotImplementedException();
  }

  //TODO: Exception handling
  public Task<bool> UpdateAsync(SeriesModel model) {
    throw new NotImplementedException();
  }
}
