using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;


namespace FirebaseUtilities
{
    public class FirebaseRemoteConfigController : Singleton<FirebaseRemoteConfigController>
    {
        async void Start()
        {
            await FetchAllAsync();
            
            LevelLoader.Instance.LoadMainMenu();
        }
        

        
        private const string PROGRESSION_KEY = "progression_config";
        private const string OBSTACLE_KEY    = "obstacle_override_config";
        private const string BALANCE_KEY     = "game_balance_config";

        public RemoteProgressionConfig Progression { get; private set; }
        public ObstacleOverrideConfig  ObstacleOverride { get; private set; }
        public GameBalanceConfig  GameBalance { get; private set; }

        public bool IsFetched { get; private set; }

        public async Task FetchAllAsync()
        {
            try
            {
                await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
                await FirebaseRemoteConfig.DefaultInstance.ActivateAsync();

                IsFetched = true;

                Progression = Parse<RemoteProgressionConfig>(PROGRESSION_KEY)
                              ?? DefaultConfigs.Progression;

                ObstacleOverride = Parse<ObstacleOverrideConfig>(OBSTACLE_KEY)
                                   ?? DefaultConfigs.ObstacleOverride;

                GameBalance = Parse<GameBalanceConfig>(BALANCE_KEY)
                              ?? DefaultConfigs.GameBalance;
            }
            catch (Exception e)
            {
                IsFetched = true;

                Progression = DefaultConfigs.Progression;

                ObstacleOverride = DefaultConfigs.ObstacleOverride;

                GameBalance = DefaultConfigs.GameBalance;
            }
           

            Debug.Log("[RC] All configs fetched and defaulted safely");
        }

        private T Parse<T>(string key) where T : class
        {
            string json = FirebaseRemoteConfig.DefaultInstance
                .GetValue(key).StringValue;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning($"[RC] {key} empty");
                return null;
            }

            return JsonUtility.FromJson<T>(json);
        }
    }
}

public static class DefaultConfigs
{
    public static RemoteProgressionConfig Progression => new RemoteProgressionConfig
    {
        worlds = new List<RemoteProgressionData>()
    };

    public static ObstacleOverrideConfig ObstacleOverride => new ObstacleOverrideConfig
    {
        levels = new List<LevelObstacleOverride>()
    };

    public static GameBalanceConfig GameBalance => new GameBalanceConfig
    {
        ballGravity = 3f,
        lightForce = 20f,
        lives = 3,
        RemoteLevelTimers = new List<RemoteLevelTimer>()
    };
}