using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chest : MonoBehaviour
{
    Animation anim;
    public GameObject treasure;
    public Image codeScreen;
    public GameObject player;
    public NetworkServerUI network;
    void Start()
    {
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCode();
    }

    public void Interact()
    {
            Code();
    }
    public void Open()
    {
        gameObject.layer = 0;
        anim.Play();
        treasure.SetActive(true);
    }
    void Code()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = false;
        }

        codeScreen.gameObject.SetActive(true);
        network.ServerSendMessage("OpenPasswords");
    }
    public void CloseCode()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = true;
        }
        codeScreen.gameObject.SetActive(false);
        network.ServerSendMessage("ClosePasswords");
    }
}
