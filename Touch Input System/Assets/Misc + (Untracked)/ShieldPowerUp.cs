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

        _bvf = GetComponent<ButtonVisualFeedBack>();
        _shieldActiveText = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>();
    }


    public void OnPowerUpButtonPressed()
    {

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
