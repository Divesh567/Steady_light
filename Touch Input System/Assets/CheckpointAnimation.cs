using UnityEngine;
using TMPro;
using DG.Tweening;

public class CheckpointAnimation : MonoBehaviour
{
    public CheckPointTextAnimValues animationValues;

    public TextMeshProUGUI textMesh;


    public void  AnimateCheckPoint()
    {
        textMesh.DOText(animationValues.endTextValue, animationValues.animDuration).SetEase(animationValues.animEase);
    }
}
