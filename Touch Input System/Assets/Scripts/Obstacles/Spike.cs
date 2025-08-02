using UnityEngine;

public class Spike : Obstacle
{
    private void OnEnable()
    {
        onTriggerEnterEvent.AddListener(OnBallCollided);
    }

    private void OnDisable()
    {
        onTriggerEnterEvent.RemoveAllListeners();
    }
    public void OnBallCollided(Collider2D collider2D)
    {
        string name = obstacleName.Replace(" ", "");

        AnalyticsEvent analyticsEvent = new AnalyticsEvent(EventName.Death)
                                                        .AddParam(ParamName.ObstacleId, LevelLoader.Instance.GetCurrentSceneName() + obstacleName);
        FirebaseAnalyticsController.LogEvent(analyticsEvent);
    }
}
