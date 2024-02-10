using UnityEngine;

public class MoveToPositon : MonoBehaviour
{
    [SerializeField]
    private Vector3 _toMovePos;
    [SerializeField]
    private float _speed;

    public bool _move = false;

    private void Update()
    {
        if (_move)
        {
            StartMoving();
        }
    }


    private void StartMoving()
    {
        Vector3 _newPos = Vector3.MoveTowards(transform.position, _toMovePos, _speed * Time.deltaTime);
        transform.position = _newPos;
    }
}
