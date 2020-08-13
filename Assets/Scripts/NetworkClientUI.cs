using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class NetworkClientUI : MonoBehaviour
{
    NetworkClient client;
    private void OnGUI()
    {
        string ipaddress = LocalIPAddress();
        GUI.Box(new Rect(10, Screen.height - 50, 100, 50), ipaddress);
        GUI.Label(new Rect(20, Screen.height - 30, 100, 20), "Status:" + client.isConnected);

        if(!client.isConnected)
        {
            if(GUI.Button(new Rect(10,10,60,50),"Connect"))
            {
                Connect();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        client = new NetworkClient();
    }

    private void Connect()
    {
        client.Connect("192.168.0.13", 25000);
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
