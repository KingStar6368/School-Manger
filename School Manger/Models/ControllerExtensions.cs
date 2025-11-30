using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;

public static class ControllerExtensions
{
    public static void AddKey(this Controller controller, string key, object Value)
    {
        controller.HttpContext.Session.SetString(key, Value.ToString());
    }
    public static void AddObject(this Controller controller, string key, object Value)
    {
        controller.HttpContext.Session.SetString(key, JsonSerializer.Serialize(Value));
    }
    public static T GetKey<T>(this Controller controller, string key)
    {
        try
        {
            if (controller.HttpContext.Session.GetString(key) == null)
                throw new InvalidOperationException("NUll Value On GetKey");
            string value = controller.HttpContext.Session.GetString(key);
            Type type = typeof(T);
            if (type == typeof(string))
                return (T)(object)value;
            if (type == typeof(long))
                return (T)(object)long.Parse(value);
            if (type == typeof(int))
                return (T)(object)int.Parse(value);
            if (type == typeof(float))
                return (T)(object)float.Parse(value);
            else
                return JsonSerializer.Deserialize<T>(value);
        }
        catch
        {
            throw new InvalidOperationException("Can Not Format Value (Convert)");
        }
    }

    // Append a notification into a list stored in TempData under the key "Notifications".
    public static void AddNotification(this Controller controller, Notification notification)
    {
        var list = new List<Notification>();
        if (controller.TempData["Notifications"] is string json && !string.IsNullOrEmpty(json))
        {
            try
            {
                var existing = JsonSerializer.Deserialize<List<Notification>>(json);
                if (existing != null)
                    list = existing;
            }
            catch
            {
                list = new List<Notification>();
            }
        }

        list.Add(notification);
        controller.TempData["Notifications"] = JsonSerializer.Serialize(list);
    }

    public static void ShowSuccess(this Controller controller, string title, string message)
    {
        var notification = new Notification
        {
            Type = NotificationType.Success,
            Title = title,
            Message = message
        };

        controller.AddNotification(notification);
    }

    public static void ShowWarning(this Controller controller, string title, string message)
    {
        var notification = new Notification
        {
            Type = NotificationType.Warning,
            Title = title,
            Message = message
        };

        controller.AddNotification(notification);
    }

    public static void ShowError(this Controller controller, string title, string message)
    {
        var notification = new Notification
        {
            Type = NotificationType.Error,
            Title = title,
            Message = message
        };

        controller.AddNotification(notification);
    }
}