using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class HackingGame : MonoBehaviour
{
    public Text inputText;
    private TimerHacking timer;
    public GameObject player;
    private NetworkServerUI network;
    [SerializeField] Tablet tablet;
    private string phrase, phrase1= "system.Override();", phrase2="firewall.Bypass();", phrase3="mainframe.DisableAlgorithms();", phrase4="kernel.GrantAccess();";
    string[] Alphabet;
    public int securityLvl=1;
    private int phase = 1, i=0, lettersTyped=0;
    private bool stop = false;
    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }
    void Start()
    {
        timer = GetComponentInChildren<TimerHacking>();
        phrase = phrase1;
        Alphabet = new string[26] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !stop)
        {
            if(phrase==phrase1)
                EnterCommand(inputText.text);
            else
             EnterCommand(inputText.text.Substring(inputText.text.Length-phrase.Length));
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && lettersTyped>0)
        {
            lettersTyped--;
            string del = inputText.text.Substring(inputText.text.Length - 1, 1);
            inputText.text= inputText.text.Remove(inputText.text.Length - 1, 1);
            if (i < phrase.Length || del == ";")
                i--;
        }
        else if(Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKeyDown(KeyCode.Backspace) && !stop)
        {
            lettersTyped++;
            //check if phrase is fully written
            if(i< phrase.Length)
            {
                inputText.text += phrase.Substring(i, 1);
                i++;
            }
            else
                inputText.text += Alphabet[Random.Range(0, Alphabet.Length)];
        }
        if (timer.timer < 0 && timer.isCounting)
        {
            timer.isCounting = false;
            Alarm();
            timer.timer = 1;
        }
    }
    void EnterCommand(string command)
    {
        switch (phase)
        {
            case 1:
                if (command == phrase1)
                {
                    inputText.text += "\nAccess granted. Security Level: " + securityLvl + "\n\nKernel:/ Mainframe:/ Firewall:/ > ";
                    phase++;
                    lettersTyped = 0;
                    i = 0;
                    phrase = phrase2;
                    timer.isCounting = true;
                    timer.securityLvl = securityLvl;
                    switch (securityLvl)
                    {
                        case 1:
                            timer.timer = 22;
                            break;
                        case 2:
                            timer.timer = 17;
                            break;
                        case 3:
                            timer.timer = 12;
                            break;
                    }
                    network.ServerSendMessage("HackingStart");
                }
                else
                    Alarm();
                break;
            case 2:
                if (command == phrase2)
                {
                    inputText.text += "\nFirewall has been bypassed.\n\nKernel:/Mainframe:/ > ";
                    phase++;
                    lettersTyped = 0;
                    i = 0;
                    phrase = phrase3;
                    switch (securityLvl)
                    {
                        case 1:
                            network.ServerSendMessage("Blocks" + 20); 
                            break;
                        case 2:
                            network.ServerSendMessage("Blocks" + 18);
                            break;
                        case 3:
                            network.ServerSendMessage("Blocks" + 16);
                            break;
                    }
                }
                else
                    Alarm();
                break;
            case 3:
                if (command == phrase3)
                {
                    inputText.text += "\nMainframe algorithms disabled.\n\nKernel:/ > ";
                    phase++;
                    lettersTyped = 0;
                    i = 0;
                    phrase = phrase4;
                    switch (securityLvl)
                    {
                        case 1:
                            network.ServerSendMessage("Blocks" + 20);
                            break;
                        case 2:
                            network.ServerSendMessage("Blocks" + 18);
                            break;
                        case 3:
                            network.ServerSendMessage("Blocks" + 16);
                            break;
                    }
                }
                else
                    Alarm();
                break;
            case 4:
                if (command == phrase4)
                {
                    inputText.text += "\nKernel breached...";
                    phase++;
                    lettersTyped = 0;
                    i = 0;
                    phrase = "";
                    stop = true;
                    switch (securityLvl)
                    {
                        case 1:
                            network.ServerSendMessage("Blocks" + 20);
                            break;
                        case 2:
                            network.ServerSendMessage("Blocks" + 18);
                            break;
                        case 3:
                            network.ServerSendMessage("Blocks" + 16);
                            break;
                    }
                }
                else
                    Alarm();
                break;
        }
    }
    //Start Hacking
    void OnEnable()
    {
        stop = false;
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null || c.gameObject.tag == "MainCamera")
            {
                continue;
            }
            c.enabled = false;
        }
        network.ServerSendMessage("OpenHelp");
        GameData.canPause = false;
    }
    //Stop Hacking
    public void Alarm()
    {
        tablet.Alarm();
        Close();
    }
    public void Close()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = true;
        }
        network.ServerSendMessage("CloseHelp");
        inputText.text = "";
        phase = 1;
        phrase = phrase1;
        lettersTyped = 0;
        i = 0;
        network.ServerSendMessage("HackingEnd");
        timer.timer = 0;
        timer.isCounting = false;
        gameObject.SetActive(false);
        GameData.canPause = true;
    }
    public void StopTimer()
    {
        timer.isCounting = false;
    }
}
