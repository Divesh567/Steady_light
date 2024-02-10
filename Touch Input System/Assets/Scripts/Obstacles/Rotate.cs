using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField]
    private float _timer;
    [SerializeField]
    private float _deafultTimer;
    private Animator _animator;
    [SerializeField]
    private bool _rotate;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer = _timer - 0.25f * Time.deltaTime;
        if (_timer <= 0)
        {
            if (!_rotate)
            {
                _rotate = true;
                _animator.SetBool("Rotate", _rotate);
            }
            else
            {
                _rotate = false;
                _animator.SetBool("Rotate", _rotate);
            }
            _timer = _deafultTimer;
        }
    }
}
