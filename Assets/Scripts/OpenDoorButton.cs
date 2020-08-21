using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    private Animator doorAnim;
    private Sounds sounds;
    [SerializeField] private bool open = false, interacting=false;
    void Start()
    {
        sounds = GetComponentInChildren<Sounds>();
        doorAnim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if(open==false && !interacting)
            StartCoroutine("OpenDoor");
        else if (open && !interacting)
            StartCoroutine("CloseDoor");
    }
    public IEnumerator OpenDoor()
    {
        Destroy(gameObject.GetComponent<BoxCollider>());
        doorAnim.SetBool("IsOpen", true);
        interacting = true;
        sounds.Sound1();
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(1.3f);
        open = true;
        interacting = false;
    }
    public IEnumerator CloseDoor()
    {
        gameObject.AddComponent<BoxCollider>();
        open = false;
        interacting = true;
        sounds.Sound2();
        doorAnim.SetBool("IsOpen", false);
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(1.3f);
        open = false;
        interacting = false;
    }
}
