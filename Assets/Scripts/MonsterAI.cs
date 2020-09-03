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
    public float runSpeed = 5f, normalSpeed = 1.5f;
    public float detectionRange = 2f;
    private Vector3 deltaPosition, prevPosition;
    private bool scream = false, charge = false, stop = false, isStay = false;
    public bool isPlayerDetect = false;
    public List<Transform> Spots;
    private Transform newSpot;
    private Transform spawnSpot;
    private float timeWaitAndObserve = 3f;
    private int startSpotsIndex = 2;

    public VHS.FirstPersonController playerMovement;
    public bool isPlayerOpenDoor = false;
    public GameObject actualDoor;
    [SerializeField] GameObject chest;
    [SerializeField] TypingInput alarm;

    void Start()
    {
        prevPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        sound = GetComponent<Sounds>();
        spawnMonster();

        setNewPointDestinationToMoster();

    }

    private void spawnMonster()
    {
        spawnSpot = Spots[6];
        transform.position = spawnSpot.position;
    }

    void Update()
    {
        checkIsDoorBlocked();
        checkPlayerDetection();
        rotateMonster();
        if(!alarm.isAlarm)
        {
            if (makeNewTarget())
            {
                print("5");
                print(newSpot);
                setNewPointDestinationToMoster();
            }
        }
      
        moveMonster();

    }

    private void moveMonster()
    {
        if (isPlayerOpenDoor)
        {
            print("1");
            prepareMonsterRunToDoor(actualDoor);
        }
        else if (alarm.isAlarm)
        {
            print("2");
            prepareMonsterRunToDoor(chest);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < detectionRange && Vector3.Distance(transform.position, player.transform.position) > 1.5f)
        {
            print("3");
            prepareMonsterToRun();
        }
        else
        {
            print("4");
            if (isStay)
            {
                isPlayerDetect = false;
                prepareMonsterToStay();
            }
            else
            {
                prepareMonsterToWalk();
            }
        }
    }

    public void prepareMonsterCheckAlarm()
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
            startMonsterRunToAlarm();
        }
    }

    private void startMonsterRunToAlarm()
    {
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        float step = normalSpeed * 100 * Time.deltaTime; // calculate distance to move
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(chest.transform.position.x, transform.position.y, chest.transform.position.z), step));
        rotateMonster();
    }


    private void checkIsDoorBlocked()
    {
        if (GameData.door1)
            startSpotsIndex = 0;
        else
            startSpotsIndex = 2;

    }

    private void checkPlayerDetection()
    {
        if (!isPlayerDetect)
            checkPlayerMovementMode();
    }

    private void checkPlayerMovementMode()
    {
        if (playerMovement.movementInputData.HasInput && playerMovement.movementInputData.IsRunning && !playerMovement.movementInputData.IsCrouching)
        {
            detectionRange = 10f;
        }
        else if(playerMovement.movementInputData.HasInput && playerMovement.movementInputData.IsCrouching)
        {
            detectionRange = 2f;
        }
        else if(!playerMovement.movementInputData.HasInput)
        {
            detectionRange = 0f;
        }
        else
        {
            detectionRange = 5f;
        }
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
        isPlayerDetect = true;
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        float step = normalSpeed * 100 * Time.deltaTime; // calculate distance to move
        agent.SetDestination(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
        rotateMonster();
        //rbd.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), step));
    }

    private void setNewPointDestinationToMoster()
    {

        newSpot = Spots[UnityEngine.Random.Range(startSpotsIndex, Spots.Count)];
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

