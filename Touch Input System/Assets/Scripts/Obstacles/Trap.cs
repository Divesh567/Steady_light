using System;
using UnityEngine;
using UnityEngine.U2D;

public abstract class Trap : MonoBehaviour
{
    public Trigger _trigger;

    public void Start()
    {
        SetTrap();
    }

    public abstract void Triggered();

    public abstract void SetTrap();

}