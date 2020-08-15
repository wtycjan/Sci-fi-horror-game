using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;

    private void Update()
    {
        //Debug only!
        if (Input.GetKeyDown("1"))
            OpenDoor1();
        if (Input.GetKeyDown("2"))
            OpenDoor2();
        if (Input.GetKeyDown("3"))
            OpenDoor3();
        if (Input.GetKeyDown("4"))
            OpenDoor4();
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

}
