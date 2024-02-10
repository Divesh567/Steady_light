using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDashSystem : MonoBehaviour
{
    private Touch _touch;
    private Vector2 _touchPos;
    private Ray2D _touchRaycast;
    [SerializeField]
    private GameObject _dashController;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(100, 100, 100, 0.5f);
        Gizmos.DrawSphere(_touchRaycast.origin, 5f);
    }

    
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchPos = Camera.main.ScreenToWorldPoint(_touch.position);
            _touchRaycast = new Ray2D(_touchPos, Vector2.down);
            if (_touch.phase == TouchPhase.Began)
            {
                if (Physics2D.OverlapCircle(_touchRaycast.origin, 25f))
                {
                    _dashController.transform.position = transform.position;
                    _dashController.gameObject.SetActive(true);
                }
            }

        }
    }
}
