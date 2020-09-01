using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public void Interact()
    {
        GameData.level1 = true;
        Destroy(gameObject);
    }
}
