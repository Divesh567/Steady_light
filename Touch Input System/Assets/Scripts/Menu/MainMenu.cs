using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : Menu<MainMenu>
{
    private GameObject _menuPanel;
    private Slider _completionBar;

    private int _completedLevels;

    private void Start()
    {
        _menuPanel = transform.GetChild(0).gameObject;
    }
    public void OnPlayPressed()
    {
        TutorialSwitch.TurorialOn = true;
        MenuManager._settingsMenuSwitch = false;
        if (MenuManager.Instance != null && SettingsMenu.Instance != null && LevelSelectorMenu.Instance != null)
        {
            MenuManager.Instance.CloseMenu(SettingsMenu.Instance);
            MenuManager.Instance.CloseMenu(CreditMenu.Instance);
            MenuManager.Instance.OpenMenu(LevelSelectorMenu.Instance);
            UpGradesManager.Instance.LoadAllUpGrades();
            MenuClose();
        }
    }

    public void OnSettingsPressed()
    {
        
        if (MenuManager.Instance != null && SettingsMenu.Instance != null)
        {
            MenuManager.Instance.OpenMenu(SettingsMenu.Instance);
        }
    }

    public override void OnQuitPressed()
    {
        base.OnQuitPressed();
    }

    public override void MenuOpen()
    {
        StartCoroutine(ResumeGame());
        _menuPanel.gameObject.SetActive(true);
        _completionBar.gameObject.SetActive(true);
        DisplayCompletionBar(DataManager.Instance.unlockedLevels);
    }

    public override void MenuClose()
    {
        StartCoroutine(ResumeGame());
        _menuPanel.gameObject.SetActive(false);
        _completionBar.gameObject.SetActive(false);
    }

    public void OnShopButtonPressed()
    {
        MenuClose();
        UpgradeMenu.Instance._isPreviousScreenMainMenu = true;
        MenuManager.Instance.OpenMenu(UpgradeMenu.Instance);
    }

    public void OnCreditsButtonPressed()
    {
        MenuManager.Instance.OpenMenu(CreditMenu.Instance);
    }

    public void OnFeedbackButtonPressed()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Immersiveorama.LightForce");
    }

    public void DisplayCompletionBar(bool[] _levelData)
    {
        float _totalLevels;
        _completedLevels = 0;
        _totalLevels = _levelData.Length;

        for(int i = 0; i < _levelData.Length - 1; i++)
        {
            if(_levelData[i] == true)
            {
                _completedLevels++;
            }
        }

        _completionBar = transform.GetChild(1).gameObject.GetComponent<Slider>();
        float _gameCompleted = (_completedLevels / _totalLevels) * 100f;
        _gameCompleted = Mathf.Round(_gameCompleted * 100f) / 100f;
        StartCoroutine(FillCompletionBar(0f,_gameCompleted));
    }

    IEnumerator FillCompletionBar(float currentValue, float targetvalue)
    {
        var completionText = _completionBar.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        float elaspedTime = 0.0f;
        while(elaspedTime < 2f)
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
