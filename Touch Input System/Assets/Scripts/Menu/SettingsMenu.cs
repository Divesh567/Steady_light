using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu<SettingsMenu>
{
  

    [Space(10)]
    [Header("Buttons")]
    [SerializeField]
    private CustomButton _musicButton;
    [SerializeField]
    private CustomButton _sfxButton;
    [SerializeField]
    private CustomButton backButton; // Add UISfx pn button Pressed

    public override void Start()
    {
        base.Start();

        _musicButton.button.onClick.AddListener(() => OnMusicPressed());
        _sfxButton.button.onClick.AddListener(() => OnSfxPressed());
        backButton.button.onClick.AddListener(() => MenuClose());
    }


    public override void MenuClose()
    {
        base.MenuClose();
        MainPanel.gameObject.SetActive(false);

        MainMenu.Instance.graphicRaycaster.enabled = true;
    }

    public override void MenuOpen()
    {
        base.MenuOpen();
        MainPanel.gameObject.SetActive(true);
    }

    private void OnSfxPressed()  //TODO - Add listner
    {
        if (SoundManager.Instance != null)
        {
            DataManager.Instance.isSfxMuted = !DataManager.Instance.isSfxMuted;
            DataManager.Instance.SaveData();
        }

    }

    private void OnMusicPressed() //TODO - Add listner
    {
        if (SoundManager.Instance != null)
        {
            DataManager.Instance.isMuiscMuted = !DataManager.Instance.isMuiscMuted;
            SoundManager.Instance.PlayMusic(); // TODO Use Event to update 
            DataManager.Instance.SaveData();
        }
    }

}
