namespace erp.Services;

public class LoggerService : ILoggerService {
  public void LogToFile(LogLevel level, string message) {
    switch (level) {
      case LogLevel.Information: message = "Information:" + message; break;
      case LogLevel.Warning: message = "Warning:" + message; break;
      case LogLevel.Error: message = "Error:" + message; break;
    }
    File.AppendAllLines(GetLogFile(), [message]);
  }

  public void LogInfo(string message) {
    LogToFile(LogLevel.Information, message);
  }

  public void LogWarn(string message) {
    LogToFile(LogLevel.Information, message);
  }

  public void LogError(string message) {
    LogToFile(LogLevel.Information, message);
  }

  public string GetLogFile() {
    string today = "";

    try {
      if (!Directory.Exists("logs")) {
        Directory.CreateDirectory("logs");
      }

      today = "logs/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
      if (!File.Exists(today)) {
        using FileStream _ = File.Create(today);
      }

    } catch { }

    return today;
  }
}
