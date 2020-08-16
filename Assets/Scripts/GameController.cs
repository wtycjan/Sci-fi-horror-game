using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;
    public Image blackScreen;
    public GameObject monster;
    public GameObject player;
    public RuntimeAnimatorController jumpAnim;
    private bool cutscene = false, cameraCutscene = false;
    Quaternion startRot, endRot;
    private void Update()
    {
        //Debug only!
        if (Input.GetKeyDown("1"))
            OpenDoor1();
        if (Input.GetKeyDown("2"))
            OpenDoor2();
        if (Input.GetKeyDown("3"))
            OpenDoor3();
        if (Input.GetKeyDown("4"))
            OpenDoor4();

        //death
        if (Vector3.Distance(monster.transform.position, player.transform.position) < 1.6f && !cutscene)
        {
            StartCoroutine("Death");
        }
        if (player.transform.rotation != endRot && cameraCutscene)
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, endRot, Time.deltaTime * 1.5f);

    }

    void OpenDoor1()
    {
        door1.SendMessage("Interact");
    }
    void OpenDoor2()
    {
        door2.SendMessage("Interact");
    }
    void OpenDoor3()
    {
        door3.SendMessage("Interact");
    }
    void OpenDoor4()
    {
        door4.SendMessage("Interact");
    }
    public IEnumerator Death()
    {
        cutscene = true;
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            c.enabled = false;
        }
        MonoBehaviour[] scripts2 = monster.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts2)
        {
            c.enabled = false;
        }
        Vector3 lookpoint = monster.GetComponentInChildren<BoxCollider>().transform.position;
        player.transform.LookAt(lookpoint);
        lookpoint = player.GetComponentInChildren<BoxCollider>().transform.position;
        monster.transform.LookAt(lookpoint);
        player.GetComponent<CharacterController>().enabled = false;
        monster.GetComponentInChildren<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(.05f);
        monster.GetComponent<Animator>().runtimeAnimatorController = jumpAnim;
        monster.GetComponent<Rigidbody>().AddForce(monster.transform.forward* 100);
        yield return new WaitForSeconds(.3f);
        monster.gameObject.SetActive(false);
        endRot = Quaternion.LookRotation(new Vector3(0, 54,0));
        cameraCutscene = true;
        //player.transform.forward = new Vector3(transform.rotation.x, 54, transform.rotation.z);
        yield return new WaitForSeconds(.7f);
        blackScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
