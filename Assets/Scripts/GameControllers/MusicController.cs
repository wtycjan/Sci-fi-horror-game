using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    private Sounds music;
    public MonsterAI monster;
    private bool chase = false;
    void Start()
    {
        music = GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (monster.isPlayerDetect &&!chase)
        {
            music.Stop();
            music.audioSource.volume = 1;
            music.Sound2();
            chase = true;
        }
        else if(!monster.isPlayerDetect && chase)
        {
            StartCoroutine(AudioFadeOut.FadeOut(music.audioSource, .4f));
            StartCoroutine(Wait());
            chase = false;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(.4f);
        music.audioSource.volume = .1f;
        music.Sound1Loop();
    }
}
