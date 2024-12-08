using UnityEngine;

public class CreditMenu : Menu<CreditMenu>
{

    public override void Start()
    {
        base.Start();
    }

    public override void MenuClose()
    {
        MainPanel.gameObject.SetActive(false);
    }

    public override void MenuOpen()
    {
        MainPanel.gameObject.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        MenuClose();

        MainMenu.Instance.graphicRaycaster.enabled = true;
    }
}
