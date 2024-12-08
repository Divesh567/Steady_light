using UnityEngine;

public class TrajectoryControl : MonoBehaviour
{
    private GameObject _ball;
    private Rigidbody2D _ballR2D;
    private Vector2 _ballVelocity;

    private void Start()
    {
        _ball = transform.parent.gameObject;
        _ballR2D = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _ballVelocity = _ballR2D.linearVelocity.normalized;

    }
}
