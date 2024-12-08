using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    private DataManager _dataManager;

    private int _firstDiamondNo;
    private int _lastDiamondNo;

    private List<Diamond> diamonds;


    

    private void Start()
    {
        _firstDiamondNo = transform.GetChild(0).GetComponent<Diamond>().GetDiamondNo();
        _lastDiamondNo = transform.GetChild(transform.childCount -1).GetComponent<Diamond>().GetDiamondNo();
        _dataManager = FindObjectOfType<DataManager>();
    }

    private void AssignDiamondIndex()
    {
        for(int i = 1; i < diamonds.Count; i++)
        {
            diamonds[i].diamondNo = i;
        }
    }
    public void CollectedDiamondNumber(int _dNo)
    {
        // Save Data;
    }
}
