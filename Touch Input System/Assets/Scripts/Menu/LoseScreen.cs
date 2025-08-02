using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : Menu<LoseScreen>
{
    [SerializeField]
    public Button restartbutton;

    private void Start()
    {
        base.Start();

        restartbutton.onClick.AddListener(OnRetryPressed);


    }
    public void OnRetryPressed()
    {
        GameMenu.Instance.objectiveUIs.ForEach(x => x.ResetUI());

        AnalyticsEvent analyticsEvent = new AnalyticsEvent(EventName.LevelRetry)
                                                            .AddParam(ParamName.Level_Name, LevelLoader.Instance.GetCurrentSceneName());
        FirebaseAnalyticsController.LogEvent(analyticsEvent);

        SceneTransitionManager.Instance.OnSceneTransitionStarted.Invoke(LevelLoader.Instance.ReloadLevel);

    }

    private void ReStartGame()
    {
        MyGameManager.Instance.StateChanged(MyGameManager.GameState.GameRunning);
        SceneTransitionManager.Instance.OnSceneTransitionCompleted.RemoveListener(ReStartGame);
    }

    public override void MenuOpen()
    {
        base.MenuOpen();
        MainPanel.gameObject.SetActive(true);
    }

    public override void MenuClose()
    {
        base.MenuClose();
        MainPanel.gameObject.SetActive(false);
    }
    public void OnUpgradeButtonPressed()
    {
      
    }
}
