using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElevatorConsole : MonoBehaviour
{
    public Image consoleScreen;
    private NetworkServerUI network;

    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }

    public void Interact()
    {
        consoleScreen.gameObject.SetActive(true);
    }
}
