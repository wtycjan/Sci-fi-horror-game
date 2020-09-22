using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class LockpickingGame : MonoBehaviour
{
    public GameObject prefabBall;
    GameObject[] gameObjects;
    public GameObject player;
    private NetworkServerUI network;
    public Image codeScreen;
    private Sounds sound;
    bool lockpicking = false;
    public int open;
    private int ball;

    private void Awake()
    {
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
    }
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();

    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();

        if (lockpicking)
        {
            gameObjects = GameObject.FindGameObjectsWithTag("Ball");
            if (gameObjects.Length == 0)
                StopGame();
        }
    }
    public void Close()
    {
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = true;
        }
        codeScreen.gameObject.SetActive(false);
        network.ServerSendMessage("CloseLockpicking");
        GameData.canPause = true;
    }
    void BeginGame(int x)
    {
        //destroy previously made balls
        DestroyBalls();
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null || c.gameObject.tag == "MainCamera")
            {
                continue;
            }
            c.enabled = false;
        }
        network.ServerSendMessage("OpenLockpicking");
        //creatte neww balls
        ball = x * 3 + 2;
        open = ball;
        StartCoroutine("BeginGame2");
        GameData.canPause = false;

    }
    public void DestroyBalls()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Ball");
        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }
    public void StopGame()
    {
        lockpicking = false;
        if(open==0)
        {
            GameData.door1 = true;
            network.ServerSendMessage("UnlockDoor1");
            sound.Sound8();
        }
        Close();
    }
    private IEnumerator BeginGame2()
    {
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(BallHandle(ball));
        yield return new WaitForSeconds(1.5f);
        lockpicking = true;
    }

    private IEnumerator BallHandle(int x)
    {
            yield return new WaitForSeconds(1.3f);
            GameObject btn = Instantiate(prefabBall, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            btn.transform.SetParent(transform);
            int random = Random.Range(0, 3);
            switch (random)
            {
                case 0:
                    btn.transform.localPosition = new Vector2(-180, 230);
                    break;
                case 1:
                    btn.transform.localPosition = new Vector2(0, 230);
                    break;
                case 2:
                    btn.transform.localPosition = new Vector2(180, 230);
                    break;
            }
            //check if spawning another ball
            ball--;
            if (ball > 0)
                StartCoroutine(BallHandle(ball));
            
            //move ballw
            while (btn.transform.localPosition.y > -230)
            {
                btn.transform.localPosition = new Vector2(btn.transform.localPosition.x, btn.transform.localPosition.y - 2.5f);
                yield return new WaitForSeconds(.01f);
            }

            Destroy(btn);
    }


}
