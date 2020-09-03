using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;
    [SerializeField] private GameObject door5;
    [SerializeField] private GameObject redButton;
    [SerializeField] private GameObject yellowButton;
    [SerializeField] private GameObject blueButton;
    public Image blackScreen;
    public GameObject monster;
    private GameObject player;
    public RuntimeAnimatorController jumpAnim;
    private Sounds sound;
    private bool cutscene = false, cameraCutscene = false;
    Quaternion startRot, endRot;
    [SerializeField] TypingInput tp;

    private void Awake()
    {
        GameData.password1= RandomPassword();
    }
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        //Debug only!
        if (Input.GetKeyDown("1"))
            if(GameData.door1)
                OpenDoor1();
        if (Input.GetKeyDown("2"))
            OpenDoor2();
        if (Input.GetKeyDown("3"))
            OpenDoor3();
        if (Input.GetKeyDown("4"))
            OpenDoor4();
        if (Input.GetKeyDown("5"))
            OpenDoor5();
        if (Input.GetKeyDown("6"))
        {
            GameData.door1 = true;
            tp.isAlarm = true;
        }
       if (Input.GetKeyDown("7"))
       {
            tp.isAlarm = false;
            GameData.door1 = false; 
       }
           

        //death
        if (Vector3.Distance(monster.transform.position, player.transform.position) < 1.6f && !cutscene)
        {
            StartCoroutine("Death");
        }
        if (player.transform.rotation != endRot && cameraCutscene)
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.AngleAxis(90, Vector3.left), Time.deltaTime * 2f);

    }

    void OpenDoor1()
    {
        door1.SendMessage("Interact");
    }
    void OpenDoor2()
    {
        door2.SendMessage("Interact");
    }
    void OpenDoor3()
    {
        door3.SendMessage("Interact");
    }
    void OpenDoor4()
    {
        door4.SendMessage("Interact");
    }
    void OpenDoor5()
    {
        door5.SendMessage("Interact");
    }

    public IEnumerator Death()
    {
        cutscene = true;
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = false;
        }
        MonoBehaviour[] scripts2 = monster.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts2)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = false;
        }
        Vector3 lookpoint = monster.GetComponentInChildren<BoxCollider>().transform.position;
        player.transform.LookAt(lookpoint);
        lookpoint = player.GetComponentInChildren<BoxCollider>().transform.position;
        monster.transform.LookAt(lookpoint);
        player.GetComponent<CharacterController>().enabled = false;
        monster.GetComponentInChildren<CapsuleCollider>().enabled = false;
        sound.Sound1();
        yield return new WaitForSeconds(.05f);
        monster.GetComponent<Animator>().runtimeAnimatorController = jumpAnim;
        monster.GetComponent<Rigidbody>().AddForce(monster.transform.forward* 100);
        yield return new WaitForSeconds(.3f);
        sound.Sound2();
        cameraCutscene = true;
        yield return new WaitForSeconds(.3f);
        monster.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);
        StartCoroutine("Restart");
    }
    public IEnumerator Restart()
    {
        blackScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public string RandomPassword()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[5];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        Debug.Log(finalString);
        return finalString;

    }

    void RedButtonPressed()
    {
        redButton.SendMessage("Interact");
    }
    void YellowButtonPressed()
    {
        yellowButton.SendMessage("Interact");
    }
    void BlueButtonPressed()
    {
        blueButton.SendMessage("Interact");
    }
}
