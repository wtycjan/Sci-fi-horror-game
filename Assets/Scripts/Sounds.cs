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
    public void SoundRandom(float y)
    {
        int x = Random.Range(1, 6);
        switch (x)
        {
            case 1:
                audioSource.PlayOneShot(sound1,y);
                break;
            case 2:
                audioSource.PlayOneShot(sound2, y);
                break;
            case 3:
                audioSource.PlayOneShot(sound3, y);
                break;
            case 4:
                audioSource.PlayOneShot(sound4, y);
                break;
            case 5:
                audioSource.PlayOneShot(sound5, y);
                break;
        }
        
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
}
