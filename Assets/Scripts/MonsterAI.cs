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

    //delete this
    public bool isPlayerOpenDoor = false;
    public GameObject actualDoor;

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

    void Update()
    {
        rotateMonster();
        if (makeNewTarget())
        {
            print("2");
            setNewPointDestinationToMoster();
            print("3");
        }
        if(isPlayerOpenDoor)
        {
            print("0");
            prepareMonsterRunToDoor(actualDoor);
            print("1");
        }    
        else if (Vector3.Distance(transform.position, player.transform.position) < 5 && Vector3.Distance(transform.position, player.transform.position) > 1.5f)
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

    private void spawnMonsterInRandomPlace()
    {
        spawnSpot = Spots[UnityEngine.Random.Range(2, Spots.Count)];
        gameObject.transform.position = spawnSpot.transform.position;
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

    public void prepareMonsterRunToDoor(GameObject targetObject)
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
            startMonsterRunToDoor(targetObject);

        }
    }

    private void startMonsterRunToDoor(GameObject targetObject)
    {
        anim.runtimeAnimatorController = runAnim;
        float step = speed * 100 * Time.deltaTime; // calculate distance to move
        rotateMonster();
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z), step));
        print("5");
    }

    private void startMonsterRun()
    {
        anim.runtimeAnimatorController = runAnim;
        float step = speed * 100 * Time.deltaTime; // calculate distance to move
        rotateMonster();
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
        //rbd.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
    }

    private void setNewPointDestinationToMoster()
    {
        newSpot = Spots[UnityEngine.Random.Range(0, Spots.Count)];
        agent.SetDestination(newSpot.transform.position);
        print("6");
    }

    private bool makeNewTarget()
    {
        float radiusAroundTargetPoint = 1f;
        if(agent.remainingDistance <= (agent.stoppingDistance + radiusAroundTargetPoint))
        {
            isPlayerOpenDoor = false;
            return true;
        }
        if(agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            print("8");
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
    IEnumerator WaitAndObserve()
    {
        prepareMonsterToStay();
        yield return new WaitForSeconds(2f);
    }
}

