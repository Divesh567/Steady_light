using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Logger_New", menuName = "Logging/New Logger")]
public class Logger : ScriptableObject
{
    public List<LogCategory> categories = new();

    public LogCategory GetCategory(LogCat categoryName)
    {
        return categories.Find(c => c.categoryName == categoryName);
    }
}
