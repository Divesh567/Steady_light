using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedWorldScreen : Menu<UnlockedWorldScreen>
{
    private int _screenToDisappear;

    public void EnableScreen(int _screenIndex)
    {
        _screenToDisappear = _screenIndex;
        transform.GetChild(_screenIndex).gameObject.SetActive(true);
        SoundManager.Instance.PlayUpgradeBought();
    }

    public void OnCloseScreenPressed()
    {
        transform.GetChild(_screenToDisappear).gameObject.SetActive(false);
    }
}
