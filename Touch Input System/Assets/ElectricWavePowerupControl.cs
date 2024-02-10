using System.Collections;
using UnityEngine;

public class ElectricWavePowerupControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private GameObject _powerup;
    private ButtonVisualFeedBack _bvf;

    private void Start()
    {
        if (MyGameManager.Instance != null)
        {
            MyGameManager.Instance.GetCurrentPowerup(this.gameObject);
        }
        _bvf = GetComponent<ButtonVisualFeedBack>();
    }

    public void OnPowerUpButtonPressed()
    {
        if (MyGameManager.Instance._currentPowers >= 1)
        {
            MyGameManager.Instance._currentPowers--;
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayPowerUp(2);
            }
            StartCoroutine("PowerUp");
            _bvf.PowerUpUsedVisualFeedback();
        }
        else
        {
            _bvf.PowerUpUsedVisualFeedback();
        }
    }

    IEnumerator PowerUp()
    {
        _ball.GetComponent<BallCollisions>()._invulnerable = true;
        _ball.GetComponent<Animator>().SetTrigger("PowerUp");
        Instantiate(_powerup, _ball.transform.position, _powerup.transform.rotation);
        yield return new WaitForSeconds(1f);
        _ball.GetComponent<BallCollisions>()._invulnerable = false;
        StopCoroutine("PowerUp");
    }
}
