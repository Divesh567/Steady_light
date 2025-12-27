using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnimTriggerManager : MonoBehaviour
{
    [SerializeField] private IntEventChannel eventChannel;
    [SerializeField] private List<AnimTriggerConfig> animTriggers;

    private void OnEnable()
    {
        if (eventChannel != null)
            eventChannel.OnEventRaised += PlayAnimById;
    }

    private void OnDisable()
    {
        if (eventChannel != null)
            eventChannel.OnEventRaised -= PlayAnimById;
    }

    private void PlayAnimById(int animId)
    {
        var trigger = animTriggers.Find(x => x.id == animId);
        if (trigger != null)
            trigger.onPlay.Invoke();
        else
            Debug.LogWarning($"[AnimTriggerManager] No animation trigger found for ID {animId}");
    }
}


[System.Serializable]
public class AnimTriggerConfig
{
    public int id;
    public UnityEvent onPlay;
    public UnityEvent onStop;
}
