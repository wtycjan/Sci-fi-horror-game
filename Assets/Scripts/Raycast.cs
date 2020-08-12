using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private GameObject raycastedObj;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    [SerializeField] private Image uiCrosshair;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
        {
            if(hit.collider.CompareTag("Interactable"))
            {
                raycastedObj = hit.collider.gameObject;
                CrosshairActive();

                if(Input.GetKeyDown("e"))
                {
                    Debug.Log("Open Door");
                    raycastedObj.SendMessage("Interact");
                }
            }
        }
        else
        {
            //Performance optimization needed here
            CrosshairNormal();
        }
    }

    void CrosshairActive()
    {
        uiCrosshair.color = Color.yellow;
    }
    void CrosshairNormal()
    {
        uiCrosshair.color = Color.white;
    }
}
