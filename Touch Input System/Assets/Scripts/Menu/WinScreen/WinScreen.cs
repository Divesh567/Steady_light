using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class WinScreen : Menu<WinScreen>
{
    [Header("UI Elements")]
    [SerializeField]
    private CustomButton nextLevelButton;

    [SerializeField]
    private DOTweenAnimation startAnimation;

    public override void Start()
    {
        base.Start();
        nextLevelButton.button.onClick.AddListener(OnNextLevelButton);
    }

    public override void MenuOpen()
    {
        base.MenuOpen();
        MainPanel.gameObject.SetActive(true);
        nextLevelButton.button.interactable = true;


        startAnimation.DOPlayAllById("WinAnim");
    }

    public override void MenuClose()
    {
        base.MenuClose();
        MainPanel.gameObject.SetActive(false);
        startAnimation.DORewindAllById("WinAnim");
    }


    private void OnHomeButtonPressed()
    {
        LevelLoader.Instance.LoadMainMenu();
    }

    private void OnNextLevelButton()
    {
        nextLevelButton.button.interactable = false;

        
        SceneTransitionManager.Instance.OnSceneTransitionStarted.Invoke( LevelLoader.Instance.LoadNextLevel );
        
    }
    private void OnUpgradeButton()
    {

    }



}
