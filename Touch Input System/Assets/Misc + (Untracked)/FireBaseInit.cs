using Firebase;
using Firebase.Analytics;
using UnityEngine;

public class FireBaseInit : MonoBehaviour
{
    private static FireBaseInit _instance;
    public static FireBaseInit Instance { get { return _instance; } }
    private string _levelComplete = "Played level 6";
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public Firebase.FirebaseApp app;
    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void LogEventLevelPlaying()
    {
        FirebaseAnalytics.LogEvent("Level_6_Reached");
    }

    public void LogEventLevelComplete(int level_complete)
    {
        FirebaseAnalytics.LogEvent("Level_completed", "Level_number", level_complete.ToString());
    }

    public void LogEventLevelFailed(int level_failed)
    {
        FirebaseAnalytics.LogEvent("Level_Failed", "Level_number", level_failed.ToString());
    }

    public void LogEventOnFireBase(string eventName)
    {
        FirebaseAnalytics.LogEvent(eventName);
    }


}
