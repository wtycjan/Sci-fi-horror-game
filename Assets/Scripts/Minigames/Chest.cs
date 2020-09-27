using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class Chest : MonoBehaviour
{
    Animation anim;
    public GameObject treasure;
    public Image codeScreen;
    private GameObject player;
    private NetworkServerUI network;
    private Sounds sounds;
    public bool isAlarm = false;
    public AudioSource alarm;
    void Start()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
        anim = GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player");
        sounds = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();

    }
    public void Interact()
    {
        Open();
    }
    public void Unlock()
    {
        gameObject.layer = 0;
        anim.Play();
        treasure.SetActive(true);
        sounds.Sound4();
        Close();
    }
    public void StartAlarm()
    {
        Close();
        isAlarm = true;
        alarm.Play();
        network.ServerSendMessage("Alarm1");
    }
    public void StopAlarm()
    {
        isAlarm = false;
        alarm.Stop();
        network.ServerSendMessage("Alarm0");
    }
    void Open()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null || c.gameObject.tag == "MainCamera")
            {
                continue;
            }
            c.enabled = false;
        }

        codeScreen.gameObject.SetActive(true);
        network.ServerSendMessage("OpenPasswords");
        GameData.canPause = false;
    }
    public void Close()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = true;
        }
        codeScreen.gameObject.SetActive(false);
        network.ServerSendMessage("ClosePasswords");
        GameData.canPause = true;
    }
}
