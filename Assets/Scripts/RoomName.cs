using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomName : MonoBehaviour
{
    private Text text;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject room;


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            StartCoroutine(ShowText());
        }
    }


    private void Start()
    {
        text = room.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowText()
    {
        text.text = gameObject.name;
        room.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        room.SetActive(false);
    }
}
