using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WinScreen : Menu<WinScreen>
{
    private GameObject _backgroundImage;
    private GameObject _winPanel;
    private Animator _animator;
    private Slider _completionBar;
    private int _completedLevels;

    private void Start()
    {
        _backgroundImage = transform.GetChild(0).gameObject;
        _winPanel = transform.GetChild(1).gameObject;
        _animator = GetComponent<Animator>();
    }
    public void OnNextLevelPressed()
    {
        TutorialSwitch.TurorialOn = true;
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.LoadNextLevel(); 
        }
        MenuClose();
    }

    public override void MenuOpen()
    {
        DisplayCompletionBar(DataManager.Instance.unlockedLevels);
        _animator.SetTrigger("On");
        _winPanel.gameObject.SetActive(true);
        _backgroundImage.gameObject.SetActive(true);
        if (GameMenu.Instance != null)
        {
            MenuManager.Instance.CloseMenu(GameMenu.Instance);
        }
        if (MyGameManager.Instance._diamonds >= 15)
        {
            _animator.SetTrigger("Upgrades");
        }
    }
    public void OnReplayButonPressed()
    {
        MyGameManager.Instance._replaying = true;
        TutorialSwitch.TurorialOn = true;
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.ReloadLevel();
        }
        MenuClose();
    }

    public void OnUpgradeButtonPresed()
    {
        UpgradeMenu.Instance._isPreviousScreenMainMenu = false;
        MenuManager.Instance.OpenMenu(UpgradeMenu.Instance);
    }

    public override void MenuClose()
    {
        StartCoroutine(ResumeGame());
        _animator.SetTrigger("Off");
        _winPanel.gameObject.SetActive(false);
        _backgroundImage.gameObject.SetActive(false);
    }
    public override void OnMainMenuButtonPressed()
    {
        MenuClose();
        base.OnMainMenuButtonPressed();
    }

    public void DisplayCompletionBar(bool[] _levelData)
    {
        float _totalLevels;
        _completedLevels = 0;
        _totalLevels = _levelData.Length;

        for (int i = 0; i < _levelData.Length - 1; i++)
        {
            if (_levelData[i] == true)
            {
                _completedLevels++;
            }
        }
        _completionBar = transform.GetChild(1).transform.GetChild(4).GetComponent<Slider>();
        float _gameCompleted = (_completedLevels / _totalLevels) * 100f;
        _gameCompleted = Mathf.Round(_gameCompleted * 100f) / 100f;
        StartCoroutine(FillCompletionBar(0f, _gameCompleted));
       
    }

    IEnumerator FillCompletionBar(float currentValue, float targetvalue)
    {
        var completionText = _completionBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        float elaspedTime = 0.0f;
        while (elaspedTime < 2f)
        {
            var _barvalue = (float)Mathf.Lerp(currentValue, targetvalue, elaspedTime / 1);
            elaspedTime += Time.deltaTime;
            _completionBar.value = _barvalue;
            completionText.text = Mathf.Round((_completionBar.value * 100f) / 100f).ToString() + "%";
            yield return null;
        }
        _completionBar.value = targetvalue;
    }
}
