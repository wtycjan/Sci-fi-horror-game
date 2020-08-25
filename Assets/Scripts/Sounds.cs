using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioClip sound5;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void Sound1()
    {
        audioSource.PlayOneShot(sound1);
    }
    public void Sound2()
    {
        audioSource.PlayOneShot(sound2);
    }
    public void Sound3()
    {
        audioSource.PlayOneShot(sound2);
    }
    public void Sound4()
    {
        audioSource.PlayOneShot(sound2);
    }
    public void Sound5()
    {
        audioSource.PlayOneShot(sound2);
    }
    public void Sound1Loop()
    {
        float x = Random.Range(0, 4f);
        audioSource.loop = true;
        audioSource.clip = sound1;
        audioSource.time = x;
        audioSource.Play();
    }
    public void Sound2Loop()
    {
        if (audioSource.clip == sound1)
            Stop();
        float x = Random.Range(0, 3f);
        audioSource.loop = true;
        audioSource.clip = sound2;
        audioSource.time = x;
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
    public void Volume(float x)
    {
        audioSource.volume = x;
    }
    public bool IsPlaying()
    {
        if (audioSource.isPlaying)
            return true;
        else
            return false;
    }
}
