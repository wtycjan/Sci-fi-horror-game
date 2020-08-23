using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorHandler : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;
    [SerializeField] private GameObject door5;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Door (1)")
        {
            playOrStopAnimation();
            door1.SendMessage("Interact");
        }
        else if (collision.gameObject.name == "Door (2)")
        {
            playOrStopAnimation();
            door2.SendMessage("Interact");
        }
        else if (collision.gameObject.name == "Door (3)")
        {
            playOrStopAnimation();
            door3.SendMessage("Interact");
        }
        else if (collision.gameObject.name == "Door (4)")
        {
            playOrStopAnimation();
            door4.SendMessage("Interact");
        }
        else if (collision.gameObject.name == "Door (5)")
        {
            playOrStopAnimation();
            door5.SendMessage("Interact");
        }
        else
        {
            
        }
    }

    private void playOrStopAnimation()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Animator>().enabled = true;
    }
}
