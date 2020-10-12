using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomName : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject room;
    bool isRunning = false;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && GameData.canPause && text.text != gameObject.name)   //not in cinematic + not in the same room but different colliders
        {
            StartCoroutine(ShowText());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameData.canPause && text.text != gameObject.name)
        {
            StopAllCoroutines();
            
        }
    }


    private void Start()
    {
        text = room.GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowText()
    {
        room.SetActive(false);
        text.text = gameObject.name;
        room.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        room.SetActive(false);
    }
}
