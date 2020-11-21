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
    NetworkServerUI network;
    NavMeshAgent agent;
    //public NavMeshSurface surface;
    private Sounds sound;
    public float runSpeed = 7f, normalSpeed = 1.5f;
    public float detectionRange = 2f;
    private Vector3 deltaPosition, prevPosition;
    private bool scream = false, charge = false, stop = false, isStay = false;
    public bool isPlayerDetect = false, isRunning = false;
    public List<Transform> Spots;
    private Transform newSpot;
    private Transform spawnSpot;
    private float timeWaitAndObserve = 3f;
    private int startSpotsIndex = 2;

    public VHS.FirstPersonController playerMovement;
    public bool isPlayerOpenCloseDoor = false;
    public GameObject actualDoor;
    [SerializeField] GameObject chest;
    [SerializeField] Chest chestAlarm;
    [SerializeField] GameObject computer;
    [SerializeField] Tablet computerAlarm;
    [SerializeField] GameObject terminal;
    [SerializeField] Lockpicking terminalAlarm;
    [SerializeField] SkinnedMeshRenderer monsterMesh;
    [SerializeField] Renderer monsterRenderer;
    [SerializeField] List<Material> transparentShader;
    [SerializeField] List<Material> visibleShader;
    Material[] visibleMaterials;
    Material[] transparetMaterials;
    private bool isDoorCloseInFrontOfMonster = false;


    void Start()
    {
        visibleMaterials = visibleShader.ToArray();
        transparetMaterials = transparentShader.ToArray();
        prevPosition = transform.position;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //GetComponent<NavMeshSurface>().BuildNavMesh(); 
        sound = GetComponent<Sounds>();
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
        spawnMonster();
        if (!GameData.respawn)
        {
            StartCoroutine(waitWithStartMonsterMovewmentDruginIntro());
        }
           
        else
        {
            spawnMonster();
            setNewPointDestinationToMoster();
        }
    }
    private IEnumerator waitWithStartMonsterMovewmentDruginIntro()
    {
        yield return new WaitForSeconds(9f);
        spawnMonster();
        setNewPointDestinationToMoster();
    }


    private void spawnMonster()
    {
        spawnSpot = Spots[UnityEngine.Random.Range(5, Spots.Count)];
        transform.position = spawnSpot.position;
    }

    void Update()
    {
        if(GameData.isGameActive)
        {
            checkIsDoorCloseInFrontOfMontser();
            checkIsDoorBlocked();
            checkPlayerDetection();
            rotateMonster();
            changeMonsterShader();
            if (!chestAlarm.isAlarm && !computerAlarm.isAlarm && !terminalAlarm.isAlarm)
            {
                if (makeNewTarget())
                {
                    setNewPointDestinationToMoster();
                }
            }
            moveMonster();
            HandleState();
        }
        
    }

    private void checkIsDoorCloseInFrontOfMontser()
    {
        isDoorCloseInFrontOfMonster = GetComponent<OpenDoorHandler>().isDoorCloseInFronfOfMonster;
    }

    private void changeMonsterShader()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 2.1f && isPlayerDetect && Math.Abs(transform.position.y- player.transform.position.y) < 2 )
        {
            monsterMesh.materials = visibleMaterials;
        }
        else
        {
            monsterMesh.materials = transparetMaterials;
        }
    }

    private void moveMonster()
    {
        if (isStay)
        {
            isRunning = false;
            isPlayerDetect = false;
            prepareMonsterToStay();
        }
        else if(isPlayerOpenCloseDoor|| chestAlarm.isAlarm || computerAlarm.isAlarm || terminalAlarm.isAlarm || isPlayerDetect
            || (Vector3.Distance(transform.position, player.transform.position) < detectionRange 
            && Vector3.Distance(transform.position, player.transform.position) > 1.5f && Math.Abs(transform.position.y - player.transform.position.y) < 1.5f))
            responseMonsterToTrigger();
        else
        {
            prepareMonsterToWalk();
        }
    }

    private void responseMonsterToTrigger()
    {
        reposnseMonsterToAlarm();
        responseMonsterToPlayer();

    }

    private void responseMonsterToPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange && Vector3.Distance(transform.position, player.transform.position) > 1.5f
            && Math.Abs(transform.position.y - player.transform.position.y) < 1.5f)
        {
            prepareMonsterToRun();
        }
        if (isPlayerDetect && isPlayerOpenCloseDoor)
        {
            prepareMosterIfDoorIsCloseInFrontOfHim();
        }
        else if (isPlayerOpenCloseDoor)
        {
            prepareMonsterRunToDoor(actualDoor);
        }
    }

    private void prepareMosterIfDoorIsCloseInFrontOfHim()
    {
        isPlayerOpenCloseDoor = false;
        isRunning = true;
        GameObject playerPositionBeforeDoorClose = player;
        startMonsterRunToAlarm(playerPositionBeforeDoorClose);
    }

    private void reposnseMonsterToAlarm()
    {
        if (chestAlarm.isAlarm)
        {
            prepareMonsterCheckAlarm(chest);
            turnOffAlarm();
        }
        if (computerAlarm.isAlarm)
        {
            prepareMonsterCheckAlarm(computer);
            turnOffAlarm();
        }
        if (terminalAlarm.isAlarm)
        {
            prepareMonsterCheckAlarm(terminal);
            turnOffAlarm();
        }
    }

    void turnOffAlarm()
    {
        if (Vector3.Distance(transform.position, chest.transform.position) < 1.5f)
        {
            chestAlarm.StopAlarm();
            StartCoroutine("stayAndObserve");
        }
        if (Vector3.Distance(transform.position, computer.transform.position) < 1.5f)
        {
            computerAlarm.StopAlarm();
            StartCoroutine("stayAndObserve");
        }
        if (Vector3.Distance(transform.position, terminal.transform.position) < 2f)
        {
            terminalAlarm.StopAlarm();
            StartCoroutine("stayAndObserve");
        }
    }

    public void prepareMonsterCheckAlarm(GameObject alarmSource)
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
            startMonsterRunToAlarm(alarmSource);
        }
    }

    private void startMonsterRunToAlarm(GameObject alarmSource)
    {
 
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        float step = normalSpeed * 100 * Time.deltaTime; // calculate distance to move
        agent.SetDestination(alarmSource.transform.position);
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
        if (!isPlayerDetect || isRunning)
            checkPlayerMovementMode();
    }

    private void checkPlayerMovementMode()
    {
        if (playerMovement.movementInputData.HasInput && playerMovement.movementInputData.IsRunning && !playerMovement.movementInputData.IsCrouching)
        {
            if(computerAlarm.isAlarm || terminalAlarm.isAlarm || chestAlarm.isAlarm)
            {
                detectionRange = 6f;
            }
            else
            {
                detectionRange = 12f;
            }

        }
        else if ((playerMovement.movementInputData.HasInput && playerMovement.movementInputData.IsCrouching && !playerMovement.isHoldingBreath) || (!playerMovement.movementInputData.HasInput && !playerMovement.isHoldingBreath))
        {
            if (computerAlarm.isAlarm || terminalAlarm.isAlarm || chestAlarm.isAlarm)
            {
                detectionRange = 2.15f;
            }
            else
            {
 
                detectionRange = 4.3f;
            }
  
        }
        else if ((!playerMovement.movementInputData.HasInput && playerMovement.isHoldingBreath) || (playerMovement.movementInputData.IsCrouching && playerMovement.isHoldingBreath) )
        {
            detectionRange = 0f;
        }
        else
        {
            if (computerAlarm.isAlarm || terminalAlarm.isAlarm || chestAlarm.isAlarm)
            {
                detectionRange = 3.35f;
            }
            else
            {
                detectionRange = 6.7f;
            }
       
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
        isPlayerDetect = false;
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        agent.SetDestination(targetObject.transform.position);
        rotateMonster();
    }

    private void startMonsterRun()
    {
        isPlayerDetect = true;
        agent.speed = runSpeed;
        anim.runtimeAnimatorController = runAnim;
        agent.SetDestination(player.transform.position);
        rotateMonster();
    }

    private void setNewPointDestinationToMoster()
    {
        isPlayerDetect = false;
        isRunning = false;
        newSpot = Spots[UnityEngine.Random.Range(startSpotsIndex, Spots.Count)];
        print(newSpot);
        agent.SetDestination(newSpot.transform.position);
    }

    private bool makeNewTarget()
    {
        float radiusAroundTargetPoint = 1.5f;
        if(agent.remainingDistance <= (agent.stoppingDistance + radiusAroundTargetPoint))
        {
            if (agent.speed != runSpeed && !chestAlarm.isAlarm && !computerAlarm.isAlarm && !terminalAlarm.isAlarm)
            {
                StartCoroutine("stayAndObserve");
            }
            isPlayerOpenCloseDoor = false;
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
    protected void HandleState()
    {

        if (anim.runtimeAnimatorController == idleAnim)
            network.ServerSendMessage("MonsterState1");  //stand
        else if (anim.runtimeAnimatorController == runAnim)
            network.ServerSendMessage("MonsterState3");  //sprint
        else
            network.ServerSendMessage("MonsterState2");  //walk
    }
}

