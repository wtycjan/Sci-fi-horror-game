using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
public class TypingInput : MonoBehaviour
{
    public InputField myField;
    public Text osText;
    public Text tipText;
    public NetworkServerUI network;
    public TimerHacking timer;
    private Dictionary<string, System.Action<string, string>> commands;
    public int securityLvl;
    private string resetText;
    private Vector3 inputPosition;
    void Start()
    {
        commands = new Dictionary<string, System.Action<string, string>>();
        
        if(securityLvl>0)
            commands.Add("system.Override();", Enter);
        else
            commands.Add(GameData.password1, UnlockChest);

        resetText = osText.text;
        inputPosition = myField.gameObject.transform.position;
        // Listen when the inputfield is validated
        myField.onEndEdit.AddListener(OnEndEdit);

    }

    private void Update()
    {
        if (myField.isActiveAndEnabled)
        {
            StartTyping();
            Stop();
        }    
        if(securityLvl > 0)
        if (timer.timer < 0 )
        {
            timer.isCounting = false;
            Alarm();
            timer.timer = 1;
        }
    }
    private void OnEndEdit(string input)
    {
        // Only consider onEndEdit if the Submit button has been pressed
        if (!Input.GetButtonDown("Submit"))
            return;

        bool commandFound = false;

        // Find the command
        foreach (var item in commands)
        {
            if (item.Key.Equals(input)) 
            {
                commandFound = true;
                item.Value(item.Key, input);
                break;
            }
        }

        // Do something if the command has not been found
        if (!commandFound && securityLvl>0)
        {
            Alarm();
        }
            

        // Clear the input field (if you want)
        myField.text = "";
    }
    private void Enter(string command, string input)
    {
        osText.text += "\nAccess granted. Security Level: "+ securityLvl;
        network.ServerSendMessage("HackingStart");
        osText.text += "\n\nKernel:/Mainframe:/Firewall:/ > ";
        tipText.gameObject.transform.position += new Vector3(275, -61, 0);
        myField.gameObject.transform.position += new Vector3(275, -61, 0);
        tipText.text = "firewall.Disable();";
        commands.Add("firewall.Disable();", Firewall);
        timer.isCounting = true;
        timer.securityLvl = securityLvl;
        switch (securityLvl)
        {
            case 1:
                timer.timer = 29;
                break;
            case 2:
                timer.timer = 24;
                break;
            case 3:
                timer.timer = 19;
                break;
        }


    }
    private void Firewall(string command, string input)
    {
        osText.text += "\nFirewall has been bypassed.\n\nKernel:/Mainframe:/ > ";
        tipText.gameObject.transform.position += new Vector3(-80, -69, 0);
        myField.gameObject.transform.position += new Vector3(-80, -69, 0);
        commands.Add("mainframe.Disable();", Mainframe);
        tipText.text = "mainframe.Disable();";
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

    private void Mainframe(string command, string input)
    {
        osText.text += "\nMainframe algorithms disabled.\n\nKernel:/ > ";
        tipText.gameObject.transform.position += new Vector3(-100, -69, 0);
        myField.gameObject.transform.position += new Vector3(-100, -69, 0);
        commands.Add("kernel.Disable();", Kernel);
        tipText.text = "kernel.Disable();";
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
    private void Kernel(string command, string input)
    {
        osText.text += "\nKernel breached...";
        tipText.text = "";
        myField.gameObject.SetActive(false);
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
    private void UnlockChest(string command, string input)
    {
        SendMessage("Close");
        SendMessage("Unlock");
    }
    public void StartTyping()
    {
        EventSystem.current.SetSelectedGameObject(myField.gameObject);
        myField.ActivateInputField();
    }
    public void Stop()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SendMessage("Close");
    }
    public void Alarm()
    {
        Debug.Log("Alarm");
        myField.gameObject.SetActive(true);
        osText.text = resetText;
        tipText.text = "";
        tipText.gameObject.transform.position = inputPosition;
        myField.gameObject.transform.position = inputPosition;
        commands.Clear();
        commands.Add("system.Override();", Enter);
        network.ServerSendMessage("HackingEnd");
        SendMessage("Close");
    }
}
