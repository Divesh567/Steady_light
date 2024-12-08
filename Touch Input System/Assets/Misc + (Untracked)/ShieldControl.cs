using UnityEngine;

public class ShieldControl : MonoBehaviour
{
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private GameObject _ball;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _ball = transform.parent.gameObject;
    }

    public void ShieldOn()
    {
        _ball.GetComponent<BallCollisions>()._invulnerable = true;
        _collider2D.enabled = true;
        _spriteRenderer.enabled = true;
        _animator.enabled = true;
    }

    public void ShieldOff()
    {
        _ball.GetComponent<BallCollisions>()._invulnerable = false;
        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
        _animator.enabled = false;
    }

    public void ShieldDestroyed()
    {
        ShieldOff();
    }
}
