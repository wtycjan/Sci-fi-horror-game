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
    private Dictionary<string, System.Action<string, string>> commands;

    void Start()
    {
        commands = new Dictionary<string, System.Action<string, string>>();
        commands.Add(GameData.password1, UnlockChest);
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
        if (!commandFound)
        {
            Alarm();
        }


        // Clear the input field (if you want)
        myField.text = "";
    }
    private void UnlockChest(string command, string input)
    {
        //SendMessage("Close");
        SendMessage("Unlock");
        commands.Remove(GameData.password1);
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
        SendMessage("StartAlarm");
    }

}
