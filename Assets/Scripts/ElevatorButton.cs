using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorButton : MonoBehaviour
{
    bool interacting = false;
    public GameController gameController;
    public Text failedtxt;
    private Sounds sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
    }
    public void Interact()
    {
        if (!interacting)
            Elevator1();
    }

    void Elevator1()
    {
        interacting = true;
        if (GameData.level1)
        {
            gameController.StartCoroutine("Restart");
            sound.Sound5();
        }
            
        else
            StartCoroutine("Text");
    }

    IEnumerator Text()
    {
        failedtxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        failedtxt.gameObject.SetActive(false);
        interacting = false;
    }
}
