using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private string _animTrigger;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(_animTrigger);
    }

    public void TriggerAnimationOnCall(string _animname)
    {

    }
}
