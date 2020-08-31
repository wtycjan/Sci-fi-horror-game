using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Lockpicking2 : MonoBehaviour
{
    public Button qButton;
    public Button wButton;
    public Button eButton;
    public int securityLvl = 1;
    private int blocks;
    void Start()
    {
        blocks = 3 * securityLvl + 2;
    }

    // Update is called once per frame
    void Update()
    {
        var pointer = new PointerEventData(EventSystem.current);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            ExecuteEvents.Execute(qButton.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            ExecuteEvents.Execute(wButton.gameObject, pointer, ExecuteEvents.submitHandler);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            ExecuteEvents.Execute(eButton.gameObject, pointer, ExecuteEvents.submitHandler);
        }
    }
}
