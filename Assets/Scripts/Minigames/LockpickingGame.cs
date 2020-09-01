using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockpickingGame : MonoBehaviour
{
    public GameObject prefabBall;
    GameObject[] gameObjects;
    public GameObject player;
    public NetworkServerUI network;
    public Image codeScreen;
    bool lockpicking = false;
    private int open;
    private int ball;

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
    }
    void BeginGame(int x)
    {
        //destroy previously made balls
        DestroyBalls();
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null)
            {
                continue;
            }
            c.enabled = false;
        }
        network.ServerSendMessage("OpenLockpicking");
        //creatte neww balls
        ball = x * 3 + 2;
        StartCoroutine("BeginGame2");
        
    }
    public void DestroyBalls()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Ball");
        for (var i = 0; i < gameObjects.Length; i++)
            Destroy(gameObjects[i]);
    }
    void StopGame()
    {
        lockpicking = false;
        if(open==0)
        {
            GameData.door1 = true;
        }
        Close();
    }
    private IEnumerator BeginGame2()
    {
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(BallHandle(ball));
        yield return new WaitForSeconds(1f);
        lockpicking = true;
    }

    private IEnumerator BallHandle(int x)
    {
            yield return new WaitForSeconds(.8f);
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
                btn.transform.localPosition = new Vector2(btn.transform.localPosition.x, btn.transform.localPosition.y - 5f);
                yield return new WaitForSeconds(.01f);
            }

            Destroy(btn);
    }

}
