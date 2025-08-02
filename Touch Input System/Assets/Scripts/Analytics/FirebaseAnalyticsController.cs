using UnityEngine;
using Firebase.Analytics;
using System.Collections.Generic;

public static class FirebaseAnalyticsController 
{
    public static void LogEvent(AnalyticsEvent analyticsEvent)
    {
#if UNITY_EDITOR
        Debug.Log($"[Analytics] {analyticsEvent.eventName} | {FormatParams(analyticsEvent.keyValues)}");
        return;
#endif


        if (analyticsEvent.keyValues == null || analyticsEvent.keyValues.Count == 0)
        {
            // No parameters
            FirebaseAnalytics.LogEvent(analyticsEvent.eventName.ToString());
            return;
        }

        // Convert to Firebase parameter array
        var firebaseParams = new List<Parameter>();
        foreach (var kvp in analyticsEvent.keyValues)
        {
            string paramKey = kvp.Key.ToString(); // Convert enum to string
            object value = kvp.Value;

            if (value is int intVal)
                firebaseParams.Add(new Parameter(paramKey, intVal));
            else if (value is float floatVal)
                firebaseParams.Add(new Parameter(paramKey, floatVal));
            else if (value is double doubleVal)
                firebaseParams.Add(new Parameter(paramKey, doubleVal));
            else
                firebaseParams.Add(new Parameter(paramKey, value.ToString()));
        }

        FirebaseAnalytics.LogEvent(analyticsEvent.eventName.ToString(), firebaseParams.ToArray());
    }

#if UNITY_EDITOR
    private static string FormatParams(Dictionary<ParamName, object> parameters)
    {
        if (parameters == null) return "None";
        var formatted = "";
        foreach (var kvp in parameters)
            formatted += $"{kvp.Key}: {kvp.Value}, ";
        return formatted.TrimEnd(',', ' ');
    }
#endif
}

public enum EventName
{
    LevelStart,
    LevelComplete,
    LevelLost,
    LevelRetry,
    Death,
    QuitDuringLevel
}

public enum ParamName
{
    Level_Name,
    DeathCount,
    time_Taken,
    ObstacleId
}

public class AnalyticsEvent
{
    public EventName eventName;
    public Dictionary<ParamName, object> keyValues;
    public string dynamicEventName;
    public AnalyticsEvent(EventName eventName)
    {
        this.eventName = eventName;
        keyValues = new Dictionary<ParamName, object>();
    }

    public AnalyticsEvent AddParam(ParamName param, object value)
    {
        keyValues[param] = value;
        return this;
    }

    public AnalyticsEvent(string eventName)
    {
        this.dynamicEventName = eventName;
        keyValues = new Dictionary<ParamName, object>();
    }
}
