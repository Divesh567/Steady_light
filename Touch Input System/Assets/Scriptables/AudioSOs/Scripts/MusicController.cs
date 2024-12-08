using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "New MusicController", menuName = "Audio/MusicController")]
public class MusicController : AudioControllerBaseSO
{
    public void PlayMusic(AudioSource source)
    {
        if (!IsAudioMute())
        {
            Debug.Log("Playing Music");
            source.Play();
            source.loop = true;
        }
        else
        {
            Debug.Log("Stopping Music");
            source.Stop();

        }
           
    }
}

