using System.Data.SqlClient;

namespace erp_hovedopgave.DataAccess;

public static class DataAccessLayer { //TODO: Consider using env var or user secret instead.
  private static readonly string _connectionString = "Data Source=37.27.179.21\\SQLEXPRESS22,1433;database=ERP;user id=sa;password='itsteatime-123';TrustServerCertificate=True";
  //TODO: Exception handling
  public static async Task<SqlDataReader> ExecuteQuery(string query) {
    var con = new SqlConnection(_connectionString);
    con.Open();
    var command = new SqlCommand(query, con);
    SqlDataReader result = await command.ExecuteReaderAsync();
    return result;
  }
}
