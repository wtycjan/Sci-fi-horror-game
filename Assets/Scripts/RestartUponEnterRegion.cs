using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartUponEnterRegion : MonoBehaviour
{
    private GameController gameController;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            gameController.StartCoroutine("Restart");
        }
    }
}
