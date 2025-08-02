using System.Collections;
using UnityEngine;

public class ElectricWavePowerupControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private GameObject _powerup;
    private ButtonVisualFeedBack _bvf;

    IEnumerator PowerUp()
    {
       yield return null;
    }
}
