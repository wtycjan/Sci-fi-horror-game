﻿using System.Collections;
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
        GameData.respawn = false;
    }
    private void FixedUpdate()
    {
        if (start)
        {
            mainCamera.transform.Translate(mainCamera.transform.right * speed / 2 * Time.deltaTime, Space.Self);
            mainCamera.transform.Translate(mainCamera.transform.up * speed / -5 * Time.deltaTime, Space.Self);
            mainCamera.transform.Translate(mainCamera.transform.forward * speed * Time.deltaTime, Space.Self);
            speed += .03f;
        }

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
        yield return new WaitForSeconds(.5f);
        sounds.GetComponent<Sounds>().Sound1();
        canvas.SetActive(false);
        start = true;
        mainCamera.enabled = true;
        yield return new WaitForSeconds(2.15f);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}