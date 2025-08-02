using UnityEngine;

public class PingPongHorizonta : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 2f;
    public float duration = 2f;

    [Header("Rotation")]
    public bool doRotate = true;
    public float rotationSpeed = 180f;

    private float initialX;

    private void Start()
    {
        initialX = transform.position.x;
    }

    private void Update()
    {
        // Calculate offset along X only
        float t = Mathf.PingPong(Time.time / duration, 1f);
        float easedT = Mathf.SmoothStep(0f, 1f, t);
        float offsetX = Mathf.Lerp(-moveDistance / 2f, moveDistance / 2f, easedT);

        // Preserve Y and Z
        Vector3 pos = transform.position;
        pos.x = initialX + offsetX;
        transform.position = pos;

        // Optional rotation
        if (doRotate)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
