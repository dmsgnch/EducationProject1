using System.Windows;
using EducationProject1.Views;

namespace EducationProject1;

public class App : Application
{
    private readonly MainWindow _mainWindow;

    public static IServiceProvider ServiceProvider { get; set; }

    public App(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        _mainWindow.Show(); 
    }
}