using UnityEngine;
using UnityEngine.InputSystem;

public class TouchController : MonoBehaviour
{
    [Inject] [SerializeField] public BallCollisions _gameBall;
    private Vector2 _startTouchPos;
    private Touch _touch;
    private float _playerLerp = 15f;
    private Ray2D _touchRaycast;
    [SerializeField]
    private LayerMask layer;
    public bool _dragging;
    [SerializeField]
    private float resetSpeed = 2f;
    private Vector2 _touchPos;
    private ForceFieldController _forceFieldController;
    private float _racastSize = 7.5f;


    public bool _force = false;

    public PlayerInput playerInput;

    private void Awake()
    {
        _forceFieldController = transform.GetChild(0).GetComponent<ForceFieldController>();
        
    }

    private void Start()
    {
        _gameBall.GetPlayerObject(this);
    }

    private void LateUpdate()
    {
        if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;

        LookAtTheBall();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(100, 100, 100, 0.5f);
        Gizmos.DrawSphere(_touchRaycast.origin, 0.5f);
    }

    private void LookAtTheBall()
    {
        Vector2 _directionToFace = _gameBall.transform.position - transform.position;
        float angle = Mathf.Atan2(_directionToFace.y, _directionToFace.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));   
    }
    private Vector2 _touchStartScreenPos;
    private Vector2 _touchOffset;
    public void CheckShouldMove(bool isClicked)
    {
        /*  if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;

          _dragging = isClicked;*/


        if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;

        _dragging = isClicked;

        if (_dragging)
        {
            // On click start, capture offset
            _touchStartScreenPos = Touchscreen.current != null ? Touchscreen.current.primaryTouch.position.ReadValue() : Mouse.current.position.ReadValue();
            Vector2 worldTouchStart = Camera.main.ScreenToWorldPoint(_touchStartScreenPos);
            _touchOffset = (Vector2)transform.position - worldTouchStart;
        }
    }

    public void Movement(Vector2 movePos)
    {
        /* if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;

         if (!_dragging) return;

         _touchPos = Camera.main.ScreenToWorldPoint(movePos);

         transform.position = _touchPos;*/

        if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning) return;
        if (!_dragging) return;

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(movePos);
        Vector2 targetPos = worldPos + _touchOffset;

        transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * _playerLerp);
    }

    public void Reset()
    {
        transform.position = new Vector2(_gameBall.transform.position.x, _gameBall.transform.position.y - 10);
        _dragging = false;
    }
}
