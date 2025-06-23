// Models/Notification.cs
public enum NotificationType
{
    Success,
    Warning,
    Error
}
public class Notification
{
    public NotificationType Type { get; set; } // "success", "warning", "error"
    public string Title { get; set; }
    public string Message { get; set; }
}