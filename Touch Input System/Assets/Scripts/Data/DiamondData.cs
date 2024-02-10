using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiamondData",menuName = "NewDiamondData",order = 52)]
public class DiamondData : ScriptableObject
{
    public List<bool> CollectedDiamondList;

    public void ChangeElementBool(int _no)
    {
        CollectedDiamondList[_no] = true;
    }
}
