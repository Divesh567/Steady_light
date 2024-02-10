using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
    }

    private void ResetPositionToStartPos()
    {
        transform.position = _startPos;
    }
}
