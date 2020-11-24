using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public MonsterAI monster;
    private GameObject player;
    private Sounds sound, music;
    private GameController gameController;
    private NetworkServerUI network;
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        music = GameObject.FindGameObjectWithTag("MusicController").GetComponent<Sounds>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        network = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkServerUI>();
        sound.Stop();
        sound.Sound5();
        music.GetComponent<MusicController>().enabled = false;
        StartCoroutine(AudioFadeOut.FadeOut(music.audioSource, .3f));
        StartCoroutine("Ending");
        GameData.canPause = false;
        monster.gameObject.SetActive(false);
    }
    public IEnumerator Ending()
    {
        network.ServerSendMessage("Pause");
        gameController.cutscene = true;
        yield return new WaitForSeconds(2.5f);
        network.CloseServer();
        yield return new WaitForSeconds(.5f);
        GameData.loadLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(1);
    }
    }
