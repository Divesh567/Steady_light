using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonVisualFeedBack : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _poweruptext;

    private void Start()
    {
        _image = transform.GetChild(0).GetComponent<Image>();
        _poweruptext = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        int powerup = MyGameManager.Instance.SetPowerUpText();
        _poweruptext.text = powerup.ToString();
    }

    public void PowerUpUsedVisualFeedback()
    {
        int powerup = MyGameManager.Instance.SetPowerUpText();
        _poweruptext.text = powerup.ToString();
    }

    public void PowerUpRegainedVisualFeedBack()
    {
        int powerup = MyGameManager.Instance.SetPowerUpText();
        _poweruptext.text = powerup.ToString();
    }

}
