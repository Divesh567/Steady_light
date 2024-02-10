using UnityEngine;
using UnityEngine.Rendering.Universal;


public class CheckpointControl : MonoBehaviour
{
    private Animator _animator;
    private Light2D _light2D;
    public bool _active = false;

    public int _spotLightPoint;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _light2D = GetComponent<Light2D>();
    }

    public void CheckpointAnimationTrigger()
    {
        if (!_active)
        {
            _animator.SetTrigger("Checkpoint");
            _light2D.enabled = true;
            _active = true;
        }
    }
}
