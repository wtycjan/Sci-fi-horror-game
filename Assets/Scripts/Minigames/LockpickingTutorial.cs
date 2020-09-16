using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockpickingTutorial : MonoBehaviour
{
    private NetworkServerUI network;

    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }
    public void OnEnable()
    {
        network.ServerSendMessage("LockpickingTutorial");
    }
    public void OnDisable()
    {
        network.ServerSendMessage("CloseLockpickingTutorial");
    }
}
