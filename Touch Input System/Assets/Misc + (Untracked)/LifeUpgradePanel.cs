using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpgradePanel : UpgradePanel
{
    protected override void OnUpgradeButtonPressed()
    {
        if (!CheckDiamonds(20)) return;

        DataManager.Instance.upgradeData.lifeUpgrade++;
    }
}
