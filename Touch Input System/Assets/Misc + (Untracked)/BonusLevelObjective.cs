using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelObjective : MonoBehaviour
{
    [SerializeField]
    private float _time;
    private bool _timeOut = false;

    private void Start()
    {
        if (MyGameManager.Instance != null && GameMenu.Instance != null &&
                 AnotherChanceScript.Instance != null)
        {

           
        }
    }
    private void Update()
    {
       
    }
}
