using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    private Touch _touch;
    private Vector3 _touchPos;
    [SerializeField]
    private GameObject _ball;
    [SerializeField]
    private float _force;
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchPos = _touchPos = Camera.main.ScreenToWorldPoint(_touch.position);

            if(_touch.phase == TouchPhase.Began)
            {
                Vector2 _directionToFace = _ball.transform.position - _touchPos;
                float _angle = Mathf.Atan2(_directionToFace.y, _directionToFace.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
            }
            else if(_touch.phase == TouchPhase.Moved)
            {
                Vector2 _directionToFace = _ball.transform.position - _touchPos;
                float _angle = Mathf.Atan2(_directionToFace.y, _directionToFace.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
            }
            else if(_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            { 
                Vector2 _directionToFace = _ball.transform.position - _touchPos;
                Rigidbody2D _ballRb = _ball.GetComponent<Rigidbody2D>();
                _ballRb.AddForce(_directionToFace * _force * Time.deltaTime, ForceMode2D.Impulse);
                gameObject.SetActive(false);
            }
        }

    }
}
