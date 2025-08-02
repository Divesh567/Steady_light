using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;

public class SequencePlayer : MonoBehaviour
{
    public SequenceHolder sequenceHolder;

    public static SequencePlayer Instance;

    public void Awake()
    {
        Instance = this;
    }

    public async UniTask PlaySequenceAsync()
    {
        var orderedSteps = sequenceHolder.sequenceSteps
        .Where(step => step != null)
        .OrderBy(step => step.sequenceIndex)
        .ToList();

        foreach (var step in orderedSteps)
        {
            await step.Execute();
        }
    }
}
