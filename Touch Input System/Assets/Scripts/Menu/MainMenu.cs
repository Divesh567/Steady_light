using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using Menus.Animations;

public class MainMenu : Menu<MainMenu>
{

    [Space(20)]
    [Header("Buttons")]
    [SerializeField]
    private CustomButton playButton;
    [SerializeField]
    private CustomButton settingsButton;
    [SerializeField]
    private CustomButton rateButton;
    [SerializeField]
    private CustomButton creditsButton;
    [SerializeField]
    private CustomButton shopButton;

    [Header("Transition Sets")]
    [SerializeField]
    private MenuTransitionSet _levelSelectionTransitionSet;
    [SerializeField]
    private MenuTransitionSet _settingsTransitionSet;
    [SerializeField]
    private MenuTransitionSet _creditsTransitionSet;
    [SerializeField]
    private MenuTransitionSet _shopMenuTransitionSet;

    public override void Start()
    {
        _levelSelectionTransitionSet.InitTranistion(this, LevelSelectionMenu.Instance);
        _settingsTransitionSet.InitTranistion(this, SettingsMenu.Instance);
        _creditsTransitionSet.InitTranistion(this, CreditMenu.Instance);
        _shopMenuTransitionSet.InitTranistion(this, UpgradeMenu.Instance);


        playButton.button.onClick.AddListener(() => OnPlayPressed());
        settingsButton.button.onClick.AddListener(() => OnSettingsPressed());
        rateButton.button.onClick.AddListener(() => OnRateUsButtonPressed());
        creditsButton.button.onClick.AddListener(() => OnCreditsButtonPressed()); // Add UISfx pn button Pressed
        shopButton.button.onClick.AddListener(() => OnShopButtonPressed()); // Add UISfx pn button Pressed

        SoundManager.Instance.PlayMusic();
    }
    private void OnPlayPressed()
    {
        TutorialSwitch.TurorialOn = true;
        MenuManager._settingsMenuSwitch = false; //Code Smell

        _levelSelectionTransitionSet.PlayTransition();
    }

    public void OnSettingsPressed()
    {
        
        if (MenuManager.Instance != null && SettingsMenu.Instance != null)
        {
            _settingsTransitionSet.PlayTransition();
        }
    }

    public override void OnQuitPressed()
    {
        base.OnQuitPressed();
    }

    public override void MenuOpen()
    {
        Debug.Log("Open MainMenu");
        StartCoroutine(ResumeGame()); // Code smell
        MainPanel.gameObject.SetActive(true);
//        DisplayCompletionBar(DataManager.Instance.unlockedLevels);
    }

    public override void MenuClose()
    {
        StartCoroutine(ResumeGame());
        MainPanel.gameObject.SetActive(false);
    }

    public void OnShopButtonPressed()
    {
        _shopMenuTransitionSet.PlayTransition();
    }

    public void OnCreditsButtonPressed()
    {
        _creditsTransitionSet.PlayTransition();
    }

    public void OnRateUsButtonPressed()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Immersiveorama.LightForce");
    }

}
