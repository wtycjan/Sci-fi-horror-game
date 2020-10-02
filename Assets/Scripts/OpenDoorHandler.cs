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
    [SerializeField] private GameObject door6;
    public bool isDoorCloseInFronfOfMonster = false;

 
    private void OnCollisionEnter(Collision collision)
    {
        isDoorCloseInFronfOfMonster = true;
    }

}
