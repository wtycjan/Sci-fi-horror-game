using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public RuntimeAnimatorController walkAnim;
    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController idleAnim;
    NavMeshAgent agent;
    private Sounds sound;
    private float runSpeed = 5f, normalSpeed = 1.5f;
    private float detectionRange = 7f;
    private Vector3 deltaPosition, prevPosition;
    private bool scream = false, charge = false, stop = false, isStay = false;
    public List<Transform> Spots;
    private Transform newSpot;
    private Transform spawnSpot;
    private float timeWaitAndObserve = 3f;

    public VHS.FirstPersonController playerMovement;
    public bool isPlayerOpenDoor = false;
    public GameObject actualDoor;

    void Start()
    {
        prevPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        sound = GetComponent<Sounds>();

        setNewPointDestinationToMoster();

        //random starting position
        spawnMonsterInRandomPlace();

    }

    void Update()
    {
        checkPlayerMovementMode();
        rotateMonster();
        if (makeNewTarget())
        {
            setNewPointDestinationToMoster();
        }
        if(isPlayerOpenDoor)
        {
            prepareMonsterRunToDoor(actualDoor);
        }    
        else if (Vector3.Distance(transform.position, player.transform.position) < detectionRange && Vector3.Distance(transform.position, player.transform.position) > 1.5f)
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

    private void checkPlayerMovementMode()
    {
        if (playerMovement.movementInputData.IsRunning)
        {
            detectionRange = 12f;
        }
        else if(playerMovement.movementInputData.IsCrouching)
        {
            detectionRange = 2f;
        }
        else
        {
            detectionRange = 7f;
        }
    }

    private void spawnMonsterInRandomPlace()
    {
        spawnSpot = Spots[UnityEngine.Random.Range(2, Spots.Count)];
        gameObject.transform.position = spawnSpot.transform.position;
    }

    private void prepareMonsterToWalk()
    {
        agent.speed = normalSpeed;
        anim.runtimeAnimatorController = walkAnim;
        scream = false;
        charge = false;
        isStay = false;
    }

    private void prepareMonsterToStay()
    {
        agent.speed = 0f;
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
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        float step = normalSpeed * 100 * Time.deltaTime; // calculate distance to move
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z), step));
        rotateMonster();
    }

    private void startMonsterRun()
    {
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        float step = normalSpeed * 100 * Time.deltaTime; // calculate distance to move
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
        rotateMonster();
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
            if (agent.speed != runSpeed)
            {
                StartCoroutine("stayAndObserve");
            }
            isPlayerOpenDoor = false;
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
        agent.speed = 0f;
        anim.runtimeAnimatorController = idleAnim;
        stop = true;
        yield return new WaitForSeconds(.4f);
        stop = false;
        charge = true;
    }

    IEnumerator stayAndObserve()
    {
        agent.isStopped = true;
        prepareMonsterToStay();
        yield return new WaitForSeconds(1f);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 180, 0), timeWaitAndObserve);
        yield return new WaitForSeconds(timeWaitAndObserve);
        prepareMonsterToWalk();
        agent.isStopped = false;
    }
}

