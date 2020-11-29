using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElevatorConsoleScreen : MonoBehaviour
{
    private NetworkServerUI network;
    public Image consoleScreen;
    public GameObject player;
    public GameObject button;
    public GameObject text;
    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();
    }
    void OnEnable()
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
        network.ServerSendMessage("OpenElevatorConsole");
        GameData.canPause = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(GameData.keycard1)
        {
            button.SetActive(true);
            text.SetActive(false);
        }
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
        consoleScreen.gameObject.SetActive(false);
        network.ServerSendMessage("CloseElevatorConsole");
        GameData.canPause = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
