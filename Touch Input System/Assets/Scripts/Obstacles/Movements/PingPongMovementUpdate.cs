using UnityEngine;

public class PingPongMovementUpdate : MonoBehaviour , IMovementModule
{
    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public float duration = 2f;

    [Header("Rotation")]
    public bool doRotate = true;
    public float rotationSpeed = 180f; // degrees per second

    public void StartMovement()
    {
        
    }

    public void StopMovement()
    {
        
    }

    private void Update()
    {
        // Ping-pong value between 0 and 1
        float t = Mathf.PingPong(Time.time / duration, 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);

        // Optional rotation
        if (doRotate)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
