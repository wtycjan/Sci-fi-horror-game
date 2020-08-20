using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text;
public class TypingInput : MonoBehaviour
{
    InputField myField;
    public Text osText;
    public Chest chest;
    public NetworkServerUI network;
    private Dictionary<string, System.Action<string, string>> commands;
    void Start()
    {
        myField = GetComponent<InputField>();
        commands = new Dictionary<string, System.Action<string, string>>();
        // Add the commands you want to recognise along with the functions to call
        commands.Add("passcode.Override();", User);
        commands.Add("firewall.Bypass();", Firewall);
        //commands.Add("user.GrantAccess();", User);
        commands.Add("mainframe.DisableAlgorithms();", Mainframe);
        commands.Add("kernel.RequestAccess();", Kernel);
        commands.Add(GameData.password1, UnlockChest);
        // Listen when the inputfield is validated
        myField.onEndEdit.AddListener(OnEndEdit);

    }

    private void Update()
    {
        if (gameObject.activeSelf)
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
            if (item.Key.ToLower().StartsWith(input.ToLower()))
            {
                commandFound = true;
                item.Value(item.Key, input);
                break;
            }
        }

        // Do something if the command has not been found
        if (!commandFound)
            Debug.Log("No command found");

        // Clear the input field (if you want)
        myField.text = "";
    }

    private void Firewall(string command, string input)
    {
        osText.text += "\nFirewall has been bypassed.\n\nKernel:/Mainframe:/ > ";
        myField.gameObject.transform.position += new Vector3(-83, -75, 0);
    }
    private void User(string command, string input)
    {
        osText.text += "\nUser logged in.\n\nKernel:/Mainframe:/Firewall:/ > ";
        myField.gameObject.transform.position += new Vector3(300, -65, 0);
        Debug.Log("user");
    }

    private void Mainframe(string command, string input)
    {
        osText.text += "\nMainframe algorithms disabled.\n\nKernel:/ > ";
        myField.gameObject.transform.position += new Vector3(-120, -75, 0);
    }
    private void Kernel(string command, string input)
    {
        osText.text += "\nAccess granted.\n\tPasscode: "+ GameData.password1;
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
