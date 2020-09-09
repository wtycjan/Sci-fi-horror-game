using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare2 : MonoBehaviour
{
    public GameObject screen;
    public float frequency = 0.2f;
    void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        screen.SetActive(false);
        yield return new WaitForSeconds(frequency);
        screen.SetActive(true);
        yield return new WaitForSeconds(frequency);
        StartCoroutine(Flicker());
    }
}
