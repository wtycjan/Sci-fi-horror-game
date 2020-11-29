using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard2 : MonoBehaviour
{
    public void Interact()
    {
        GameData.keycard1 = true;
        Destroy(gameObject);
    }

}
