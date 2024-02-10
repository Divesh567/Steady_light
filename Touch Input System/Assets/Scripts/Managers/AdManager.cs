using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour,IUnityAdsInitializationListener,IUnityAdsLoadListener,IUnityAdsShowListener
{
    private static AdManager _instance;
    public static AdManager Instance { get { return _instance; } }

    private bool _internAdLoaded;
    private bool _rewardAdLoaded;
    

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Advertisement.Initialize("5127135", false, this);
    }

    public void OnInitializationComplete()
    {
        LoadRewardAd();
        LoadInterAd();
    }

    public void LoadInterAd()
    {
        if (!_internAdLoaded)
        {
            Advertisement.Load("Interstitial_Android", this);
        }
    }

    public void LoadRewardAd()
    {
        if (!_rewardAdLoaded)
        {
            Advertisement.Load("Rewarded_Android", this);
        }
    }

    public void ShowInterAd()
    {
        FireBaseInit.Instance.LogEventOnFireBase("watch_interAd");
        Advertisement.Show("Interstitial_Android", this);
    }

    public void ShowRewardedAd()
    {
        FireBaseInit.Instance.LogEventOnFireBase("watch_rewardAd"); 
        Advertisement.Show("Rewarded_Android", this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
       if(placementId == "Interstitial_Android")
       {
            _internAdLoaded = true;
       }
       if(placementId == "Rewarded_Android")
       {
            _rewardAdLoaded = true;
       }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        //Debug.Log("Failed to load" + placementId);
        if (UnityAdsLoadError.INITIALIZE_FAILED.Equals(error))
        {
            //Debug.Log("Init Failed");
        }
        else if (UnityAdsLoadError.INVALID_ARGUMENT.Equals(error))
        {
           // Debug.Log("invalid Argument");
           //Debug.Log(message);
        }
        else if (UnityAdsLoadError.NO_FILL.Equals(error))
        {
           // Debug.Log("No Fill");
        }
        else if (UnityAdsLoadError.TIMEOUT.Equals(error))
        {
            //Debug.Log("T_O");
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        if (placementId == "Interstitial_Android")
        {
            _internAdLoaded = false;
            LoadInterAd();
            return;
        }

        if (placementId == "Rewarded_Android")
        {
            _rewardAdLoaded = false;
            LoadRewardAd();
            MyGameManager.Instance.RewardADWatch();
        }
        else
        {
            MenuManager.Instance.OpenMenu(LoseScreen.Instance);
        }
      
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
  
    }
}
