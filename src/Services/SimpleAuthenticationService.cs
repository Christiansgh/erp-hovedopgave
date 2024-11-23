using erp.Exceptions;

namespace erp.Services;

public class SimpleAuthenticationService : IAuthenticationService<KeyValuePair<string, string>> {
    private readonly string? _username;
    private readonly string? _password;
    private readonly ILoggerService _logger;

    public SimpleAuthenticationService(ILoggerService logger) {
        _username = Environment.GetEnvironmentVariable("API_USER");
        _password = Environment.GetEnvironmentVariable("API_PASS");
        _logger = logger;

        if (string.IsNullOrEmpty(_username)) {
            _logger.LogError("SimpleAuthenticationService - Environment Variable '$API_USER' not found.");
            throw new EnvironmentVariableMissingException("SimpleAuthenticationService", "API_USER");
        }

        if (string.IsNullOrEmpty(_password)) {
            _logger.LogError("DataAccessLayer - Environment Variable '$API_PASS' not found.");
            throw new EnvironmentVariableMissingException("DataAccessLayer", "API_PASS");
        }
    }

    public bool Authenticate(KeyValuePair<string, string> login) {
        return string.Equals(_username, login.Key) && string.Equals(_password, login.Value);
    }
}

