using System;
using Firebase.RemoteConfig;
using UnityEngine;

namespace FirebaseUtilities
{
    public class FirebaseRemoteConfigController
    {
        private const string RC_KEY = "progression_config";

        public RemoteProgressionConfig Config { get; private set; }
        public bool HasValidConfig => Config != null;

        public async void FetchAndApply(Action onComplete = null)
        {
            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            await FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

            string json =
                FirebaseRemoteConfig.DefaultInstance
                    .GetValue(RC_KEY).StringValue;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("[RC] Empty progression config");
                Config = null;
            }
            else
            {
                Config = JsonUtility.FromJson<RemoteProgressionConfig>(json);
                Debug.Log("[RC] Progression config loaded " + json);
            }
            
            onComplete?.Invoke();
        }

    }
}