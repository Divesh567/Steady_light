using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_SequenceHolder", menuName = "SO's/Sequence/SequenceHolder")]
public class SequenceHolder : ScriptableObject
{
    public List<SequenceStepBase> sequenceSteps;

    public void AddStep(SequenceStepBase sequenceStep, int index)
    {
        sequenceSteps.Add(sequenceStep);
    }
}


