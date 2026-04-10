using UnityEngine.Events;

public abstract class GameEventChannel<T> : BaseEventChannel
{
    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T data)
    {
        OnEventRaised?.Invoke(data);
    }
}