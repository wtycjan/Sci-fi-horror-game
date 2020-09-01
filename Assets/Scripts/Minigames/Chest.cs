﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chest : MonoBehaviour
{
    Animation anim;
    public GameObject treasure;
    public Image codeScreen;
    private GameObject player;
    public NetworkServerUI network;
    void Start()
    {
        anim = GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player");
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
    }
    void Open()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = false;
        }

        codeScreen.gameObject.SetActive(true);
        network.ServerSendMessage("OpenPasswords");
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
    }
}
