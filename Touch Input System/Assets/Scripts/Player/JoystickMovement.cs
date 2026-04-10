using UnityEngine;

public class JoyStickMovement : MonoBehaviour
{
    [Inject][SerializeField] private BallCollisions _gameBall;
    [SerializeField] private FloatingJoystick joystick;

    [Header("Movement")]
    [SerializeField] private float followSpeed = 12f;

    private Vector2 _inputDir;

    private void Start()
    {
       
    }

    private void LateUpdate()
    {
        /*if (MyGameManager.Instance.gameState != MyGameManager.GameState.GameRunning)
            return;*/

        ReadJoystick();
        MoveRelativeToBall();
        LookAtTheBall();
    }

    private void ReadJoystick()
    {
        _inputDir = joystick.Direction;

        if (_inputDir.sqrMagnitude > 1f)
            _inputDir.Normalize();
    }

    [SerializeField] private float minRadius = 2.5f; // strong push
    [SerializeField] private float maxRadius = 6f;   // weak push

    private void MoveRelativeToBall()
    {
        if (_inputDir == Vector2.zero)
            return;
        
        float magnitude = _inputDir.magnitude;

        if (magnitude < 0.01f)
            return;

        Vector2 dir = _inputDir.normalized;

        float radius = Mathf.Lerp(
            maxRadius,
            minRadius,
            magnitude
        );
        Vector3 targetPos =
            _gameBall.transform.position -
            (Vector3)(dir * radius);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * followSpeed
        );
    }

    private void LookAtTheBall()
    {
        Vector2 dir = _gameBall.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Reset()
    {
        transform.position =
            _gameBall.transform.position +
            Vector3.down * maxRadius;
    }
}