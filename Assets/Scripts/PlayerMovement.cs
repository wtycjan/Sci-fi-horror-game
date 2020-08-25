using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -10f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public Transform cameraControl;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Sounds sounds;
    private MouseLook mouse;
    Vector3 velocity;
    bool isGrounded;
    bool stop = false;
    bool sprint = false;

    private void Start()
    {
        sounds = GetComponentInChildren<Sounds>();
        mouse = GetComponentInChildren<MouseLook>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //walking
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (!stop)
            controller.Move(move * speed * Time.deltaTime);


        //sprint
        if (Input.GetKey(KeyCode.LeftShift) && !sprint && !stop)
        {
            sprint = true;
            speed = 6;
            mouse.mouseSensitivity = 60;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && sprint)
        {
            sounds.Stop();
            sprint = false;
            speed = 2.5f;
            mouse.mouseSensitivity = 140;
        }

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !sprint && !stop && !sounds.IsPlaying())
        {
           if (!sounds.IsPlaying())
             sounds.Sound1Loop();
        }
        else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && sprint)
        {
            if(sounds.audioSource.clip==sounds.sound1)
                sounds.Stop();
            if (!sounds.IsPlaying())
                sounds.Sound2Loop();
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && sounds.IsPlaying())
            sounds.Stop();


        //jump removed
        /*if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //peek/lean

        if (Input.GetKey(KeyCode.Q) && !sprint)
        {
            stop = true;
            sounds.Stop();
            mouse.blockY = true;
            if (cameraControl.localPosition.x>-1f)
            {
                cameraControl.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
                cameraControl.localPosition = new Vector3(cameraControl.localPosition.x - 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
            }

        }
        else if (!Input.GetKey(KeyCode.Q) && cameraControl.localPosition.x < 0)
        {
            stop = false;
            mouse.blockY = false;
            cameraControl.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
            cameraControl.localPosition = new Vector3(cameraControl.localPosition.x + 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
        }

        if (Input.GetKey(KeyCode.E) && !sprint)
        {
            stop = true;
            sounds.Stop();
            mouse.blockY = true;
            if (cameraControl.localPosition.x < 1f)
            {
                cameraControl.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
                cameraControl.localPosition = new Vector3(cameraControl.localPosition.x + 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
            }
        }
        else if (!Input.GetKey(KeyCode.E) && cameraControl.localPosition.x > 0)
        {
            stop = false;
            mouse.blockY = false;
            cameraControl.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
            cameraControl.localPosition = new Vector3(cameraControl.localPosition.x - 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
        }


    }

}