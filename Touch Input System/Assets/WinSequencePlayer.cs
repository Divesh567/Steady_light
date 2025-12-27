using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEditor;

public class WinSequencePlayer : MonoBehaviour
{
    [Header("References")]
    [Inject] [SerializeField] private BallCollisions ball;
    [Inject] [SerializeField] private CinemachineVirtualCamera vCam;
    [Inject] [SerializeField] private Volume globalVolume;
    [Inject] [SerializeField] private ProgressAnimationController progressController;
    [Inject] [SerializeField] private DrawLine drawLine;

    [SerializeField] private GameObject fragmentPrefab;
    [SerializeField] private Material dissolveMaterial;
    [Header("Dissolve Settings")]
    [SerializeField] private float dissolveDuration = 1.5f;
    [SerializeField] private AnimationCurve dissolveCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Fragments")]
    [SerializeField] private int fragmentCount = 6;
    [SerializeField] private float fragmentScatterRadius = 1.5f;
    [SerializeField] private float fragmentFlyDuration = 1f;

    [Header("Camera Settings")]
    [SerializeField] private float cameraMoveDuration = 2f;

    [Header("Time/PP Settings")]
    [SerializeField] private float slowTimeScale = 0.5f;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float volumeTransitionDuration = 1f;

    private Transform originalFollow, originalLookAt;

    [SerializeField]
    private AudioControllerMono fragmentAudio;

#if UNITY_EDITOR
    [ContextMenu("Inject Dependencies")]
    private void InjectInEditor()
    {
        SimpleInjector.Inject(this, gameObject);
        EditorUtility.SetDirty(this); // mark dirty so changes persist
    }
#endif
    private void Awake()
    {
        if (vCam != null)
        {
            originalFollow = vCam.Follow;
            originalLookAt = vCam.LookAt;
        }
    }

  
    public async UniTask PlayWinSequence()
    {
        // Step 1. Slow time + PP effect + start dissolve
        await SlowTimeAndVolume();
      
        List<GameObject> fragments = await DissolveBallAndBurstFragments();
        vCam.Follow = null;
        vCam.LookAt = null;
        
        foreach (var frag in fragments)
        {
              
            // Start flying this fragment
            MoveFragment(frag, progressController.gameObject.transform.position).Forget();

            UniTask.Delay(100);


        }
        
        /*// Step 2 & 3. Move fragments one by one, trigger camera + progress
        bool cameraMoved = false;
        bool progressStarted = false;

        foreach (var frag in fragments)
        {
            // Start flying this fragment
            MoveFragment(frag, progressController.gameObject.transform.position).Forget();

            if (!cameraMoved)
            {
                cameraMoved = true;
                MoveCameraTo(progressController.gameObject.transform.position).Forget();
            }

            if (!progressStarted)
            {
                progressStarted = true;
                progressController.Execute().Forget();
            }

            // small stagger so they move one after another
            await UniTask.Delay(150);
        }*/
    }

    private async UniTask SlowTimeAndVolume()
    {
        float originalTimeScale = Time.timeScale;
        float originalWeight = globalVolume.weight;

        Time.timeScale = slowTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        float t = 0f;
        while (t < volumeTransitionDuration)
        {
            t += Time.unscaledDeltaTime;
            float lerpT = Mathf.Clamp01(t / volumeTransitionDuration);
            globalVolume.weight = Mathf.Lerp(originalWeight, 1f, lerpT);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        globalVolume.weight = 1f;

        await UniTask.Delay((int)(slowDuration * 1000), DelayType.UnscaledDeltaTime);

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = 0.02f;
    }

    private async UniTask<List<GameObject>> DissolveBallAndBurstFragments()
    {
        Renderer rend = ball.GetComponent<Renderer>();
        if (rend == null) return new List<GameObject>();

        Material matInstance = new Material(dissolveMaterial);
        rend.material = matInstance;

        List<GameObject> fragments = new List<GameObject>();
        List<Vector3> scatterPositions = new List<Vector3>();

        // Pre-spawn fragments at ball center
        for (int i = 0; i < fragmentCount; i++)
        {
            var frag = Instantiate(fragmentPrefab, ball.transform.position, Quaternion.identity);
            fragments.Add(frag);

            Vector3 scatterPos = ball.transform.position + Random.insideUnitSphere * fragmentScatterRadius;
            scatterPos.z = ball.transform.position.z;
            scatterPositions.Add(scatterPos);
        }

        // Dissolve & scatter together
        float time = 0f;
        while (time < dissolveDuration)
        {
            time += Time.deltaTime;
            float t = time / dissolveDuration;
            float curveVal = dissolveCurve.Evaluate(t);
            matInstance.SetFloat("_Cutoff", curveVal);

            for (int i = 0; i < fragments.Count; i++)
            {
                if (fragments[i] != null)
                {
                    fragments[i].transform.position = Vector3.Lerp(
                        ball.transform.position,
                        scatterPositions[i],
                        t
                    );
                }
            }

            await UniTask.Yield();
        }

        matInstance.SetFloat("_Cutoff", 1f);
        rend.enabled = false;

        return fragments;
    }

    bool progressStarted = false;
    private async UniTask MoveFragment(GameObject frag, Vector3 target)
    {
        Vector3 start = frag.transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fragmentFlyDuration;
            frag.transform.position = Vector3.Lerp(start, target, t);
            await UniTask.Yield();
        }
        
        if (!progressStarted)
        {
            progressStarted = true;
               
            progressController.Execute().Forget();
        }

        fragmentAudio.PlayAudioClip();
        Destroy(frag);
    }

    private async UniTask MoveCameraTo(Vector3 targetPosition)
    {
        return;
        
        if (vCam == null) return;

        vCam.Follow = null;
        vCam.LookAt = null;

        Vector3 startPos = vCam.transform.position;
        startPos.z = -10;
        targetPosition.z = -10;

        vCam.transform.DOKill();

        await vCam.transform.DOMove(targetPosition, cameraMoveDuration)
            .SetEase(Ease.OutBack)
            .AsyncWaitForCompletion();
    }
}
