using UnityEngine;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CheckpointAnimation : MonoBehaviour
{
    public CheckPointTextAnimValues animationValues;

    public TextMeshProUGUI textMesh;

    [Button("Play Animation")]
    public void  AnimateCheckPoint()
    {
        Debug.Log("Playing anim");
        textMesh.DOText(animationValues.endTextValue, animationValues.animDuration).SetEase(animationValues.animEase);
    }
}
