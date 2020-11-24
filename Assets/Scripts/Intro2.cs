using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Intro2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject elevatorDoor;
    [SerializeField] private GameObject explosion, sparks1, sparks2;
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
            sound.Sound11();
            blackScreen.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(4.5f);
            sound.Sound9();
            yield return new WaitForSeconds(1.5f);
            sound.Sound10();
            yield return new WaitForSeconds(4);
        }
        yield return new WaitForSeconds(.5f);
        sound.Sound9();
        explosion.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Destroy(blackScreen.gameObject);
        elevatorDoor.GetComponent<OpenDoorButton>().UnlockDoorIntro();
        GameData.canPause = true;
        network.ServerSendMessage("Unpause");
        GameData.isGameActive = true;
        yield return new WaitForSeconds(1f);
        sparks1.SetActive(true);
        yield return new WaitForSeconds(3f);
        sparks2.SetActive(true);
        Destroy(gameObject);

    }
}
