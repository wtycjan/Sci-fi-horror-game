using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public bool jumpscare = true;
    public GameObject spider1;
    public GameObject spider2;
    private Sounds sound;

    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
    }
    public void Interact()
    {
        GameData.level1 = true;
        if(jumpscare)
        {
            spider1.SetActive(true);
            spider2.SetActive(true);
            sound.Sound3();
        }
        Destroy(gameObject);
    }

}
