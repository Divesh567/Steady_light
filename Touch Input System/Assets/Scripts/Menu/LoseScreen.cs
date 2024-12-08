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
        LevelLoader.Instance.ReloadLevel();
    }

    public override void MenuOpen()
    {
        canvas.sortingOrder = 20;
        MainPanel.gameObject.SetActive(true);
    }

    public override void MenuClose()
    {
        canvas.sortingOrder = 0;
        MainPanel.gameObject.SetActive(false);
    }
    public void OnUpgradeButtonPressed()
    {
      
    }
}
