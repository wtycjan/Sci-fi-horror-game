using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare3 : MonoBehaviour
{
    public GameObject spider1;
    public GameObject spider2;
    private Sounds sound;
    bool jumpscare = false;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
    }
    public void Interact()
    {
        if (!jumpscare)
        {
            spider1.SetActive(true);
            spider2.SetActive(true);
            sound.Sound3();
            jumpscare = true;
        }
    }
}
