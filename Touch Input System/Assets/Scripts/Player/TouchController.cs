using UnityEngine;

public class TouchController : MonoBehaviour
{
    public GameObject _gameBall;
    private Vector2 _startTouchPos;
    private Touch _touch;
    private float _playerLerp = 15f;
    private Ray2D _touchRaycast;
    [SerializeField]
    private LayerMask layer;
    private bool _dragging = false;
    [SerializeField]
    //private float resetSpeed = 2f;
    private Vector2 _touchPos;
    private ForceFieldController _forceFieldController;
    private float _racastSize = 7.5f;


    public bool _force = false;

    private void Start()
    {
        _forceFieldController = transform.GetChild(0).GetComponent<ForceFieldController>();
        _gameBall.GetComponent<BallCollisions>().GetPlayerObject(gameObject.GetComponent<TouchController>());
    }

    private void Update()
    {
        if (MyGameManager.GameStarted)
        {
            LookAtTheBall();
            Movement();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(100, 100, 100, 0.5f);
        Gizmos.DrawSphere(_touchRaycast.origin, 0.5f);
    }

    private void LookAtTheBall()
    {
        if (!_force)
        {
            Vector2 _directionToFace = _gameBall.transform.position - transform.position;
            float angle = Mathf.Atan2(_directionToFace.y, _directionToFace.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {

            _touch = Input.GetTouch(0);
            _touchPos = Camera.main.ScreenToWorldPoint(_touch.position);
            _touchRaycast = new Ray2D(_touchPos, Vector2.zero);


            if (_touch.phase == TouchPhase.Began)
            {
                if (Physics2D.OverlapCircle(_touchRaycast.origin, _racastSize))
                {

                    Vector2 _startpos = new Vector2(_touchPos.x - Camera.main.transform.position.x,
                                                 _touchPos.y -
                                                 Camera.main.transform.position.y).normalized;

                    transform.position = new Vector2(Camera.main.transform.position.x + (_startpos.x * 12f),
                                                                Camera.main.transform.position.y + (_startpos.y * 12f));
                    _startTouchPos = new Vector2(_touchPos.x - transform.position.x,
                                                                _touchPos.y - transform.position.y);

                    if (!_dragging)
                    {
                        _dragging = true;
                    }
                   
                }
            }
            else if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            {
                if (_dragging)
                {
                    _dragging = false;
                }
                
            }
        }

        if (_dragging)
        {
            transform.position = Vector3.Lerp
                (transform.position, new Vector2(_touchPos.x - _startTouchPos.x,
                                                              _touchPos.y - _startTouchPos.y), _playerLerp * Time.deltaTime);
        }
        else
        {
            /*transform.position = Vector3.Lerp(transform.position,
                new Vector2(_gameBall.transform.position.x, _gameBall.transform.position.y - 12),
                resetSpeed * Time.deltaTime);*/
        }
    }

    public void Reset()
    {
        transform.position = new Vector2(_gameBall.transform.position.x, _gameBall.transform.position.y - 10);
        _dragging = false;
    }
}
