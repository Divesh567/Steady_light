using UnityEngine;
using System.Collections;

public class FireballPowerup : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private GameObject _powerUp;
    private ButtonVisualFeedBack _bvf;
    [SerializeField]
    private AudioClip _fireUpSfx;
    [SerializeField]
    private bool _infinitePowerUp = false;
    public bool _isPowerUpActive = false;

    private void Start()
    {

        _bvf = GetComponent<ButtonVisualFeedBack>();
    }


    public void OnPowerUpButtonPressed()
    {
        

    }

    IEnumerator PowerUp()
    {
        yield return new WaitForSeconds(0f);
        _ball.GetComponent<BallCollisions>()._invulnerable = true;
        _ball.GetComponent<TrailRenderer>().enabled = false;
        GameObject Powerup = Instantiate(_powerUp, _ball.transform.position, _ball.transform.rotation);
        Powerup.GetComponent<FireBall>()._powerUpController = this;
        Powerup.transform.parent = _ball.transform;
    }
}



