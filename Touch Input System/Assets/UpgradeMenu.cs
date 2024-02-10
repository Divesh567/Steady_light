using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

 public class UpgradeMenu : Menu<UpgradeMenu>
{
    private GameObject _backGrounPanel1;
    private GameObject _backGroundPanel2;
    private GameObject _selectUpgradeText;
    private GameObject _lifeButton;
    private GameObject _timeButton;
    private GameObject _powerButton;
    private GameObject _diamondPanel;

    [SerializeField]
    private GameObject _lifePanel;
    [SerializeField]
    private GameObject _timePanel;
    [SerializeField]
    private GameObject _powerPanel;
    [SerializeField]
    private TextMeshProUGUI _diamondsText;
    private Animator _animator;

    [SerializeField]
    private GameObject _lifeMaxed;
    [SerializeField]
    private GameObject _timeMaxed;
    [SerializeField]
    private GameObject _powerMaxed;
    [SerializeField]
    private GameObject _lifeBuyButton;
    [SerializeField]
    private GameObject _timeBuyButton;
    [SerializeField]
    private GameObject _powerBuyButton;

    private bool _lifeMax;
    private bool _timeMax;
    private bool _powerMax;

    private int lifes;
    private float _time;
    private int powers;

    private int _lifeCost = 15;
    private int _timeCost = 15;
    private int _powerCost = 15;

    public bool _isPreviousScreenMainMenu = true;

    private void Start()
    {
        _backGrounPanel1 = transform.GetChild(0).gameObject;
        _backGroundPanel2 = transform.GetChild(1).gameObject;
        _selectUpgradeText = transform.GetChild(2).gameObject;
        _lifeButton = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        _timeButton = transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        _powerButton = transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
        _diamondPanel = transform.GetChild(3).gameObject;
        _animator = GetComponent<Animator>();
    }

    public override void MenuOpen()
    {
        UpdateImages();
        _backGrounPanel1.SetActive(true);
        _backGroundPanel2.SetActive(true);
        _selectUpgradeText.SetActive(true);
        _diamondPanel.SetActive(true);
        _diamondsText.text = MyGameManager.Instance.GetDiamonds().ToString();
    }
    public override void MenuClose()
    {
        _backGrounPanel1.SetActive(false);
        _selectUpgradeText.SetActive(false);
        _diamondPanel.SetActive(false);
        for(int i = 0; i<= 2; i++)
        {
            _backGroundPanel2.transform.GetChild(i).gameObject.SetActive(false);
        }
        _backGroundPanel2.SetActive(false);
    }

    public void OnLifeButtonPressed()
    {
        _selectUpgradeText.SetActive(false);
        _timePanel.SetActive(false);
        _powerPanel.SetActive(false);
        _lifePanel.SetActive(true);
        SoundManager.Instance.UiButtonPressed();
    }

    public void OnTimeButtonPressed()
    {
        _selectUpgradeText.SetActive(false);
        _powerPanel.SetActive(false);
        _lifePanel.SetActive(false);
        _timePanel.SetActive(true);
        SoundManager.Instance.UiButtonPressed();
    }

    public void OnPowerUpButtonPressed()
    {
        _selectUpgradeText.SetActive(false);
        _timePanel.SetActive(false);
        _lifePanel.SetActive(false);
        _powerPanel.SetActive(true);
        SoundManager.Instance.UiButtonPressed();
    }

    public void SetUpgrades(int lifeBought, float timeBought, int powersBought)
    {
        lifes = lifeBought;
        _time = timeBought;
        powers = powersBought;
    }
    public void UpdateImages()
    {
        for(int  i = 0; i < lifes ; i++)
        {
            _lifeButton.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            if (lifes == 3)
            {
                _lifeMax = true;
            }

        }
        for (int p = 0; p < powers; p++)
        {
            _powerButton.transform.GetChild(p).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            if(powers == 3)
            {
                _powerMax = true;
            }
           
        }
        if(_time == 4)
        {
            _timeButton.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else if(_time == 8)
        {
            _timeButton.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            _timeButton.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else if(_time == 12)
        {
            _timeButton.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            _timeButton.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            _timeButton.transform.GetChild(2).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            _timeMax = true;

        }
        else
        {
            //nothing
        }
        PowerMaxedVf(_lifeMax, _timeMax, _powerMax);

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
            UpdateImages();
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
            UpdateImages();
        }
    }

    public void OnBuyPowerupButtonPressed()
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
            UpdateImages();
        }
    }

    private void OnAnItemBought()
    {
        _diamondsText.text = MyGameManager.Instance.GetDiamonds().ToString();
        _animator.SetTrigger("ItemBought_Vf");
    }

    public void OnBackButtonPressed()
    {
        if (_isPreviousScreenMainMenu)
        {
            MenuClose();
            MenuManager.Instance.OpenMenu(MainMenu.Instance);
            SoundManager.Instance.UiButtonPressed();
        }
        else
        {
            MenuClose();
            SoundManager.Instance.UiButtonPressed();
        }
    }

    public void PowerMaxedVf(bool life, bool time, bool power)
    {
        if (life)
        {
            _lifeMaxed.SetActive(true);
            _lifeBuyButton.SetActive(false);
        }
        if (time)
        {
            _timeMaxed.SetActive(true);
            _timeBuyButton.SetActive(false);
        }
        if (power)
        {
            _powerMaxed.SetActive(true);
            _timeBuyButton.SetActive(false);
        }

    }
}
