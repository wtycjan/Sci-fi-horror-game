using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using VHS;
using UnityEngine.Rendering.PostProcessing;
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
    [SerializeField] private GameObject computer;
    [SerializeField] private ParticleSystem[] deathEffects;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject pointLight;
    public Image blackScreen;   //death
    public Image blackScreen2; //intro
    public GameObject pauseMenu;
    public MonsterAI monster;
    private FirstPersonController player;
    public RuntimeAnimatorController jumpAnim;
    private NetworkServerUI network;
    private Sounds sound;
    public bool cutscene = false;
    private bool cameraCutscene = false;
    Quaternion startRot, endRot;
   /* [SerializeField] TypingInput tp;
    [SerializeField] TypingInput ctp;
    [SerializeField] Lockpicking gtp;
    */
    private void Awake()
    {
        GameData.password1= RandomPassword();
        GameData.level1 = false;
        GameData.door1 = false;
        GameData.lockpickingTutoral = false;
    }
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
        StartCoroutine("UpdatePosition");
        //*************************
        //Enable before building
        StartCoroutine("Intro");
        //*************************
    }
    private void Update()
    {
        //Debug only!
       /* if (Input.GetKeyDown("1"))
                OpenDoor1();
        if (Input.GetKeyDown("2"))
            OpenDoor2();
        if (Input.GetKeyDown("3"))
            OpenDoor3();
        if (Input.GetKeyDown("4"))
            OpenDoor4();
        if (Input.GetKeyDown("5"))
            OpenDoor5();
        */
        setNewBrightness();

        //death
        if (Vector3.Distance(monster.transform.position, player.transform.position) < 1.6f && !cutscene && monster.isPlayerDetect )
        {
            StartCoroutine("Death");
        }
        if (player.transform.rotation != endRot && cameraCutscene)
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.AngleAxis(90, Vector3.left), Time.deltaTime * 2f);

        //pause
        if(Input.GetKeyDown(KeyCode.Escape) && GameData.canPause)
        {
            pauseMenu.SetActive(true);
            GameData.canPause = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
    private void setNewBrightness()
    {
        flashlight.GetComponent<Light>().intensity = PlayerPrefs.GetFloat("brightness-volume");
        pointLight.GetComponent<Light>().intensity = PlayerPrefs.GetFloat("brightness-volume");
    }


    private void turnOnDeathEffects()
    {
        foreach (ParticleSystem deathEffect in deathEffects)
        {
            deathEffect.Play();
        }
    }

    public IEnumerator Intro()
    {
        if(!GameData.respawn)
        {
            sound.Sound5();
            blackScreen2.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(11f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(blackScreen2.gameObject);
        OpenDoor5();
        GameData.canPause = true;
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
        GameData.respawn = true;
        GameData.canPause = false;
        cutscene = true;
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null || c.gameObject.tag == "MainCamera")
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
        monster.GetComponent<NavMeshAgent>().enabled = false;
        Vector3 lookpoint = monster.GetComponentInChildren<BoxCollider>().transform.position;
        player.transform.LookAt(lookpoint);
        lookpoint = player.GetComponentInChildren<BoxCollider>().transform.position;
        monster.transform.LookAt(lookpoint);
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponentInChildren<Sounds>().Stop();
        monster.GetComponentInChildren<CapsuleCollider>().enabled = false;
        sound.Sound1();
        yield return new WaitForSeconds(.05f);
        monster.GetComponent<Animator>().runtimeAnimatorController = jumpAnim;
        monster.GetComponent<Rigidbody>().AddForce(monster.transform.forward* 200);
        yield return new WaitForSeconds(.3f);
        turnOnDeathEffects();
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
        //network.CloseServer();
        network.ServerSendMessage("Restart");
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
    void StopHackTimer()
    {
        computer.GetComponent<Tablet>().StopTimer();
    }
    void StopHacking()
    {
        computer.GetComponent<Tablet>().Completed();
    }

    public IEnumerator UpdatePosition()
    {
        Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.z);
        network.ServerSendMessage("player: " + pos.x + " " + pos.y);
        if (Vector3.Distance(monster.transform.position, player.transform.position) < 15f)
        {
            pos = new Vector2(monster.transform.position.x, monster.transform.position.z);
            network.ServerSendMessage("monster: " + pos.x + " " + pos.y);
        }
        else
            network.ServerSendMessage("monster: " + -999+ " " + -999);
        yield return new WaitForSeconds(1f);
        StartCoroutine("UpdatePosition");
    }
}
