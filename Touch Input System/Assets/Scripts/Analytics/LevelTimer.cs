using System;
using UnityEngine;

public class LevelTimer 
{
    private DateTime startTime;
    public void StartTimer()
    {
        startTime = DateTime.UtcNow;
    }

    public void StopTimer(bool wonLevel)
    {
        var stopTime = DateTime.UtcNow;

        TimeSpan totalTime = stopTime - startTime;

        if (wonLevel)
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEvent(EventName.LevelComplete.ToString() + LevelLoader.Instance.GetCurrentSceneName())
                                                            .AddParam(ParamName.time_Taken, totalTime.TotalSeconds);
            FirebaseAnalyticsController.LogEvent(analyticsEvent);

        }
        else
        {
            AnalyticsEvent analyticsEvent = new AnalyticsEvent(EventName.LevelLost)
                                                          .AddParam(ParamName.Level_Name, LevelLoader.Instance.GetCurrentSceneName());
            FirebaseAnalyticsController.LogEvent(analyticsEvent);
        }


    }
}
