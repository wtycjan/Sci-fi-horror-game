using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpicking : MonoBehaviour
{
    public Image codeScreen;
    public Image tutoralScreen;
    public Material openMaterial;
    public int securityLvl = 1;
    public bool isAlarm = false;
    public AudioSource alarm;
    private NetworkServerUI network;

    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }
    public void Interact()
    {
        if (GameData.lockpickingTutoral == false)
            Tutorial();
        else
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
        network.ServerSendMessage("Alarm2");
    }
    public void StopAlarm()
    {
        isAlarm = false;
        alarm.Stop();
        network.ServerSendMessage("Alarm0");
    }
    void Tutorial()
    {
        tutoralScreen.gameObject.SetActive(true);
        GameData.canPause = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void CloseTutorial()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameData.lockpickingTutoral = true;
        tutoralScreen.gameObject.SetActive(false);
        codeScreen.gameObject.SetActive(true);
        codeScreen.BroadcastMessage("BeginGame", securityLvl);
    }
}
