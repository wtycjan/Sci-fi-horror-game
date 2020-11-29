using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public int seconds;
    void Start()
    {
        Destroy(gameObject, seconds);
    }


}
