using UnityEngine;
using UnityEngine.UI;


public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _ball;
    private float _shieldtime = 5f;
    private ButtonVisualFeedBack _bvf;

    private Text _shieldActiveText;

    private bool _shieldOn = false;

    private void Start()
    {
        if (MyGameManager.Instance != null)
        {
            MyGameManager.Instance.GetCurrentPowerup(this.gameObject);
        }
        _bvf = GetComponent<ButtonVisualFeedBack>();
        _shieldActiveText = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
    }


    public void OnPowerUpButtonPressed()
    {
        if (MyGameManager.Instance._currentPowers >= 1)
        {
            MyGameManager.Instance._currentPowers--;
            _shieldOn = !_shieldOn;
            if (_shieldOn)
            {
                _shield.GetComponent<ShieldControl>().ShieldOn();
                _shieldActiveText.text = "ON";
            }
            else
            {
                _shield.GetComponent<ShieldControl>().ShieldOff();
                _shieldActiveText.text = "OFF";
            }
            _bvf.PowerUpUsedVisualFeedback();
        }
        else
        {
            _bvf.PowerUpUsedVisualFeedback();
        }
    }


    private void Update()
    {
        if (_shieldOn)
        {
            if (_shieldtime > 0)
            {
                _shieldtime -= Time.deltaTime;
            }
            else
            {
                _shield.GetComponent<ShieldControl>().ShieldDestroyed();
                _shieldtime = 5f;
                _bvf.PowerUpUsedVisualFeedback();
            }
        }
    }
}
