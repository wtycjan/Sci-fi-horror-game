using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using TMPro;
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
    }
    private void Update()
    {
        if(start)
        {
            mainCamera.transform.Translate(mainCamera.transform.right * speed/2 * Time.deltaTime, Space.Self);
            mainCamera.transform.Translate(mainCamera.transform.up * speed / -5 * Time.deltaTime, Space.Self);
            mainCamera.transform.Translate(mainCamera.transform.forward * speed * Time.deltaTime, Space.Self);
            speed += .03f;
        }
            
    }
    public void PlayButton()
    {
        print("0");
        StartCoroutine("BeginGame");
        print("0.5");
    }
    public void QuitButton()
    {
        Application.Quit();
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
    public IEnumerator BeginGame()
    {
        print("sttart");
        StartCoroutine(AudioFadeOut.FadeOut(sounds, .4f));
        print("1");
        yield return new WaitForSeconds(.5f);
        print("2");
        sounds.GetComponent<Sounds>().Sound1();
        print("3");
        canvas.SetActive(false);
        print("4");
        start = true;
        print("5");
        mainCamera.enabled = true;
        print("6");
        yield return new WaitForSeconds(2.15f);
        print("7");
        blackScreen.SetActive(true);
        print("8");
        yield return new WaitForSeconds(1);
        print("9");
        SceneManager.LoadScene(1);
    }
}
