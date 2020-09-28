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
    public AudioClip sound6;
    public AudioClip sound7;
    public AudioClip sound8;
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
        audioSource.PlayOneShot(sound3);
    }
    public void Sound4()
    {
        audioSource.PlayOneShot(sound4);
    }
    public void Sound5()
    {
        audioSource.PlayOneShot(sound5);
    }
    public void Sound6()
    {
        audioSource.PlayOneShot(sound6);
    }
    public void StartRunBreathSound()
    {
        audioSource.volume = .35f;
        audioSource.clip = sound5;
        audioSource.Play();
    }
    public void StopRunBreath()
    {
        audioSource.clip = sound6;
    }
    public void StartNormalBreath()
    {
        if(audioSource.clip != sound5 && !audioSource.isPlaying)
        {
            audioSource.volume = 0.17f;
            audioSource.loop = true;
            audioSource.clip = sound6;
            audioSource.Play();
        }

    }
    public void StopNormalBreath()
    {
        audioSource.Stop();
    }
    public void Sound7()
    {
        audioSource.PlayOneShot(sound7);
    }
    public void Sound8()
    {
        audioSource.PlayOneShot(sound8);
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
