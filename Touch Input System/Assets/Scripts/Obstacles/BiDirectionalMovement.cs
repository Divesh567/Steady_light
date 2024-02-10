using System.Collections;
using UnityEngine;

public class BiDirectionalMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 _moveDirection;
    [SerializeField]
    private Vector3 _rotationDirection;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _timer;

    private void Start()
    {
        StartCoroutine(ChangeDirection());
    }
    private void Update()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.World);
        transform.Rotate(_rotationDirection, Space.Self);
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(_timer);
        _moveDirection = -_moveDirection;
        _rotationDirection = -_rotationDirection;
        StartCoroutine(ChangeDirection());
        if (GetComponent<SawVfx>() != null)
        {
            GetComponent<SawVfx>().FlipObject();
        }
    }
}
