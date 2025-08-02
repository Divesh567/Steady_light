using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class CameraMover : SequenceStepBase
{
    [SerializeField] private float moveDuration = 2f;

    public CinemachineVirtualCamera vCam;
    private Transform originalFollow;
    private Transform originalLookAt;

    public Transform targetPos;


    private void Awake()
    {
        if (vCam != null)
        {
            originalFollow = vCam.Follow;
            originalLookAt = vCam.LookAt;
        }
    }
    public override void Start()
    {
        base.Start();
        targetPos = FindObjectOfType<ProgressAnimationController>().transform; // To be refactored
       
    }

    public void DetachCinemachine()
    {
        if (vCam != null)
        {
            vCam.Follow = null;
            vCam.LookAt = null;
        }
    }

    public void RestoreCinemachine()
    {
        if (vCam != null)
        {
            vCam.Follow = originalFollow;
            vCam.LookAt = originalLookAt;
        }
    }

    public async UniTask MoveToPosition(Vector3 targetPosition)
    {
        DetachCinemachine();

        var startPosition = vCam.transform.position;

        // Clamp the z-position
        startPosition.z = -10;
        targetPosition.z = -10;

        // Cancel any ongoing tween if needed
        vCam.transform.DOKill();

        await vCam.transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.OutBack)
            .AsyncWaitForCompletion(); // Await the tween to complete
    }

    public override async UniTask Execute()
    {
        FindObjectOfType<ProgressAnimationController>().SetProgressSprite(); // To be refactored

        await MoveToPosition(targetPos.position);
    }
}

