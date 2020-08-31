using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    private Animator doorAnim;
    private Sounds sounds;
    [SerializeField] private bool open = false, interacting = false, isMonsterOpen = false, isMonsterCheckOpenDoor = false, isMonsterInDoorRange = false;
    private bool isMonsterNearbyAndOpen = false, isMonsterClose = false;
    [SerializeField] private GameObject monster;
    public MonsterAI monsterScript;
    void Start()
    {
        sounds = GetComponentInChildren<Sounds>();
        doorAnim = GetComponent<Animator>();
    }

    void Update()
    {
        alarmMonsterAboutOpenDoor();
        StartCoroutine(waitAndCloseDoor());
    }

    public IEnumerator waitAndCloseDoor()
    {
        if(isMonsterNearbyAndOpen && open)
        {
            isMonsterNearbyAndOpen = false;
            yield return new WaitForSeconds(3f);
            isMonsterOpen = false;
            isMonsterClose = true;
            StartCoroutine(CloseDoor());
        }
        
    }

    public void Interact()
    {
        if (open == false && !interacting)
        { 
            StartCoroutine("OpenDoor");
        }
        else if (open && !interacting)
        {
            isMonsterClose = false;
            StartCoroutine("CloseDoor");
        }
    }
    public IEnumerator OpenDoor()
    {
        detectIsMonsterInDoorRange();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        doorAnim.SetBool("IsOpen", true);
        interacting = true;
        sounds.Sound1();
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(1.3f);
        open = true;
        interacting = false;
    }

    private void detectIsMonsterInDoorRange()
    {
        if ((Vector3.Distance(transform.position, monster.transform.position) < 15))
        {
            isMonsterInDoorRange = true;
        }
        else
        {
            isMonsterInDoorRange = false;
        }
    }

    public IEnumerator CloseDoor()
    {
        if(isMonsterCheckOpenDoor)
        {
            isMonsterCheckOpenDoor = false;
        }
        gameObject.GetComponent<BoxCollider>().enabled = true;
        open = false;
        interacting = true;
        sounds.Sound2();
        doorAnim.SetBool("IsOpen", false);
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(1.3f);
        open = false;
        interacting = false;
    }
    private void openMonsterDoor()
    {
        StartCoroutine(OpenDoor());
        isMonsterInDoorRange = false;
        isMonsterNearbyAndOpen = true;
        isMonsterOpen = true;
    }

    private void alarmMonsterAboutOpenDoor()
    {
        if ((open && !isMonsterOpen && !isMonsterCheckOpenDoor && isMonsterInDoorRange) ||
            (!isMonsterClose && !isMonsterCheckOpenDoor && isMonsterInDoorRange))
        {
            monsterScript.actualDoor = gameObject;
            monsterScript.isPlayerOpenDoor = true;
            isMonsterCheckOpenDoor = true;
        }
    }
}
