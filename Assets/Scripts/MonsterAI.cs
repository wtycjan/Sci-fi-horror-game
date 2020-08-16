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
    public RuntimeAnimatorController jumpAnim;
    public float speed = 3f;
    private Vector3 prevPosition;
    void Start()
    {
        prevPosition = transform.position;
        path = GetComponent<FollowPath>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //rotate
        Vector3 deltaPosition = transform.position - prevPosition;
        if (deltaPosition != Vector3.zero)
        {
            transform.forward = deltaPosition;
        }
        prevPosition = transform.position;


        if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, player.transform.position) > 1.6f)
        {
            anim.runtimeAnimatorController = runAnim;
            Debug.Log("Attack");
            path.attacking = true;
            float step = speed * Time.deltaTime; // calculate distance to move
            rb2d.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
        }
        else
        {
            anim.runtimeAnimatorController = walkAnim;
            path.attacking = false; //performance !
        }
            


    }
}
