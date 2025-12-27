using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPointMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform _point1;
    [SerializeField]
    private Transform _point2;

    private bool _follow = true;
    private bool _returing = true;
    private Transform _currentPoint;
    [SerializeField]
    private float _speed;

    private void Start()
    {
        _currentPoint = _point2;
    }

    private void Update()
    {
        if(MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;
        
        if(_follow == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PointToPoint"))
        {
            if (!_returing)
            {
                _currentPoint = _point2;
            }
            else
            {
                _currentPoint = _point1;
            }
            _returing = !_returing;
        }
    }


}
