namespace erp.Exceptions;

public class EnvironmentVariableMissingException : Exception {
    public EnvironmentVariableMissingException(string path, string envVarName) : base($"{path} - Environment variable '${envVarName}' not found.") { }
}
