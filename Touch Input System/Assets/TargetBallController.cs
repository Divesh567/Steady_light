using Cinemachine;
using UnityEngine;

public class TargetBallController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerObject;
    [SerializeField]
    private GameObject _ball1;
    [SerializeField]
    private GameObject _ball2;
    [SerializeField]
    private GameObject _cvCam;

    private GameObject _selectedGameball;

    private void Start()
    {
        _selectedGameball = _ball1;
    }
    public void OnSwitchTargetBallPressed()
    {
        if (_selectedGameball == _ball1)
        {
            _selectedGameball = _ball2;
            _playerObject.GetComponent<TouchController>()._gameBall = _selectedGameball;
            _cvCam.GetComponent<CinemachineVirtualCamera>().Follow = _selectedGameball.transform;
        }
        else
        {
            _selectedGameball = _ball1;
            _playerObject.GetComponent<TouchController>()._gameBall = _selectedGameball;
            _cvCam.GetComponent<CinemachineVirtualCamera>().Follow = _selectedGameball.transform;
        }
    }
}
