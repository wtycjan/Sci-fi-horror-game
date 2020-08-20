using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class NetworkClientUI : MonoBehaviour
{
    static NetworkClient client;
    private void OnGUI()
    {
        string ipaddress = LocalIPAddress();
        //GUI.Box(new Rect(10, Screen.height - 50, 200, 150), ipaddress);
        //GUI.Label(new Rect(20, Screen.height - 30, 200, 90), "Status:" + client.isConnected);

        if (!client.isConnected)
        {
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Connect"))
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
        client.Connect("192.168.8.143", 25000);
    }

    static public void SendBtnInfo(int bDelta)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = bDelta + "";
            client.Send(888, msg);
            Debug.Log("msg sent");
        }
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
