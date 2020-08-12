using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private Animator btnAnim;
    private Animator doorAnim;
    [SerializeField] private bool open = false;
    void Start()
    {
        btnAnim = GetComponent<Animator>();
        doorAnim = door.GetComponent<Animator>();
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
        btnAnim.SetBool("IsPressed", true);
        doorAnim.SetBool("IsOpen", true);
        yield return new WaitForSeconds(.1f);
        btnAnim.SetBool("IsPressed", false);
        yield return new WaitForSeconds(6f);
        open = true;
    }
    public IEnumerator CloseDoor()
    {
        open = false;
        btnAnim.SetBool("IsPressed", true);
        doorAnim.SetBool("IsOpen", false);
        yield return new WaitForSeconds(.1f);
        btnAnim.SetBool("IsPressed", false);
        yield return new WaitForSeconds(6f);
        open = false;
    }
}
