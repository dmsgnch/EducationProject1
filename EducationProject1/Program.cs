using EducationProject1.Components.Events.CollisionEvents;
using EducationProject1.Services.FigureSaveListCreatorServices;
using EducationProject1.Services.FigureSaveListCreatorServices.Abstract;
using EducationProject1.Services.FigureSaveListLoaderServices;
using EducationProject1.Services.FigureSaveListLoaderServices.Abstract;
using EducationProject1.Services.SaveLoaderSelectorServices;
using EducationProject1.Services.SaveLoaderSelectorServices.Abstract;
using EducationProject1.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EducationProject1;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddTransient<SaveLoaderSelectorServiceBase, SaveLoaderSelectorService>();
                services.AddTransient<FigureSaveListCreatorServiceBase, FigureSaveListCreatorService>();
                services.AddTransient<FigureSaveListLoaderServiceBase, FigureSaveListLoaderService>();
                
                services.AddLogging(configure =>
                {
                    configure.AddConsole();
                    configure.AddDebug();   
                });
            })
            .Build();
        var app = host.Services.GetService<App>();
        App.ServiceProvider = host.Services;
        app?.Run();
    }
}