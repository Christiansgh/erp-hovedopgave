﻿using erp.Exceptions;
using erp.Services;
using System.Data.SqlClient;

namespace erp;

public class DataAccessLayer {
    private readonly string? _connectionString;
    private readonly ILoggerService _logger;

    public DataAccessLayer(ILoggerService logger) {
        _logger = logger;
        _connectionString = Environment.GetEnvironmentVariable("HOVEDOPGAVE_ERP_CONNECTION_STRING");

        if (string.IsNullOrEmpty(_connectionString)) {
            _logger.LogError("DataAccessLayer - Environment Variable $HOVEDOPGAVE_ERP_CONNECTION_STRING not found.");
            throw new EnvironmentVariableMissingException("DataAccessLayer", "HOVEDOPGAVE_ERP_CONNECTION_STRING");
        }
    }

    public Task<SqlDataReader> ExecuteQueryAsync(string query) {
        var con = new SqlConnection(_connectionString);
        try {
            con.Open();
            var command = new SqlCommand(query, con);
            return command.ExecuteReaderAsync();
        } catch (Exception ex) {
            _logger.LogError($"DataAccessLayer - Failed to execute the query. con string: '{_connectionString}', query: '{query}', {ex.Message}");
            throw;
        }
    }
}
