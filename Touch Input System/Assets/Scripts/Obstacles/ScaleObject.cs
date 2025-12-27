using TMPro;
using UnityEngine;

public class ScaleObject : Trap
{
    private Vector3 originalScale;
    public override void Triggered()
    {
        transform.localScale = originalScale;
    }

    public override void SetTrap()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        _trigger._traps.Add(this);
    }
}