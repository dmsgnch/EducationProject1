namespace EducationProject1.Components.Loggers.Abstract;

public abstract class LogMachineBase
{
    public abstract Task LogToFile(LogType logType, string message);
    public abstract Task LogToConsole(LogType logType, string message);
}