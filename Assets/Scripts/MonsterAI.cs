using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    FollowPath path;
    Rigidbody rb2d;
    public RuntimeAnimatorController walkAnim;
    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController idleAnim;
    private Sounds sound;
    public float speed = 3f;
    private Vector3 deltaPosition, prevPosition;
    private bool scream = false, charge = false, stop = false;
    void Start()
    {
        prevPosition = transform.position;
        path = GetComponent<FollowPath>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody>();
        sound = GetComponent<Sounds>();
    }

    void Update()
    {
        //rotate
        deltaPosition = transform.position - prevPosition;
        if (deltaPosition != Vector3.zero && !stop)
        {
            transform.forward = deltaPosition;
        }
        prevPosition = transform.position;


        if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, player.transform.position) > 1.6f)
        {
            if (!charge)
            {
                StartCoroutine("Prepare");
            }
            else if (!scream)
            {
                sound.Sound1();
                scream = true;
            }
            else
            {
                anim.runtimeAnimatorController = runAnim;
                Debug.Log("Attack");
                float step = speed * Time.deltaTime; // calculate distance to move
                rb2d.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
            }

        }
        else
        {
            anim.runtimeAnimatorController = walkAnim;
            path.attacking = false; //performance !
            scream = false;
            charge = false;
        }
            


    }

    IEnumerator Prepare()
    {
        stop = true;
        anim.runtimeAnimatorController = idleAnim;
        path.attacking = true;
        yield return new WaitForSeconds(.4f);
        stop = false;
        charge = true;
    }
}

