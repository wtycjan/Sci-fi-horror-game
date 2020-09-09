using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EndCredits : MonoBehaviour
{
    public GameObject text1, text2, text3, text4, text5, interactText, horror;
    public MonsterAI monster;
    private GameObject player;
    private Sounds sound, music;
    private GameController gameController;
    void Start()
    {
        sound = GameObject.FindGameObjectWithTag("SoundController").GetComponent<Sounds>();
        music = GameObject.FindGameObjectWithTag("MusicController").GetComponent<Sounds>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        sound.Stop();
        music.GetComponent<MusicController>().enabled = false;
        StartCoroutine(AudioFadeOut.FadeOut(music.audioSource, .3f));
        StartCoroutine("Ending");
        interactText.SetActive(false);
        GameData.canPause = false;
        horror.SetActive(false);
        monster.gameObject.SetActive(false);
    }

    public IEnumerator Ending()
    {
        gameController.cutscene = true;
        MonoBehaviour[] scripts = player.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour c in scripts)
        {
            if (c == null || c.gameObject.tag == "MainCamera")
            {
                continue;
            }

            c.enabled = false;
        }
        yield return new WaitForSeconds(.5f);
        music.audioSource.volume = 1;
        music.Sound2();
        text1.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        text1.SetActive(false);
        yield return new WaitForSeconds(.5f);
        text2.SetActive(true);
        yield return new WaitForSeconds(5f);
        text2.SetActive(false);
        yield return new WaitForSeconds(.5f);
        text3.SetActive(true);
        yield return new WaitForSeconds(3f);
        text3.SetActive(false);
        yield return new WaitForSeconds(.5f);
        text4.SetActive(true);
        yield return new WaitForSeconds(3f);
        text4.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        text5.SetActive(true);
        yield return new WaitForSeconds(5f);
        text5.SetActive(false);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

}
