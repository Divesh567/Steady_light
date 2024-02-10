using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetCheck : Menu<InternetCheck>
{
    private GameObject _mainpanel;
    private void Start()
    {
        _mainpanel = transform.GetChild(0).gameObject;
    }
    public override void MenuOpen()
    {
        _mainpanel.SetActive(true);
    }

    public override void MenuClose()
    {
        _mainpanel.SetActive(false);
    }

    public void OnRetryButtonPressed()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            GetComponent<Animator>().SetTrigger("NoWifi");
        }
        else
        {
            AdManager.Instance.LoadInterAd();
            AdManager.Instance.LoadRewardAd();
            MenuClose();
        }   
    }

    public void OnNeverMindButtonPressed()
    {
        MenuClose();
    }





}
