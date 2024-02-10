using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    private DataManager _dataManager;

    private int _firstDiamondNo;
    private int _lastDiamondNo;

    private void Start()
    {
        _firstDiamondNo = transform.GetChild(0).GetComponent<Diamond>().GetDiamondNo();
        _lastDiamondNo = transform.GetChild(transform.childCount -1).GetComponent<Diamond>().GetDiamondNo();
        _dataManager = FindObjectOfType<DataManager>();
        CheckDiamonds();
    }
    public void CollectedDiamondNumber(int _dNo)
    {
        _dataManager.diamondData[_dNo] = true;
        MyGameManager.Instance.DiamondCollected();
        CheckDiamonds();
    }

    private void CheckDiamonds()
    {
        int t = 0;
        int c = 0;
        for (int i = _firstDiamondNo; i <= _lastDiamondNo; i++)
        {
            if (_dataManager.diamondData[i] == false)
            {
                transform.GetChild(t).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(t).gameObject.SetActive(false);
                c++;
            }
            t++;
        }
        SetDiamondUIValues(t , c);
    }

    private void SetDiamondUIValues(int total,int collected)
    {
        GameMenu.Instance.EnableDiamondUI(total++, collected++);
    }
}
