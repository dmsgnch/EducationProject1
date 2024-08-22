namespace EducationProject1.Components.Extensions.EventArgsExtensions;

public static class EventArgExtensions
{
    public static void Raise<TEventArgs>(
        this TEventArgs e,
        Object sender,
        ref EventHandler<TEventArgs> eventDelegate)
    {
        EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
        if (temp is not null) temp(sender, e);
    }
}