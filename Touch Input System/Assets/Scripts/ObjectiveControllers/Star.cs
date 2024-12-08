using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
    [SerializeField]
    GameObject _starCollectedFx;
    private void Awake()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PitchChangeStarObjective();
        }
    }
    private void Start()
    {
        Debug.Log("TRIGGERED " + gameObject.name);
        ObjectiveEventHandler.OnStarInitEventCaller(this);

    }

    public void StarCollected()
    {
        ObjectiveEventHandler.OnStarCollectedEventCaller(this);

        Instantiate(_starCollectedFx, transform.position, transform.rotation);
        Destroy(gameObject);
       
    }
}
