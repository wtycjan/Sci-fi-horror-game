using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tablet : MonoBehaviour
{
    bool interacting = false;
    public Image hackingScreen;
    public GameObject player;
    public NetworkServerUI network;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            StopHacking();
    }

    public void Interact()
    {
        if (!interacting)
            Hacking();
    }
    void Hacking()
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
    void StopHacking()
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
