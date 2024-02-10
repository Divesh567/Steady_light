using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private CinemachineVirtualCamera _vCam;
    private CinemachineCameraOffset _vCamCameraOffset;

    [SerializeField]
    private float _offset;
    [SerializeField]
    private float _offsetLerp;


    private void Start()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _vCamCameraOffset = _vCam.GetComponent<CinemachineCameraOffset>();
    }

    private void Update()
    {
        Vector2 _playerDir = (-_player.transform.up) * _offset;
        _vCamCameraOffset.m_Offset = Vector3.Lerp(new Vector3(_vCamCameraOffset.m_Offset.x, _vCamCameraOffset.m_Offset.y, -10f), new Vector3(-_playerDir.x, -_playerDir.y, -10f), (_offsetLerp * Time.deltaTime));
    }
}
