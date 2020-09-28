using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime/16, Space.Self);
    }
}
