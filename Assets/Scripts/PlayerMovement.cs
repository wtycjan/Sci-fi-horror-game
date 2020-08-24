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
    Vector3 velocity;
    bool isGrounded;
    bool stop = false;
    bool sprint = false;

    private void Start()
    {
        sounds = GetComponentInChildren<Sounds>();
        StartCoroutine(Footsteps());
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (!stop)
            controller.Move(move * speed * Time.deltaTime);


        //jump removed
        /*if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.Q))
        {
            stop = true;
            if (cameraControl.localPosition.x>-1f)
            {
                cameraControl.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
                cameraControl.localPosition = new Vector3(cameraControl.localPosition.x - 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
            }

        }
        else if (!Input.GetKey(KeyCode.Q) && cameraControl.localPosition.x < 0)
        {
            stop = false;
            cameraControl.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
            cameraControl.localPosition = new Vector3(cameraControl.localPosition.x + 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
        }

        if (Input.GetKey(KeyCode.E))
        {
            stop = true;
            if (cameraControl.localPosition.x < 1f)
            {
                cameraControl.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
                cameraControl.localPosition = new Vector3(cameraControl.localPosition.x + 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
            }
        }
        else if (!Input.GetKey(KeyCode.E) && cameraControl.localPosition.x > 0)
        {
            stop = false;
            cameraControl.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
            cameraControl.localPosition = new Vector3(cameraControl.localPosition.x - 3.5f * Time.deltaTime, cameraControl.localPosition.y, cameraControl.localPosition.z);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !sprint)
        {
            sprint = true;
            speed =6;
        }
        else if(!Input.GetKey(KeyCode.LeftShift) && sprint)
        {
            sprint = false;
            speed =2.5f;
        }
    }

    private IEnumerator Footsteps()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (sprint)
            {
                sounds.SoundRandom(1);
            }
            else
            {
                sounds.SoundRandom(.55f);
                yield return new WaitForSeconds(.5f);
                
            }
        }
        yield return new WaitForSeconds(.35f);
        StartCoroutine(Footsteps());
    }
}