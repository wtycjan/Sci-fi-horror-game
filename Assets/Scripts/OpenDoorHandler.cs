using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class OpenDoorHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Door (1)")
        {
            playOrStopAnimation();
            SendMessage("OpenDoor1");
            Thread.Sleep(1000);
            SendMessage("OpenDoor1");
        }
        if (collision.gameObject.name == "Door (2)")
        {
            playOrStopAnimation();
            SendMessage("OpenDoor2");
            Thread.Sleep(1000);
            SendMessage("OpenDoor2");
        }
        if (collision.gameObject.name == "Door (3)")
        {
            playOrStopAnimation();
            SendMessage("OpenDoor3");
            Thread.Sleep(1000);
            SendMessage("OpenDoor3");
        }
        if (collision.gameObject.name == "Door (4)")
        {
            playOrStopAnimation();
            SendMessage("OpenDoor4");
            Thread.Sleep(1000);
            SendMessage("OpenDoor4");
        }
        if (collision.gameObject.name == "Door (5)")
        {
            playOrStopAnimation();
            SendMessage("OpenDoor5");
            Thread.Sleep(1000);
            SendMessage("OpenDoor5");
        }
        else
        {
            print("hit");
        }
    }

    private void playOrStopAnimation()
    {
        GetComponent<Animator>().enabled = false;
        Thread.Sleep(500);
        GetComponent<Animator>().enabled = true;
    }
}
