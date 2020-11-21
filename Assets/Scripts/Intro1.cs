using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Intro1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject elevatorDoor;
    private NetworkServerUI network;
    private Sounds sound;
    public Image blackScreen; //intro
    void Start()
    {
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
            sound.Sound5();
            blackScreen.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(10);
        }
        yield return new WaitForSeconds(1f);
        Destroy(blackScreen.gameObject);
        elevatorDoor.GetComponent<OpenDoorButton>().UnlockDoorIntro();
        GameData.canPause = true;
        network.ServerSendMessage("Unpause");
        GameData.isGameActive = true;


    }
}
