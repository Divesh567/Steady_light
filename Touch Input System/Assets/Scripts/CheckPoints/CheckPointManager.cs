using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public CheckPoint initalCheckPoint;
    public List<CheckPoint> allCheckPoints =  new List<CheckPoint>();
    public int currentCheckPoint = 0;


    private void Start()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            if (i == 0) initalCheckPoint = transform.GetChild(i).GetComponent<CheckPoint>();

            allCheckPoints.Add(transform.GetChild(i).GetComponent<CheckPoint>());
        }
    }

}
