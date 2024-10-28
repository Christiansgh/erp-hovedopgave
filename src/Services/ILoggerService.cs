namespace erp.Services;

public interface ILoggerService {
  void LogToFile(LogLevel level, string message);
  void LogInfo(string message);
  void LogWarn(string message);
  void LogError(string message);
  string GetLogFile();
}
