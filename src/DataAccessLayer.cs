using System.Data.SqlClient;

namespace erp;

public class DataAccessLayer {
  private string _connectionString;

  public DataAccessLayer() {//TODO: Consider using env var or user secret instead.
    _connectionString = "Data Source=37.27.179.21\\SQLEXPRESS22,1433;database=ERP;user id=sa;password='itsteatime-123';TrustServerCertificate=True";
  }

  //TODO: Exception handling
  public Task<SqlDataReader> ExecuteQueryAsync(string query) {
    var con = new SqlConnection(_connectionString);
    con.Open();
    var command = new SqlCommand(query, con);
    return command.ExecuteReaderAsync();
  }
}
