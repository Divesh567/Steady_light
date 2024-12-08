using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioControllerMono : MonoBehaviour
{
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip audioClip)
    {

        if (DataManager.Instance.saveDataSO.saveData.soundSettings.isSfxMuted) return;

        audioSource.clip = audioClip;
        audioSource.Play();
    }
    
}
