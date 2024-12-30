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
        
    }

    public void OnPowerUpButtonPressed()
    {
       
    }

    IEnumerator PowerUp()
    {
       yield return null;
    }
}
