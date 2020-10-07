using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    private Animator doorAnim;
    private Sounds sounds;
    private AudioSource audio;
    [SerializeField] private bool open = false, interacting = false, isMonsterOpen = false, isMonsterCheckOpenDoor = false, isMonsterInDoorRange = false;
    private bool isMonsterNearbyAndOpen = false, isMonsterClose = false;
    [SerializeField] private GameObject monster;
    public MonsterAI monsterScript;
    int i = 0;
    void Start()
    {
        sounds = GetComponentInChildren<Sounds>();
        doorAnim = GetComponent<Animator>();
        audio = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        //alarmMonsterAboutOpenDoor();
        //StartCoroutine(waitAndCloseDoor());
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
        if (open == false)
        { 
            StartCoroutine("OpenDoor");
        }
        else if (open)
        {
            isMonsterClose = false;
            StartCoroutine("CloseDoor");
        }
    }
    public IEnumerator OpenDoor()
    {
        if (interacting)
        {
            StopCoroutine("CloseDoor");
        }
        doorAnim.SetFloat("Open", -1f);

        detectIsMonsterInDoorRange();
        if(isMonsterInDoorRange)
        {
            alarmMonsterAboutOpenDoor();
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        doorAnim.SetBool("IsOpen", true);
        interacting = true;

        sounds.Stop();
        sounds.Sound1();
        yield return new WaitWhile(AnimatorIsPlaying2);
        StartCoroutine(AudioFadeOut.FadeOut(audio, .35f));

        open = true;
        interacting = false;
        doorAnim.SetFloat("Open", 0f);
        print("open_end");
    }

    private void detectIsMonsterInDoorRange()
    {
        if ((Vector3.Distance(transform.position, monster.transform.position) < 15f))
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
        if (interacting)
        {
            StopCoroutine("OpenDoor");
        }
        doorAnim.SetBool("IsOpen", false);
        doorAnim.SetFloat("Open", 1f);

        detectIsMonsterInDoorRange();
        if(isMonsterInDoorRange)
        {
            alarmMonsterAboutOpenDoor();
        }
        gameObject.GetComponent<BoxCollider>().enabled = true;
        open = false;
        interacting = true;

        sounds.Stop();
        sounds.Sound2();
        yield return new WaitWhile(AnimatorIsPlaying);
        sounds.Stop();

        doorAnim.SetFloat("Open", 0f);
        open = false;
        interacting = false;
    }
    public void UnlockDoor()
    {
        open = true;
        doorAnim.SetBool("IsOpen", true);
        sounds.Sound1();
        gameObject.GetComponent<BoxCollider>().enabled = false;
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
        monsterScript.actualDoor = gameObject;
        monsterScript.isPlayerOpenCloseDoor = true;

    }
    bool AnimatorIsPlaying()
    {

        return doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < .95f;
    }
    bool AnimatorIsPlaying2()
    {
        return
               doorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > .1f;
    }
}
