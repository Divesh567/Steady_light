using UnityEngine;

public class PauseMenu : Menu<PauseMenu>
{
    [Space(20)]
    [Header("Buttons")]
    public CustomButton playButton;
    public CustomButton sfxButton;
    public CustomButton musicButton;

    public override void MenuClose()
    {
        base.MenuClose();
        MainPanel.gameObject.SetActive(false);

    }
    public override void MenuOpen()
    {
        base.MenuOpen();
        MainPanel.gameObject.SetActive(true);
    }

    private void Start()
    {
        base.Start();

        musicButton.button.onClick.AddListener(() => OnMusicPressed());
        sfxButton.button.onClick.AddListener(() => OnSfxPressed());
        playButton.button.onClick.AddListener(() => 
        {
            MenuClose();
            Time.timeScale = 1;

        });
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
