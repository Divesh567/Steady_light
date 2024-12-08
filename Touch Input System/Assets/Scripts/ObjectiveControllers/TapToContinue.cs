using UnityEngine;

public class TapToContinue : MonoBehaviour
{
    private GameObject _ball;
    private float _ballGravity;
    private void Start()
    {
        _ball = FindObjectOfType<BallCollisions>().gameObject;
        _ballGravity = _ball.GetComponent<Rigidbody2D>().gravityScale;
        _ball.GetComponent<Rigidbody2D>().gravityScale = 0f;
        _ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
    public void OnTapToContinuePressed()
    {
        _ball.GetComponent<Rigidbody2D>().gravityScale = _ballGravity;
       /* if (GameMenu.Instance != null)
        {
            GameMenu.Instance.GamePanelOpen();
        }*/
        Destroy(gameObject);
    }
}
