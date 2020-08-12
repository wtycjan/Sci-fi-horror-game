using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvents : MonoBehaviour
{
    public GameObject monster;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        monster.SetActive(true);
    }
}
