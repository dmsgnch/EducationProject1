namespace EducationProject1.Components.Events.CollisionEvents;

public class NewCollisionEventArgs : EventArgs 
{
    public string From { get; }
    public string To { get; }
    public string Subject { get; }
    public string Point { get; }

    public NewCollisionEventArgs(string from, string to, string subject, string point)
    {
        From = from;
        To = to;
        Subject = subject;
        Point = point;
    }
}