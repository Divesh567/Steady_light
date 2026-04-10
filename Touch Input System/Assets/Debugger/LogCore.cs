using UnityEngine;

public static class LogCore
{
    private static Logger settings;

    public static void Initialize(Logger config)
    {
        settings = config;
    }

    public static void Log(LogCat category, object obj)
    {
        var message = obj?.ToString() ?? "null";

        if (settings == null)
        {
            Debug.LogWarning("LogCore is not initialized with a LogSettingsSO.");
            return;
        }

        var cat = settings.GetCategory(category);
        if (cat == null || !cat.enabled) return;

        string prefix = $"<color=#{ColorUtility.ToHtmlStringRGB(cat.color)}>[{category}]</color>";
        Debug.Log($"{prefix} {message}");
    }
}
