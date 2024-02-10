using UnityEngine;

public class LoseScreen : Menu<LoseScreen>
{
    private GameObject _backgroundImage;
    private GameObject _losePanel;
    private Animator _animator;

    private void Start()
    {
        _backgroundImage = transform.GetChild(0).gameObject;
        _losePanel = transform.GetChild(1).gameObject;
        _animator = GetComponent<Animator>();
    }
    public void OnRetryPressed()
    {
        MyGameManager.Instance._replaying = true;
        MenuClose();
        TutorialSwitch.TurorialOn = false;
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.ReloadLevel();
        }
    }

    public override void MenuOpen()
    {
        if (GameMenu.Instance != null && MenuManager.Instance != null)
        {
            MenuManager.Instance.CloseMenu(GameMenu.Instance);
        }
        _losePanel.gameObject.SetActive(true);
        _backgroundImage.gameObject.SetActive(true);
        _animator.SetTrigger("On");
        StartCoroutine(PauseGame());
    }

    public override void MenuClose()
    {
        StartCoroutine(ResumeGame());
        _losePanel.gameObject.SetActive(false);
        _backgroundImage.gameObject.SetActive(false);
        _animator.SetTrigger("Off");
    }
    public override void OnMainMenuButtonPressed()
    {
        MenuClose();
        base.OnMainMenuButtonPressed();
    }

    public void OnSkipLevelButtonPressed()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            MenuManager.Instance.OpenMenu(InternetCheck.Instance);
        }
        else
        {
            if (MyGameManager.Instance != null && AdManager.Instance != null)
            {
                MyGameManager.Instance._levelType = "sl";
                AdManager.Instance.ShowRewardedAd();
                MenuClose();
            }
        }
    }
}
