using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tablet : MonoBehaviour
{
    public Image hackingScreen;
    public int securityLvl=1;
    public bool isAlarm = false;
    public AudioSource alarm;

    public void Interact()
    {
        hackingScreen.gameObject.SetActive(true);
        hackingScreen.GetComponent<HackingGame>().securityLvl = securityLvl;
    }
    public void Alarm()
    {
        isAlarm = true;
        alarm.Play();
    }
}
