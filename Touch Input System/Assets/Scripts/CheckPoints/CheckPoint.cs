using UnityEngine;
using UnityEngine.Rendering.Universal;


public class CheckPoint : MonoBehaviour
{
    private Light2D _light2D;
    public bool _active = false;

    public int _spotLightPoint;

    public CheckPointManager manager;
    public CheckpointAnimation ckAnimation;

    private void Awake()
    {
        manager = GetComponentInParent<CheckPointManager>();
        ckAnimation = GetComponent<CheckpointAnimation>();
    }

    private void Start()
    {
        _light2D = GetComponent<Light2D>();
    }

    public void CheckpointAnimationTrigger()
    {

       // ckAnimation.AnimateCheckPoint();
        _light2D.enabled = true;
        _active = true;
       
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayCheckpointReached(); // TODO AUDIO Refactor this
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (!_active)
            {
                collision.GetComponent<BallCollisions>()._lastCheckPoint = this.transform;
                manager.currentCheckPoint = transform.GetSiblingIndex();
                CheckpointAnimationTrigger();
              
            }
        }
    }
}
