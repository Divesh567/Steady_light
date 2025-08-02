using UnityEditor.Timeline;
using UnityEngine;
using Cysharp.Threading.Tasks;
public abstract class SequenceStepBase : MonoBehaviour
{
    public float startWait;
    public float endWait;

    public int sequenceIndex;

    public SequenceHolder sequenceHolder;

    public virtual void Start()
    {
        sequenceHolder.AddStep(this, sequenceIndex);
    }

    public abstract UniTask Execute();
}
