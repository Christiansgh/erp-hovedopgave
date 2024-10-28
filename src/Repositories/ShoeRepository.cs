using erp.Models;
using System.Data.SqlClient;

namespace erp.Repositories;

public class ShoeRepository : IRepository<Shoe, string, bool> {
  private DataAccessLayer _dataAccessLayer;

  public ShoeRepository(DataAccessLayer dataAccessLayer) {
    _dataAccessLayer = dataAccessLayer;
  }

  //TODO: Exception handling
  public async Task<bool> CreateAsync(Shoe model) {
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
  public Task<bool> DeleteAsync(Shoe model) {
    throw new NotImplementedException();
  }

  //TODO: Exception handling
  public async Task<List<Shoe>> ReadAllAsync() {
    var allShoes = new List<Shoe>();

    using (SqlDataReader result = await _dataAccessLayer.ExecuteQueryAsync("SELECT * FROM shoes")) {
      while (await result.ReadAsync()) {
        var shoe = new Shoe(
          Brand: result.GetString(result.GetOrdinal("Brand")),
          Color: result.GetString(result.GetOrdinal("Color")),
          Size: result.GetInt32(result.GetOrdinal("Size")),
          StockAmount: result.GetInt32(result.GetOrdinal("Stock"))
        );

        allShoes.Add(shoe);
      }
    }

    return allShoes;
  }

  //TODO: Exception handling
  public Task<Shoe> ReadByIdAsync(string id) {
    throw new NotImplementedException();
  }

  //TODO: Exception handling
  public Task<bool> UpdateAsync(Shoe model) {
    throw new NotImplementedException();
  }
}
