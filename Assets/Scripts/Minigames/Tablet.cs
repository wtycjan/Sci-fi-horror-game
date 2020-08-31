using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tablet : MonoBehaviour
{
    bool interacting = false;
    public Image hackingScreen;
    private GameObject player;
    public NetworkServerUI network;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (!interacting)
            Open();
    }
    void Open()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = false;
        }

        interacting = true;
        hackingScreen.gameObject.SetActive(true);
        network.ServerSendMessage("OpenHelp");
    }
    void Close()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = true;
        }
        interacting = false;
        hackingScreen.gameObject.SetActive(false);
        network.ServerSendMessage("CloseHelp");
    }
}
