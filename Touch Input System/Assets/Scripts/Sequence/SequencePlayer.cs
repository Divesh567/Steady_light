using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
        // group steps by index
        var groupedSteps = sequenceHolder.sequenceSteps
            .Where(step => step != null)
            .GroupBy(step => step.sequenceIndex)
            .OrderBy(group => group.Key);

        foreach (var group in groupedSteps)
        {
            // run all steps in this group in parallel
            List<UniTask> tasks = new List<UniTask>();
            foreach (var step in group)
            {
                tasks.Add(step.Execute());
            }

            await UniTask.WhenAll(tasks);
        }
    }
}
