using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckPointAnimation", menuName = "Game Animation/Misc/CheckPointAnimation", order = 50)]
public class CheckPointTextAnimValues : ScriptableObject
{
    public float animDuration;
    public string startTextValue;
    public string endTextValue;

    public Ease animEase;

}
