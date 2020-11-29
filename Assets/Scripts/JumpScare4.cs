using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare4 : MonoBehaviour
{
    private Sounds sound;
    void Start()
    {
        sound = GetComponent<Sounds>();
    }
    public void Interact()
    {
        sound.Stop();
        sound.Sound1();
        gameObject.layer = 0;
    }
}
