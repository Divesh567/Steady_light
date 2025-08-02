using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
public class PingPongMovement : MonoBehaviour, IMovementModule
{
    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public float duration = 2f;
    public Ease ease = Ease.InOutSine;

    [Header("Rotation")]
    public bool doRotate = true;
    public float rotationSpeed = 180f; // degrees per second
    private int rotationDirection = 1;

    private Tween moveTween;

    void Start()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        transform.position = pointA.position;

        moveTween = transform.DOMove(pointB.position, duration)
                             .SetEase(ease)
                             .SetLoops(-1, LoopType.Yoyo)
                             .OnStepComplete(ChangeRotationDirection);
    }

    public void StopMovement()
    {
        if (moveTween != null && moveTween.IsActive())
            moveTween.Kill();
    }

    void Update()
    {
        if (doRotate)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * rotationDirection * Time.deltaTime);
        }
    }

    public void ChangeRotationDirection()
    {
        rotationDirection *= -1;
    }
}
