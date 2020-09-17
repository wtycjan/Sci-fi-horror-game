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
    private GameController gameController;
    private int connections = 0;
    private void OnGUI()
    {
        string ipaddress = LocalIPAddress();
        //GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        //GUI.Label(new Rect(20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
        GUI.Label(new Rect(20, Screen.height - 20, 100, 20), "Connnected:" + NetworkServer.connections.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        NetworkServer.DisconnectAll();
        if (!NetworkServer.active)
        { 
        NetworkServer.Listen(25000);
        
        }
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        NetworkServer.RegisterHandler(888, ServerRecieveMessage);
    }

    private void Update()
    {
        //send data to any new connections
        if(NetworkServer.connections.Count>connections)
        {
            ServerSendMessage("Pswd " + GameData.password1);
            connections++;
        }
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
            case "5":
                gameController.SendMessage("OpenDoor5");
                break;
            case "Red":
                gameController.SendMessage("RedButtonPressed");
                break;
            case "Yellow":
                gameController.SendMessage("YellowButtonPressed");
                break;
            case "Blue":
                gameController.SendMessage("BlueButtonPressed");
                break;
            case "StopHackTimer":
                gameController.SendMessage("StopHackTimer");
                break;
            case "StopHacking":
                gameController.SendMessage("StopHacking");
                break;
        }


        Debug.Log(msg.value);
        
    }
    public void ServerSendMessage(string message)
    {
            StringMessage msg = new StringMessage();
            msg.value = message;
            NetworkServer.SendToAll(888, msg);
            //Debug.Log("msg sent" + msg.value);
    }
    public void CloseServer()
    {
        NetworkServer.Shutdown();
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
