using UnityEngine;

[RequireComponent(typeof(FaceAnimation))]
public class ColDectFaces : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 1.5f;
    [SerializeField] private LayerMask detectionLayers;

    private FaceAnimation _faceAnimation;

    private void Awake()
    {
        _faceAnimation = GetComponent<FaceAnimation>();
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayers);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Spike"))
            {
                _faceAnimation.BallStateChange(BallState.Angry);
                return; // Prioritize this state
            }
            else if (hit.CompareTag("BoostField"))
            {
                _faceAnimation.BallStateChange(BallState.Smile);
                return;
            }
            else if (hit.CompareTag("Objective"))
            {
                _faceAnimation.BallStateChange(BallState.Smile);
                return;
            }
        }

        // If nothing nearby, revert to neutral
        _faceAnimation.BallStateChange(BallState.Normal);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}