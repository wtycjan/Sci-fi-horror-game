using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VHS;
public class Intro1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject elevatorDoor;
    private FirstPersonController player;
    private NetworkServerUI network;
    private Sounds sound;
    public Image blackScreen; //intro
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
        //*************************
        //Enable before building
        StartCoroutine("Intro");
        //*************************
    }

    public IEnumerator Intro()
    {
        if (!GameData.respawn)
        {
            yield return new WaitForSeconds(.1f);
            MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour c in scripts)
            {
                if (c == null || c.gameObject.tag == "MainCamera")  //postprocess
                {
                    continue;
                }

                c.enabled = false;
            }
            sound.Sound5();
            blackScreen.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(10);
            foreach (MonoBehaviour c in scripts)
            {
                if (c == null || c.gameObject.tag == "MainCamera")  //postprocess
                {
                    continue;
                }

                c.enabled = true;
            }
        }
        yield return new WaitForSeconds(1f);
        Destroy(blackScreen.gameObject);
        elevatorDoor.GetComponent<OpenDoorButton>().UnlockDoorIntro();
        GameData.canPause = true;
        network.ServerSendMessage("Unpause");
        GameData.isGameActive = true;


    }
}
