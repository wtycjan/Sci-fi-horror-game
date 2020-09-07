using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpicking : MonoBehaviour
{
    public Image codeScreen;
    public Material openMaterial;
    public int securityLvl = 1;
    public bool isAlarm = false;
    public AudioSource alarm;

    public void Interact()
    {
        Open();
    }
    void Update()
    {
        //if unlocked
        if(GameData.door1)
        {
            GetComponentInChildren<Light>().color = Color.green;
            GetComponent<MeshRenderer>().material = openMaterial;
            gameObject.layer = 0; //isInteractable = false;
        }
    }
    void Open()
    {
        codeScreen.gameObject.SetActive(true);
        codeScreen.BroadcastMessage("BeginGame",securityLvl);
    }

    public void Alarm()
    {
        isAlarm = true;
        alarm.Play();
    }
}
