using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : Menu<ShopMenu>
{
    private GameObject _backGrounPanel;
    private GameObject _upgradePanels;
    private GameObject _powerupPanels;
    private GameObject _diamondsPanel;
    private TextMeshProUGUI _diamondsText;
    private Animator _animator;
    [SerializeField]
    private GameObject _maximumReached1;
    [SerializeField]
    private GameObject _maximumReached2;
    [SerializeField]
    private GameObject _maximumReached3;

    public bool _lifemax;
    public bool _timeMax;
    public bool _powerMax;
    private int _lifeCost = 20;
    private int _timeCost = 20;
    private int _powerCost = 20;

  
    private void Start()
    {
        FireBaseInit.Instance.LogEventOnFireBase("Shop_Open");
        _backGrounPanel = transform.GetChild(0).gameObject;
        _upgradePanels = transform.GetChild(0).gameObject.
                                transform.GetChild(0).gameObject.
                                    transform.GetChild(1).gameObject;
        _powerupPanels = transform.GetChild(0).gameObject.
                                transform.GetChild(1).gameObject.
                                    transform.GetChild(1).gameObject;
        _diamondsPanel = transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
        _animator = GetComponent<Animator>();
    }

    public override void MenuOpen()
    {
        MyGameManager.Instance.CheckUpgrades();
        _maximumReached1.gameObject.SetActive(_lifemax);
        _maximumReached2.gameObject.SetActive(_timeMax);
        _maximumReached3.gameObject.SetActive(_powerMax);
        _backGrounPanel.SetActive(true);
        _diamondsText = _diamondsPanel.transform.GetChild(0).gameObject.
                            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        _diamondsText.text = MyGameManager.Instance.GetDiamonds().ToString();
    }

    public override void MenuClose()
    {
        _backGrounPanel.SetActive(false);
        _upgradePanels.SetActive(true);
        _powerupPanels.SetActive(false);
    }
    
    public void OnPowerUpButtonPressed()
    {
        _upgradePanels.SetActive(false);
        _powerupPanels.SetActive(true);
        SoundManager.Instance.UiButtonPressed();
    }

    public void OnUpGradeButtonPressed()
    {
        _upgradePanels.SetActive(true);
        _powerupPanels.SetActive(false);
        SoundManager.Instance.UiButtonPressed();
    }

    public void OnBackButtonPressed()
    {
        MenuClose();
        MenuManager.Instance.OpenMenu(MainMenu.Instance);
        SoundManager.Instance.UiButtonPressed();
    }

    public void OnBuyLifeButtonPressed()
    {
        if (_lifeCost > MyGameManager.Instance._diamonds)
        {
            _animator.SetTrigger("Ned_Vf");
        }
        else
        {
            UpGradesManager.Instance.OnLifeBought();
            MyGameManager.Instance.OnAnUpgradeBought(_lifeCost);
            SoundManager.Instance.PlayUpgradeBought();
            OnAnItemBought();
            FireBaseInit.Instance.LogEventOnFireBase("Life_bought");
        }
    }

    public void OnBuyTimeButtonPressed()
    {
        if (_timeCost > MyGameManager.Instance._diamonds)
        {
            _animator.SetTrigger("Ned_Vf");
        }
        else
        {
            UpGradesManager.Instance.OnTimeBought();
            MyGameManager.Instance.OnAnUpgradeBought(_timeCost);
            SoundManager.Instance.PlayUpgradeBought();
            OnAnItemBought();
            FireBaseInit.Instance.LogEventOnFireBase("time_bought");
        }
    }

    public void OnButPowerupButtonPressed()
    {
        if (_timeCost > MyGameManager.Instance._diamonds)
        {
            _animator.SetTrigger("Ned_Vf");
        }
        else
        {
            UpGradesManager.Instance.OnPowerUpBought();
            MyGameManager.Instance.OnAnUpgradeBought(_powerCost);
            SoundManager.Instance.PlayUpgradeBought();
            OnAnItemBought();
            FireBaseInit.Instance.LogEventOnFireBase("power_bought");
        } 
    }

    private void OnAnItemBought()
    {
        _diamondsText.text = MyGameManager.Instance.GetDiamonds().ToString();
        _animator.SetTrigger("ItemBought_Vf");
    }

    public void SetUpgradeToMaxVf(int _index)
    {
        if(_index == 1)
        {
            _maximumReached1.gameObject.SetActive(true);
        }
        else if(_index == 2)
        {
            _maximumReached2.gameObject.SetActive(true);
        }
        else if(_index == 3)
        {
            _maximumReached3.gameObject.SetActive(true);
        }

    }


}
