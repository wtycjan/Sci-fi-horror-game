using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    private AudioSource audioSource;
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
    public void Stop()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}
