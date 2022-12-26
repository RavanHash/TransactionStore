namespace LoggerService;

public interface ILoggerManager
{
    void LogError(string message);
    void LogWarning(string message);
    void LogDebug(string message);
    void LogInformation(string message);
}