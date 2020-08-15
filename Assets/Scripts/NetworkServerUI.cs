using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class NetworkServerUI : MonoBehaviour
{
    CrossPlatformInputManager.VirtualButton doorBtn;
    public GameController gameController;
    private void OnGUI()
    {
        string ipaddress = LocalIPAddress();
        //GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        //GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
        //GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connnected:" + NetworkServer.connections.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        doorBtn = new CrossPlatformInputManager.VirtualButton("Fire3");
        CrossPlatformInputManager.RegisterVirtualButton(doorBtn);

        NetworkServer.Listen(25000);
        NetworkServer.RegisterHandler(888, ServerRecieveMessage);
    }

    void ServerRecieveMessage (NetworkMessage message)
    {
        StringMessage msg = new StringMessage();
        msg.value = message.ReadMessage<StringMessage>().value;

        switch (msg.value)
        {
            case "1":
                gameController.SendMessage("OpenDoor1");
                break;
            case "2":
                gameController.SendMessage("OpenDoor2");
                break;
            case "3":
                gameController.SendMessage("OpenDoor3");
                break;
            case "4":
                gameController.SendMessage("OpenDoor4");
                break;
        }


        Debug.Log(msg.value);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
