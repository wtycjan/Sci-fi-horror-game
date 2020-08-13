using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    private Animator doorAnim;
    [SerializeField] private bool open = false;
    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if(open==false)
            StartCoroutine("OpenDoor");
        else
            StartCoroutine("CloseDoor");
    }
    public IEnumerator OpenDoor()
    {
        doorAnim.SetBool("IsOpen", true);
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(6f);
        open = true;
    }
    public IEnumerator CloseDoor()
    {
        open = false;
        doorAnim.SetBool("IsOpen", false);
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(6f);
        open = false;
    }
}
