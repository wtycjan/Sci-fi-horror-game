using UnityEngine;
using System.Collections;

public class FlickeringLights : MonoBehaviour {
	
	public Light flashingLight;
	// public Light secondFlashingLight;
	private float randomNumber;
	public float speed = 0.95f;
    public float frequency = 1;
    void Start(){
		
		flashingLight.enabled = true;
		//secondFlashingLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        Flash();
	}
    private void Flash()
    {
        randomNumber = Random.value;
        if (randomNumber <= speed)
        {
            flashingLight.enabled = true;
            // secondFlashingLight.enabled = true;
        }
        else
        {
            flashingLight.enabled = false;
            //  secondFlashingLight.enabled = false;

        }
    }
}﻿