using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "WinSequence/BallDissolveStep")]
public class BallDissolveStep : SequenceStepBase
{
    [Header("Dissolve")]
    [SerializeField] private float dissolveDuration = 1.5f;
    [SerializeField] private AnimationCurve dissolveCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Fragments")]
    [SerializeField] private GameObject fragmentPrefab;
    [SerializeField] private int fragmentCount = 10;
    [SerializeField] private float fragmentScatterRadius = 1.5f;
    [SerializeField] private float fragmentFlyDuration = 1f;

    [Header("References")]
    [SerializeField] private Transform progressTarget; // assign glow sequence target
    [SerializeField] private Material dissolveMaterial; // instanced material for ball

    [SerializeField] GameObject ball;

    public override async UniTask Execute()
    {
        progressTarget = FindAnyObjectByType<ProgressAnimationController>().transform;

        Renderer rend = ball.GetComponent<Renderer>();
        if (rend == null) return;

        // ensure unique instance of material
        Material matInstance = new Material(dissolveMaterial);
        rend.material = matInstance;

        // 1. Dissolve animation
        float time = 0f;
        while (time < dissolveDuration)
        {
            time += Time.deltaTime;
            float t = time / dissolveDuration;
            float curveVal = dissolveCurve.Evaluate(t);
            matInstance.SetFloat("_Cutoff", curveVal); // assumes shader param
            await UniTask.Yield();
        }
        matInstance.SetFloat("_Cutoff", 1f);

        // hide ball at the end
        ball.SetActive(false);

        // 2. Spawn fragments
        List<GameObject> fragments = new List<GameObject>();
        for (int i = 0; i < fragmentCount; i++)
        {
            Vector3 scatterPos = ball.transform.position + Random.insideUnitSphere * fragmentScatterRadius;
            scatterPos.z = ball.transform.position.z; // lock Z if 2D
            var frag = Object.Instantiate(fragmentPrefab, scatterPos, Quaternion.identity);
            fragments.Add(frag);
        }

        // 3. Animate fragments to progressTarget
        List<UniTask> flyTasks = new List<UniTask>();
        foreach (var frag in fragments)
        {
            flyTasks.Add(MoveFragment(frag, progressTarget.position));
        }
    }

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
    }
}
