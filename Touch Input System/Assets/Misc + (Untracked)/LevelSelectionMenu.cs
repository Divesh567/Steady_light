using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Menus.Animations;

public class LevelSelectionMenu : Menu<LevelSelectionMenu>
{
    [Space(20)]
    [Header("Background")]
    [SerializeField]
    private Image _backGroundImage;

    [Space(10)]
    [Header("Navigation Buttons")]
    [SerializeField]
    private CustomButton _backButton;



    [Header("Transition Sets")]
    [SerializeField]
    private MenuTransitionSet _mainMenuTransitionSet;
    [SerializeField]
    private MenuTransitionSet _loadingScreenTransitionSet;



    public List<World> worlds; // Not to be used

    private void OnEnable()
    {
        LevelSelectionEvents.OnLevelSelectedEvent += OnLevelSelected;
    }

    private void OnDisable()
    {
        LevelSelectionEvents.OnLevelSelectedEvent -= OnLevelSelected;
    }

    public override void Start()
    {
        _mainMenuTransitionSet.InitTranistion(this, MainMenu.Instance);
        _loadingScreenTransitionSet.InitTranistion(this, LoadingScreen.Instance);
        _backButton.button.onClick.AddListener(() => OnBackButtonPressed());

        base.Start();
    }
    public override void MenuOpen()
    {
        base.MenuOpen();
        MainPanel.gameObject.SetActive(true);
    }


    public  void OnBackButtonPressed()
    {
        _mainMenuTransitionSet.PlayTransition();
    } 
    public override void MenuClose()
    {
        base.MenuClose();
        MainPanel.gameObject.SetActive(false);
    }

    private void OnLevelSelected()
    {
        _loadingScreenTransitionSet.PlayTransition();
    }
}
