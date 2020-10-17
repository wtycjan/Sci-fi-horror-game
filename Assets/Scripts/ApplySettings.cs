using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class ApplySettings : MonoBehaviour
{
    public AudioMixer audioMusicMixer;
    public AudioMixer audioSoundsMixer;
    void Start()
    {
        audioMusicMixer.SetFloat("volume", PlayerPrefs.GetFloat("music-volume"));
        audioSoundsMixer.SetFloat("volume", PlayerPrefs.GetFloat("sound-volume"));
    }

}
