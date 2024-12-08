using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private CustomButton upgradeButton;

    private void Start()
    {
        upgradeButton.button.onClick.AddListener(OnUpgradeButtonPressed);
    }

    protected abstract void OnUpgradeButtonPressed();

    protected virtual bool CheckDiamonds(int cost)
    {
        if (DataManager.Instance.resourcesData.totalDiamondsCollected < cost) return false;

        return true;
    }

}
