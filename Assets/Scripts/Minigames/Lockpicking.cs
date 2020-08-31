using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpicking : MonoBehaviour
{
    public Image codeScreen;
    private GameObject player;
    public NetworkServerUI network;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        Open();
    }
    void Open()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = false;
        }

        codeScreen.gameObject.SetActive(true);
        network.ServerSendMessage("OpenLockpicking");
    }
    public void Close()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = true;
        }
        codeScreen.gameObject.SetActive(false);
        network.ServerSendMessage("ClooseLockpicking");
    }
}
