using UnityEngine;

public class PlayTeleportAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio()
    {
        _audioSource.Play();
    }
}
