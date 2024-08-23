using System.Windows.Threading;

namespace EducationProject1.Components.Helpers;

public static class UiHelper
{
    private static Dispatcher _dispatcher;

    public static void Initialize(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public static void RunOnUiThread(Action action)
    {
        _dispatcher?.Invoke(action);
    }
}