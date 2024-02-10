using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BallCollisions : MonoBehaviour
{
    private TouchController _player;

    private Transform _lastCheckPoint = null;
    private Rigidbody2D _ballRigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private Vector3 _startPos;
    private Light2D _light2D;
    private Collider2D _collider;

    public bool _invulnerable = false;

    [Header("Ball Death Effects")]
    [SerializeField]
    private GameObject _deathVfx;
    [SerializeField]
    private AudioClip _ballDeathSfx;
    [SerializeField]
    private float _ballDeathSfxVolume;

    [Header("Ball Bounce Effects")]
    [SerializeField]
    private GameObject _bounceVfx;
    [SerializeField]
    private AudioClip _ballBounceSfx;
    [SerializeField]
    private float _ballBounceSfxVolume;

    private void Start()
    {
        MyGameManager.Instance.GetBall(GetComponent<BallCollisions>());
        _startPos = transform.position;
        _collider = GetComponent<Collider2D>();
        _ballRigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        GameMenu.Instance.GetAndSetBallSprite(_spriteRenderer.sprite);
        _lastCheckPoint = null;
    }

    public void GetPlayerObject(TouchController _playerobject)
    {
        _player = _playerobject;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            if (!_invulnerable)
            {
                if (MyGameManager.Instance != null)
                {
                    MyGameManager.Instance.LifeLost();
                    Instantiate(_deathVfx, transform.position, transform.rotation);
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlayBallDeath(_ballDeathSfx, _ballDeathSfxVolume);
                    }
                    StartCoroutine(Respawn());
                }
            }
        }

        if (collision.IsTouching(_collider))
        {
            if (collision.CompareTag("Objective"))
            {
                if (MyGameManager.Instance != null)
                {
                    MyGameManager.Instance.LevelWon();
                }
            }
            else if (collision.CompareTag("Spike"))
            {
                if (!_invulnerable)
                {
                    if (MyGameManager.Instance != null)
                    {
                        MyGameManager.Instance.LifeLost();
                        Instantiate(_deathVfx, transform.position, transform.rotation);
                        if (SoundManager.Instance != null)
                        {
                            SoundManager.Instance.PlayBallDeath(_ballDeathSfx, _ballDeathSfxVolume);
                        }
                        StartCoroutine(Respawn());
                    }
                    
                }
            }
            else if (collision.CompareTag("Star"))
            {
                var star = collision.gameObject.GetComponent<StarControl>();
                if (star != null)
                {
                    star.StarCollected();
                }
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlayStarCollected();
                }
            }
            else if (collision.CompareTag("CheckPoint"))
            {
                _lastCheckPoint = collision.transform;
                var checkPoint = collision.GetComponent<CheckpointControl>();
                if (checkPoint._active == false)
                {
                    checkPoint.CheckpointAnimationTrigger();
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlayCheckpointReached();
                    }
                    MyGameManager.Instance.AddTimeFromUpgrades();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayBallBounce(_ballBounceSfx, _ballBounceSfxVolume);
            }
            Instantiate(_bounceVfx, transform.position, transform.rotation);
        }
    }

    public void Reset()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        transform.DetachChildren();
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _light2D.enabled = false;
        _trailRenderer.enabled = false;
        _ballRigidbody.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(1.5f);

        GoToLastPosition();
        _player.GetComponent<TouchController>().Reset();
        _collider.enabled = true;
        _spriteRenderer.enabled = true;
        _light2D.enabled = true;
        _trailRenderer.enabled = true;
        _ballRigidbody.velocity = new Vector2(0, 0);
    }

    public void GoToLastPosition()
    {
        if (_lastCheckPoint == null)
        {
            transform.position = _startPos;
            _player.GetComponent<TouchController>().Reset();
        }
        else
        {
            transform.position = _lastCheckPoint.position; 
            _player.GetComponent<TouchController>().Reset();
        }
    }
        
}
