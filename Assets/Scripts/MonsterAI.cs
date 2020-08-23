using System;
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
    private bool scream = false, charge = false, stop = false, isStay = false;
    public List<Transform> Spots;
    private Transform newSpot;
    private Transform spawnSpot;

    void Start()
    {
        prevPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rbd = GetComponent<Rigidbody>();
        sound = GetComponent<Sounds>();

        setNewPointDestinationToMoster();

        //random starting position
        spawnMonsterInRandomPlace();

    }

    private void spawnMonsterInRandomPlace()
    {
        spawnSpot = Spots[UnityEngine.Random.Range(2, Spots.Count)];
        gameObject.transform.position = spawnSpot.transform.position;
    }

    void Update()
    {
        rotateMonster();
        if (makeNewTarget())
        {
            setNewPointDestinationToMoster();
        }


        if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, player.transform.position) > 1.5f)
        {
            prepareMonsterToRun();
        }
        else
        {
            if(isStay)
            {
                prepareMonsterToStay();
            }
            else
            {
                prepareMonsterToWalk();
            }
        }



    }

    private void prepareMonsterToWalk()
    {
        anim.runtimeAnimatorController = walkAnim;
        scream = false;
        charge = false;
        isStay = false;
    }

    private void prepareMonsterToStay()
    {
        print("I worked");
        anim.runtimeAnimatorController = idleAnim;
        scream = false;
        charge = false;
        isStay = true;
    }

    private void prepareMonsterToRun()
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
            startMonsterRun();
        }
    }

    private void startMonsterRun()
    {
        anim.runtimeAnimatorController = runAnim;
        Debug.Log("Attack");
        float step = speed * 100 * Time.deltaTime; // calculate distance to move
        rotateMonster();
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
        //rbd.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
    }

    private void setNewPointDestinationToMoster()
    {
        newSpot = Spots[UnityEngine.Random.Range(0, Spots.Count)];
        agent.SetDestination(newSpot.transform.position);
    }

    private bool makeNewTarget()
    {
        float radiusAroundTargetPoint = 1f;
        if(agent.remainingDistance <= (agent.stoppingDistance + radiusAroundTargetPoint))
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

