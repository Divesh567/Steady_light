using Cinemachine;
using UnityEngine;

public class InjectTargetGroupElements : MonoBehaviour
{
    [Inject] [SerializeField] private BallCollisions ballCollisions;
    [Inject] [SerializeField] private TouchController touchController;

    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    public void Start()
    {
        cinemachineTargetGroup.AddMember(ballCollisions.transform, 1.5f, 2);
        cinemachineTargetGroup.AddMember(touchController.transform, 1f, 2);
    }
}
