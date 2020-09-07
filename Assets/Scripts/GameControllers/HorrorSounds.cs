using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorSounds : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    private AudioSource audioSource;
    private Sounds sounds;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sounds = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        StartCoroutine("Horror");
    }

    private IEnumerator Horror()
    {
        int x = Random.Range(0, 15);
        yield return new WaitForSeconds(40+x);
        if (!sounds.IsPlaying())
            RandomSound();
        StartCoroutine("Horror");
    }
    public void RandomSound()
    {
        int x = Random.Range(0, 4);
        switch (x)
        {
            case 0:
                audioSource.PlayOneShot(sound1);
                break;
            case 1:
                audioSource.PlayOneShot(sound2);
                break;
            case 2:
                audioSource.PlayOneShot(sound3);
                break;
            case 3:
                audioSource.PlayOneShot(sound4);
                break;

        }
    }
}
