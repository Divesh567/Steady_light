using UnityEngine;

public class CreditMenu : Menu<CreditMenu>
{
    private GameObject _panels;

    private void Start()
    {
        _panels = transform.GetChild(0).gameObject;
    }

    public override void MenuClose()
    {
        _panels.gameObject.SetActive(false);
    }

    public override void MenuOpen()
    {
        _panels.gameObject.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        MenuClose();
    }
}
