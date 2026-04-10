using UnityEngine;

public class LoggerInitializer : MonoBehaviour
{
    [SerializeField] private Logger logSettings;

    private void Awake()
    {
        LogCore.Initialize(logSettings);
    }
}
