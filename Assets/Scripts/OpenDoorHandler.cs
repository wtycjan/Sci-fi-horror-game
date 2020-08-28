using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenDoorHandler : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;
    [SerializeField] private GameObject door5;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Door (1)")
        {
            
            StartCoroutine(playOrStopAnimation());
            door1.SendMessage("openMonsterDoor");
        }
        else if (collision.gameObject.name == "Door (2)")
        {
            StartCoroutine(playOrStopAnimation());
            door2.SendMessage("openMonsterDoor");
        }
        else if (collision.gameObject.name == "Door (3)")
        {
            StartCoroutine(playOrStopAnimation());
            door3.SendMessage("openMonsterDoor");
        }
        else if (collision.gameObject.name == "Door (4)")
        {
            StartCoroutine(playOrStopAnimation());
            door4.SendMessage("openMonsterDoor");
        }
        else if (collision.gameObject.name == "Door (5)")
        {
            StartCoroutine(playOrStopAnimation());
            door5.SendMessage("openMonsterDoor");
        }
        else
        {

        }
    }

    IEnumerator playOrStopAnimation()
    {
        
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        SendMessage("prepareMonsterToStay");

        yield return new WaitForSeconds(2f);
        
        SendMessage("prepareMonsterToWalk");
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
    
}
