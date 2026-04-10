using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleOverrides
{
    
    
}

[Serializable]
public class GameBalanceConfig
{
    public float ballGravity;
    public float lightForce;
    public int lives;
    public List<RemoteLevelTimer> RemoteLevelTimers;
}

[Serializable]
public class RemoteLevelTimer
{
    public string levelName = "";
    public float time = 100;
}

[Serializable]
public class ObstacleOverrideConfig
{
    public List<LevelObstacleOverride> levels;
}

[Serializable]
public class LevelObstacleOverride
{
    public string levelName = "";
    public List<ObstacleOverride> obstacles;
}

[Serializable]
public class ObstacleOverride
{
    public string obstacleId;
    public bool enabled;
}
