using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private float _force;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(_force, 0), ForceMode2D.Impulse);
    }


}
