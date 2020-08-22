﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    Rigidbody rbd;
    public RuntimeAnimatorController walkAnim;
    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController idleAnim;
    NavMeshAgent agent;
    private Sounds sound;
    public float speed = 3f;
    private Vector3 deltaPosition, prevPosition;
    private bool scream = false, charge = false, stop = false;
    public List<Transform> Spots;
    private Transform newSpot;

    void Start()
    {
        prevPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rbd = GetComponent<Rigidbody>();
        sound = GetComponent<Sounds>();

        newSpot = Spots[UnityEngine.Random.Range(0, Spots.Count)];
        agent.SetDestination(newSpot.transform.position);
    }

    void Update()
    {

        rotateMonster();
        if (makeNewTarget()) 
        {
            print("reach");
            newSpot = Spots[UnityEngine.Random.Range(0, Spots.Count)];
            agent.SetDestination(newSpot.transform.position);
        }

        
        if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, player.transform.position) > 1.6f)
        {
            rotateMonster();
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
                
                rbd.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
            }

        }
        else
        {
            anim.runtimeAnimatorController = walkAnim;
            scream = false;
            charge = false;
        }



    }

    private bool makeNewTarget()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }
        if(agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    private void rotateMonster()
    {
        deltaPosition = transform.position - prevPosition;
        if (deltaPosition != Vector3.zero && !stop)
        {
            transform.forward = deltaPosition;
        }
        prevPosition = transform.position;
    }
    IEnumerator Prepare()
    {
        stop = true;
        anim.runtimeAnimatorController = idleAnim;
        yield return new WaitForSeconds(.4f);
        stop = false;
        charge = true;
    }



}

