using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private AudioSource _audioSource;
    private Renderer _renderer;

    [SerializeField]
    private float _isVisibleVolume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_renderer.isVisible && !DataManager.Instance.isSfxMuted)
        {
            _audioSource.volume = _isVisibleVolume;
        }
        else
        {
            _audioSource.volume = 0f;
        }
    }
}
