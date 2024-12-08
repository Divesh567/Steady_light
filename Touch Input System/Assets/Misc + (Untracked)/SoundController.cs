using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private float _minSoundDist;
    [SerializeField]
    private float _avgSoundDist;
    [SerializeField]
    private float _maxSoundDist;

    [SerializeField]
    private float _minDistVolume;
    [SerializeField]
    private float _avgDistVolume;
    [SerializeField]
    private float _maxDistVolume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Vector2.Distance(Camera.main.transform.position, transform.position) >= _maxSoundDist)
        {
            _audioSource.volume = _maxDistVolume;
        }
        else if ((Vector2.Distance(Camera.main.transform.position, transform.position) >= _avgSoundDist))
        {
            _audioSource.volume = _avgDistVolume;
        }
        else if ((Vector2.Distance(Camera.main.transform.position, transform.position) >= _minSoundDist))
        {
            _audioSource.volume = _minDistVolume;
        }
    }
}
