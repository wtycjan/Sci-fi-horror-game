using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare1 : MonoBehaviour
{
    public GameObject screen1, screen2, screen3;
    private Sounds sound;
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
    }

    private void OnTriggerEnter(Collider other)
    {
        screen1.SetActive(false);
        screen2.SetActive(false);
        screen3.SetActive(false);
        sound.Sound6();
        Destroy(gameObject);
    }
}
