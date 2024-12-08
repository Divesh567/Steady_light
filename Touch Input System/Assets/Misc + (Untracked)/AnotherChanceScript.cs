using UnityEngine;

public class AnotherChanceScript : Menu<AnotherChanceScript>
{
    private GameObject _backGround;
    private GameObject _enduranceChancePanel;
    private GameObject _timeTrailChancePanel;
    private Animator _animator;
    public bool _timetrail = false;
    public bool _singleLife = false;

    private void Start()
    {
        _backGround = transform.GetChild(0).gameObject;
        _enduranceChancePanel = transform.GetChild(1).gameObject;
        _timeTrailChancePanel = transform.GetChild(2).gameObject;
        _animator = transform.GetComponent<Animator>();
    }

    public override void MenuOpen()
    {
        if (!_timetrail && !_singleLife)
        {
            _backGround.SetActive(true);
            _enduranceChancePanel.SetActive(true);
            _animator.SetTrigger("On");
        }
        else if (!_singleLife && _timetrail)
        {
            _backGround.SetActive(true);
            _timeTrailChancePanel.SetActive(true);
            _animator.SetTrigger("On");
            _timetrail = false;
        }
        else
        {
            MenuCloseBeforeWatchingAd();
        }

    }
    public void MenuCloseBeforeWatchingAd()
    {
        _backGround.SetActive(false);
        _animator.SetTrigger("Off");
        _enduranceChancePanel.SetActive(false);
        _timeTrailChancePanel.SetActive(false);
        if (LoseScreen.Instance != null && MenuManager.Instance != null && GameMenu.Instance != null)
        {
            MenuManager.Instance.OpenMenu(LoseScreen.Instance);
        }
    }

    public void OnCloseButtonPresed()
    {
        MenuCloseBeforeWatchingAd();
    }

    public void OnWatchADButtonPressed()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            MenuManager.Instance.OpenMenu(InternetCheck.Instance);
            return;
        }
        _animator.SetTrigger("Off");
        _backGround.SetActive(false);
        _enduranceChancePanel.SetActive(false);
        _timeTrailChancePanel.SetActive(false);
    }
}
