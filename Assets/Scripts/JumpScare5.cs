using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare5 : MonoBehaviour
{
    public GameObject spider1, spider2, spider3;
    private Sounds sound;
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spider1.SetActive(true);
            spider2.SetActive(true);
            spider3.SetActive(true);
            sound.Sound3();
            Destroy(gameObject);
        }

    }
}
