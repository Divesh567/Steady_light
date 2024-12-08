using System.Collections;
using UnityEngine;

public class MagicSmokePowerUpController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private GameObject _resetVfx;

    private BallCollisions _ballCollisions;
    private Rigidbody2D _ballRB2D;
    private SpriteRenderer _ballSR;
    private TrailRenderer _ballTR;
    private ButtonVisualFeedBack _bvf;

    private void Start()
    {

        _ballCollisions = _ball.GetComponent<BallCollisions>();
        _ballSR = _ball.GetComponent<SpriteRenderer>();
        _ballTR = _ball.GetComponent<TrailRenderer>();
        _ballRB2D = _ball.GetComponent<Rigidbody2D>();
        _bvf = GetComponent<ButtonVisualFeedBack>();
    }

    public void OnPowerUpButtonPressed()
    {

    }

    IEnumerator PowerUp()
    {
        _ballSR.enabled = false;
        _ballTR.enabled = false;
        _ballCollisions._invulnerable = true;
        _ballRB2D.linearVelocity = new Vector2(0, 0);
        SoundManager.Instance.PlayPowerUp(0);
        Instantiate(_resetVfx, _ball.transform.position, _ball.transform.rotation);
        yield return new WaitForSeconds(1f);
        _ball.gameObject.GetComponent<BallCollisions>().GoToLastPosition();
        SoundManager.Instance.PlayPowerUp(0);
        Instantiate(_resetVfx, _ball.transform.position, _ball.transform.rotation);
        _ballRB2D.linearVelocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.25f);
        _ballCollisions._invulnerable = false;
        _ballSR.enabled = true;
        _ballTR.enabled = true;
        StopCoroutine("PowerUp");
    }


}
