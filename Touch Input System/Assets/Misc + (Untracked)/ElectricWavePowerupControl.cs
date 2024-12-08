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

        _bvf = GetComponent<ButtonVisualFeedBack>();
    }

    public void OnPowerUpButtonPressed()
    {

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
