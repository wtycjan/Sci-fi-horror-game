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
        ip.text = LocalIPAddress();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameData.respawn = false;
        print("brghtness: " + PlayerPrefs.GetFloat("brightness-volume") + "\nsens: " + PlayerPrefs.GetFloat("sensivity-volume"));
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
    public void PlayButton()
    {
        StartCoroutine("BeginGame");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public string LocalIPAddress()
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
