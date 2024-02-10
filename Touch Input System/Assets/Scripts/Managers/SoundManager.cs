using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static bool MusicSfx = true;
    public static bool SoundSfx = true;

    //Cache Components
    [Header("Sfx Source")]
    [SerializeField]
    private AudioSource _audioSource;
    [Header("Music Source")]
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _obstacleSfxManager;
    [SerializeField]
    private AudioSource _boostFieldSfxManager;

    [SerializeField]
    private AudioClip _uiButtonPress;

    [SerializeField]
    private AudioClip _starCollected;
    [SerializeField]
    private float _starCollectedVolume;
    [SerializeField]
    private AudioClip _checkpointReached;
    [SerializeField]
    private float _checkpointReachedVolume;
    [SerializeField]
    private AudioClip _levelComplete;
    [SerializeField]
    private float _levelCompleteVolume;
    [SerializeField]
    private AudioClip _diamondCollected;
    [SerializeField]
    private float _diamondVolume;
    [SerializeField]
    private AudioClip _upgradeBoughtSfx;
    [SerializeField]
    private float _upgradeBoughtSfxVolume;

    [Header("The BoostField Sfx")]
    [SerializeField]
    private List<AudioClip> _boostFieldSfx;
    [SerializeField]
    private float _boostFieldSfxVolume;
    private int _boostFieldSfxToPlay;
    private float _boostFieldSfxResetTimer = 1f;

    [Header("Power Sfx")]
    [SerializeField]
    private AudioClip _magicSmokeSfx;
    [SerializeField]
    private float _magicSmokeSfxVolume;
    [SerializeField]
    private AudioClip _fireballSfx;
    [SerializeField]
    private float _fireballSfxVolume;
    [SerializeField]
    private AudioClip _electricBombSfx;
    [SerializeField]
    private float _electricBombSfxVolume;

    private float _timeTrailPitch = 1.15f;
    private float _starObjectivePitch = 1f;
    private float _enduranceObjectivePitch = 0.53f;

    public void PlayBallBounce(AudioClip _ballBounce, float _ballBounceVolume)
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_ballBounce, _ballBounceVolume);
        }
    }

    public void PlayBallDeath(AudioClip _ballDeath, float _ballDeathVolume)
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_ballDeath, _ballDeathVolume);
        }
    }

    public void PlayForceFieldDisabled(AudioClip _soundEffect, float _volume)
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_soundEffect, _volume);
            _audioSource.priority = 0;
        }
    }

    public void PlayForceFieldEnabled(AudioClip _soundEffect, float _volume)
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_soundEffect, _volume);
            _audioSource.priority = 0;
        }
    }

    public void PlayStarCollected()
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_starCollected, _starCollectedVolume);
        }
    }

    public void PlayCheckpointReached()
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_checkpointReached, _checkpointReachedVolume);
        }
    }

    public void PlayLevelComplete()
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_levelComplete, _levelCompleteVolume);
        }
    }

    public void PlayAndStopMusic()
    {
        if (MusicSfx)
        {
            _musicSource.Play();
        }
        else
        {
            _musicSource.Stop();
        }
    }

    public void PitchChangeTimeTrail()
    {
        _musicSource.pitch = _timeTrailPitch;
    }

    public void PitchChangeEnudrance()
    {
        _musicSource.pitch = _enduranceObjectivePitch;
    }

    public void PitchChangeStarObjective()
    {
        _musicSource.pitch = _starObjectivePitch;
    }

    public void PlayBoostFieldSfx()
    {
        StopCoroutine(BoostFieldSfxReset());
        _boostFieldSfxManager.GetComponent<AudioSource>().PlayOneShot
                                (_boostFieldSfx[_boostFieldSfxToPlay], _boostFieldSfxVolume);

        _boostFieldSfxToPlay++;
        if (_boostFieldSfxToPlay == _boostFieldSfx.Count)
        {
            _boostFieldSfxToPlay = 0;
        }
        StartCoroutine(BoostFieldSfxReset());
    }
    IEnumerator BoostFieldSfxReset()
    {
        yield return new WaitForSeconds(_boostFieldSfxResetTimer);
        _boostFieldSfxToPlay = 0;
    }

    public void PlayUiButtonSfx()
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_uiButtonPress);
        }
    }

    public void UiButtonPressed()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayUiButtonSfx();
        }
    }

    public void PlayPowerUp(int powerupNO)
    {
        switch (powerupNO)
        {
            case 0:
                _audioSource.PlayOneShot(_magicSmokeSfx, _magicSmokeSfxVolume);
                break;
            case 1:
                _audioSource.PlayOneShot(_fireballSfx, _fireballSfxVolume);
                break;
            case 2:
                _audioSource.PlayOneShot(_electricBombSfx, _electricBombSfxVolume);
                break;

        }

    }

    public void PlayDiamondCollected()
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_diamondCollected, _diamondVolume);
        }
    }

    public void PlayUpgradeBought()
    {
        if (SoundSfx)
        {
            _audioSource.PlayOneShot(_upgradeBoughtSfx, _upgradeBoughtSfxVolume);
        }
    }

}
