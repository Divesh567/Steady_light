using System;
using UnityEngine;

[Serializable]
public class LogCategory
{
    public LogCat categoryName;
    public bool enabled = true;
    public Color color = Color.white;
}

public enum LogCat 
{ 
    None,
    Default,
}

