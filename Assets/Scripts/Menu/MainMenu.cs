using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using TMPro;
using System.Net.NetworkInformation;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI ip;
    public GameObject canvas;
    public GameObject blackScreen;
    public AudioSource sounds;
    private Camera mainCamera;
    bool start = false;
    float speed = 0.1f;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameData.respawn = false;
        print("brghtness: " + PlayerPrefs.GetFloat("brightness-volume") + "\nsens: " + PlayerPrefs.GetFloat("sensivity-volume"));
        ip.text = GetLocalIPv4();
    }
    private void FixedUpdate()
    {
        if(start)
        {
            mainCamera.transform.Translate(mainCamera.transform.right * speed/2 * Time.deltaTime, Space.Self);
            mainCamera.transform.Translate(mainCamera.transform.up * speed / -5 * Time.deltaTime, Space.Self);
            mainCamera.transform.Translate(mainCamera.transform.forward * speed * Time.deltaTime, Space.Self);
            speed += .03f;
        }

        //debug
        //if(Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    PlayerPrefs.DeleteAll();
        //}
            
    }
    public void PlayButton(int i)
    {
        StartCoroutine("BeginGame");
        GameData.loadLevel = i;
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    /*public string LocalMacIPAddress()
    {
        string localIP = "";
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up)
            {
                foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.Address.ToString();
                    }
                }
            }
        }
        return localIP;
    }*/

    internal static string GetLocalIPv4()
    {  // Checks your IP adress from the local network connected to a gateway. This to avoid issues with double network cards
        string output = "";  // default output
        foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces()) // Iterate over each network interface
        {  
            // Find the network interface which has been provided in the arguments, break the loop if found
            if (item.OperationalStatus != OperationalStatus.Up)
                continue;

           IPInterfaceProperties adapterProperties = item.GetIPProperties();
           if (adapterProperties.GatewayAddresses.Count == 0)
                continue;

            foreach (UnicastIPAddressInformation ip in adapterProperties.UnicastAddresses)
            {   // If the IP is a local IPv4 adress
                if (ip.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    continue;
                if (IPAddress.IsLoopback(ip.Address))
                    continue;
                output = ip.Address.ToString();

            }
        }
        // Return results
        return output;
    }


    public string LocalIPAddress()
    {
        string localIP = "";
        foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            //IPAddress ip = dnsIp.MapToIPv4();
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply reply = ping.Send("google.com");
            IPStatus status = reply.Status;

            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
               
                //break;
            }
        }
        return localIP;
    }
    public IEnumerator BeginGame()
    {
        print("sttart");
        StartCoroutine(AudioFadeOut.FadeOut(sounds, .4f));
        yield return new WaitForSeconds(.5f);
        sounds.GetComponent<Sounds>().Sound1();
        canvas.SetActive(false);
        start = true;
        mainCamera.enabled = true;
        yield return new WaitForSeconds(2.15f);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
