using UnityEngine;

public abstract class AudioControllerBaseSO : ScriptableObject
{
    public enum SoundType
    {
        sfx,
        music
    }
    public SoundType soundType;

    public virtual bool IsAudioMute()

    {   if(soundType == SoundType.sfx)
        {
            return DataManager.Instance.isSfxMuted;
        }
        else
        {
            return DataManager.Instance.isMuiscMuted;
        }
    }
    public virtual void PlayAudio(AudioSource source, AudioClip clip)
    {
        if (!IsAudioMute())
        {
            source.clip = clip;
            source.Play();
        }
    }
}
