using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHeroCircle : MonoBehaviour
{
    private CircleCollider2D col;
    private Animator anim;
    private bool isClickable = false;
    private GameObject ball;
    private LockpickingGame lockpick;
    private Sounds sound;
    [SerializeField] Lockpicking terminalAlarm;

    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        lockpick = GetComponentInParent<LockpickingGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isClickable = true;
        ball = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isClickable = false;
    }
    void Interact()
    {
        StartCoroutine("animControl");
        if (isClickable)
        {
            Destroy(ball);
            lockpick.open--;
            sound.Sound7();
        }
        else
        {
            lockpick.StopGame();
            terminalAlarm.Alarm();
        }


    }
    private IEnumerator animControl()
    {
        anim.SetBool("isPressed", true);
        yield return new WaitForSeconds(.2f);
        anim.SetBool("isPressed", false);
    }
}
