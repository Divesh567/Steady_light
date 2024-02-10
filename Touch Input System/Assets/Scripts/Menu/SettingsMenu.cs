using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : Menu<SettingsMenu>
{
   
    private GameObject _panelChild;

    [SerializeField]
    private Sprite _musicOff;
    [SerializeField]
    private Sprite _muiscOn;
    [SerializeField]
    private Sprite _sfxOff;
    [SerializeField]
    private Sprite _sfxOn;

    [SerializeField]
    private Image _musicImage;
    [SerializeField]
    private Image _sfxImage;

    private void Start()
    {
        _panelChild = transform.GetChild(0).gameObject;
    }


    public override void MenuClose()
    {
        _panelChild.gameObject.SetActive(false);
    }

    public override void MenuOpen()
    {
        _panelChild.gameObject.SetActive(true);
        UpdateImages();
    }

    public void OnSfxPressed()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.SoundSfx = !SoundManager.SoundSfx;
            if (SoundManager.SoundSfx)
            {
                _sfxImage.sprite = _sfxOn;
            }
            else
            {
                _sfxImage.sprite = _sfxOff;
            }
        }
    }

    public void OnMusicPressed()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.MusicSfx = !SoundManager.MusicSfx;
            SoundManager.Instance.PlayAndStopMusic();
            if (SoundManager.MusicSfx)
            {
                _musicImage.sprite = _muiscOn;
            }
            else
            {
                _musicImage.sprite = _musicOff;
            }
        }
    }

    public void UpdateImages()
    {
        if (SoundManager.Instance != null)
        {
            if (SoundManager.SoundSfx)
            {
                _sfxImage.sprite = _sfxOn;
            }
            else
            {
                _sfxImage.sprite = _sfxOff;
            }

            if (SoundManager.MusicSfx)
            {
                _musicImage.sprite = _muiscOn;
            }
            else
            {
                _musicImage.sprite = _musicOff;
            }
        }
    }
}
