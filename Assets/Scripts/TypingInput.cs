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
    public Chest chest;
    public NetworkServerUI network;
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
            StartTyping();
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
            Debug.Log("Alarm");
            osText.text = resetText;
            myField.gameObject.transform.position = inputPosition;
            commands.Clear();
            commands.Add("system.Override();", Enter);
        }
            

        // Clear the input field (if you want)
        myField.text = "";
    }
    private void Enter(string command, string input)
    {
        osText.text += "\nAccess granted. Security Level: "+ securityLvl;
        switch (securityLvl)
        {
            case 1:
                osText.text += "\n\nKernel:/ > ";
                myField.gameObject.transform.position += new Vector3(95, -64, 0);
                commands.Add("kernel.Disable();", Kernel);
                break;
            case 2:
                osText.text += "\n\nKernel:/Mainframe:/ > ";
                myField.gameObject.transform.position += new Vector3(205, -64, 0);
                commands.Add("mainframe.Disable();", Mainframe);
                break;
            case 3:
                osText.text += "\n\nKernel:/Mainframe:/Firewall:/ > ";
                myField.gameObject.transform.position += new Vector3(295, -64, 0);
                commands.Add("firewall.Disable();", Firewall);
                break;
        }

        
        Debug.Log("user");
    }
    private void Firewall(string command, string input)
    {
        osText.text += "\nFirewall has been bypassed.\n\nKernel:/Mainframe:/ > ";
        myField.gameObject.transform.position += new Vector3(-88, -74, 0);
        commands.Add("mainframe.Disable();", Mainframe);
    }

    private void Mainframe(string command, string input)
    {
        osText.text += "\nMainframe algorithms disabled.\n\nKernel:/ > ";
        myField.gameObject.transform.position += new Vector3(-110, -74, 0);
        commands.Add("kernel.Disable();", Kernel);
    }
    private void Kernel(string command, string input)
    {
        osText.text += "\nKernel breached.\n\tPasscode: "+ GameData.password1;
        myField.gameObject.SetActive(false);
        network.ServerSendMessage("Pswd "+ GameData.password1);
    }
    private void UnlockChest(string command, string input)
    {
        chest.CloseCode();
        chest.Open();
    }
    public void StartTyping()
    {
        EventSystem.current.SetSelectedGameObject(myField.gameObject);
        myField.ActivateInputField();
    }

}
