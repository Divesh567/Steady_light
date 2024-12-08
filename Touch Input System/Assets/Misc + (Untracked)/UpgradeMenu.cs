using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Menus.Animations;

public class UpgradeMenu : Menu<UpgradeMenu>
{
    public UpgradePanel lifeUpgradePanel;
    public UpgradePanel timeUpgradePanel;
    public UpgradePanel powerUpgradePanel;

    public CustomButton backButton;


    [Header("Transition Sets")]
    [SerializeField]
    private MenuTransitionSet _mainMenuTransitionSet;

    private void Start()
    {
        _mainMenuTransitionSet.InitTranistion(this, MainMenu.Instance);

        backButton.button.onClick.AddListener(OnBackButtonPressed);
    }


    public override void MenuOpen()
    {
        MainPanel.gameObject.SetActive(true);
    }

    public override void MenuClose()
    {
        MainPanel.gameObject.SetActive(false);
    }

    public void OnBackButtonPressed()
    {
        _mainMenuTransitionSet.PlayTransition();
    }
}
