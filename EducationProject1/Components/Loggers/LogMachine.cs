using System.IO;
using EducationProject1.Components.Loggers.Abstract;

namespace EducationProject1.Components.Loggers;

public class LogMachine : LogMachineBase
{
    const string LogFileName = @"myApp.log";

    public override async Task LogToFile(LogType logType, string message)
    {
        await using (StreamWriter file = new StreamWriter(LogFileName, true))
        {
            string logData = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logType}] {message}";
            await file.WriteLineAsync(logData);
        }
    }

    public override async Task LogToConsole(LogType logType, string message)
    {
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logType}] {message}");
    }
}