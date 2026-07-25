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

    [SerializeField] private Animator animator;
    
 

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

        UpdateProgressPercentage();
        animator.SetTrigger("CenterTrigger");
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

    public GameObject circleParent;
    public TextMeshProUGUI percentageText;
    private void UpdateProgressPercentage()
    {
        int totalLevels  = LevelLoader.Instance.levelHolder.GetAllActiveLevelCount();
        int completedLevels = LevelLoader.Instance.levelHolder.GetAllCompletedLevelCount();
        
        LogCore.Log(LogCat.Default, $"Completed {completedLevels} /{totalLevels} levels");
        
        float completedPercentage = completedLevels / (float)totalLevels * 100;
        
        circleParent.gameObject.SetActive(true);
        percentageText.text = $"{completedPercentage}%";
        
        

    }




}
