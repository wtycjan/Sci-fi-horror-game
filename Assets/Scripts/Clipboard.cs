using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clipboard : MonoBehaviour
{
    private GameObject player;
    public Image screen;
    public TextMeshProUGUI pswd;
    private NetworkServerUI network;
    void Start()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        pswd.text = GameData.password1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && screen.gameObject.activeSelf)
            Close();
    }
    public void Interact()
    {
        if(screen.gameObject.activeSelf)
            Close();
        else
            Open();
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
        screen.gameObject.SetActive(true);
        GameData.canPause = false;
        network.ServerSendMessage("SavePassword");
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
        screen.gameObject.SetActive(false);
        GameData.canPause = true;
        network.ServerSendMessage("ClosePasswords");
    }
}
