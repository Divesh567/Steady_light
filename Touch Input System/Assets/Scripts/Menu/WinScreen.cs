using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class WinScreen : Menu<WinScreen>
{
    [Header("UI Elements")]
    [SerializeField]
    private CustomButton homeButton;
    [SerializeField]
    private CustomButton nextLevelButton;
    [SerializeField]
    private CustomButton upgradeButton;

    [SerializeField]
    private DOTweenAnimation startAnimation;




    public override void Start()
    {
        base.Start();

        homeButton.button.onClick.AddListener(OnHomeButtonPressed);
        nextLevelButton.button.onClick.AddListener(OnNextLevelButton);
        upgradeButton.button.onClick.AddListener(OnUpgradeButton);
    }

    public override void MenuOpen()
    {
        canvas.sortingOrder = 20;
        MainPanel.gameObject.SetActive(true);
        startAnimation.DOPlayAllById("WinAnim");
    }

    public override void MenuClose()
    {
        canvas.sortingOrder = 0;
        MainPanel.gameObject.SetActive(false);
    }


    private void OnHomeButtonPressed()
    {
        LevelLoader.Instance.LoadMainMenu();
    }

    private void OnNextLevelButton()
    {
        LevelLoader.Instance.LoadNextLevel();
    }
    private void OnUpgradeButton()
    {

    }



}
