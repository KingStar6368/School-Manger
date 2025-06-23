using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public static class ControllerExtensions
{
    public static void AddKey(this Controller controller, string key, object Value)
    {
        controller.HttpContext.Session.SetString(key, Value.ToString());
    }
    public static T GetKey<T>(this Controller controller, string key)
    {
        if (controller.HttpContext.Session.GetString(key) == null)
            throw new InvalidOperationException("NUll Value On GetKey");
        string value = controller.HttpContext.Session.GetString(key);
        Type type = typeof(T);
        if (type == typeof(string))
            return (T)(object)value;
        if(type == typeof(long))
            return (T)(object)long.Parse(value);
        if (type == typeof(int))
            return (T)(object)int.Parse(value);
        if (type == typeof(float))
            return (T)(object)float.Parse(value);
        else
            throw new InvalidOperationException("Can Not Format Value (Convert)");
    }

    public static void ShowSuccess(this Controller controller, string title, string message)
    {
        controller.TempData["Notification"] = "";
        var notification = new Notification
        {
            Type = NotificationType.Success,
            Title = title,
            Message = message
        };

        // Serialize to JSON
        controller.TempData["Notification"] = JsonSerializer.Serialize(notification);
    }

    public static void ShowWarning(this Controller controller, string title, string message)
    {
        controller.TempData["Notification"] = "";
        var notification = new Notification
        {
            Type = NotificationType.Warning,
            Title = title,
            Message = message
        };

        // Serialize to JSON
        controller.TempData["Notification"] = JsonSerializer.Serialize(notification);
    }

    public static void ShowError(this Controller controller, string title, string message)
    {
        controller.TempData["Notification"] = "";
        var notification = new Notification
        {
            Type = NotificationType.Error,
            Title = title,
            Message = message
        };

        // Serialize to JSON
        controller.TempData["Notification"] = JsonSerializer.Serialize(notification);
    }
}