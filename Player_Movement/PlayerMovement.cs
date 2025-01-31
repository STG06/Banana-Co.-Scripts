using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    //Character controller of player
    public CharacterController controller;

    //Moving on ground
    public float moveSpeed;

    //Jump Variables
    public float jumpHeight;

    //check current speed
    public float currentSpeed;

    //Stamina
    [HideInInspector] public StaminaBar_Controller staminaController;

    //Jumping
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    bool isGrounded;

    //Accessing our camera 
    public Camera cam;

    //Headbob
    public HeadBob headBob;

    //Sound Effects
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip walkingSound;
    public AudioClip runningSound;
    public AudioClip pantingWhenRunning;
    public AudioClip pantingSound;
    private bool playWalkingSound = false;
    private bool playRunningSound = false;
    private bool panting = false;
   
    public void Start()
    {
        //Stamina
        staminaController = GetComponent<StaminaBar_Controller>(); // Accessing our stamina controller script 
        audioSource.volume = 0.05f;
        audioSource2.volume = 0.05f;
        
    }

    public void Update()
    {

        //Checking if the player is grounded 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;



        //Checking if sprinting
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && Input.GetKey("w"))
        {
            //Stamina Bar Drain
            if (staminaController.playerStamina > 0)
            {
                staminaController.Sprinting();
            }

            if (staminaController.weAreSprinting) //Sprinting
            {
                headBob.Bobbing(9f, 0.05f);
                headBob.CameraRotation(40f, 4.5f);
                StopWalkingSoundEffect();
                RunningSoundEffect();
            }
            else //trying to sprint when you have stamina is regening
            {
                headBob.Bobbing(7f, 0.03f);
                headBob.CameraRotation(40f, 2.5f);
                StopRunningSoundEffect();
                WalkingSoundEffect();
            }
        }
        else if (currentSpeed > 0f && Input.GetKey("w")) // walking forward 
        {
            headBob.Bobbing(7f, 0.03f);
            headBob.CameraRotation(40f, 2.5f);
            staminaController.weAreSprinting = false;
            StopRunningSoundEffect();
            WalkingSoundEffect();
          
        }
        else if(currentSpeed > 0f) // Walking side to side or back
        {
            headBob.Bobbing(7f, 0.05f);
            staminaController.weAreSprinting = false;
            StopRunningSoundEffect();
            WalkingSoundEffect();
           
        }
        else // Idling
        {
            headBob.Bobbing(2f, 0.01f);
            staminaController.weAreSprinting = false;
            StopRunningSoundEffect();
            StopWalkingSoundEffect();
        }

        if (!staminaController.hasRegenerated) // Panting if our stamina isn't regenerated
        {
            PantingSoundEffect();
        }
        else
        {
            StopPantingSoundEffect();
        }
       
        //Moving Player 
        controller.Move(move * moveSpeed * Time.deltaTime);

        //Checking current speed of player 
        currentSpeed = controller.velocity.magnitude;

        //Implementing Gravity to character
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }

    public void SetRunSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void WalkingSoundEffect()
    {
        if (!playWalkingSound)
        {
            audioSource.clip = walkingSound;
            audioSource.loop = true;
            audioSource.Play();
            playWalkingSound = true;
        }
    }

    private void StopWalkingSoundEffect()
    {
        if (playWalkingSound)
        {
            audioSource.Stop();
            playWalkingSound = false;
        }
    }

    private void RunningSoundEffect()
    {
        if (!playRunningSound)
        {
            audioSource2.clip = pantingWhenRunning;
            audioSource.clip = runningSound;

            audioSource2.loop = true;
            audioSource.loop = true;

            audioSource.Play();
            audioSource2.Play(); 

            playRunningSound = true;
        }
    }

    private void StopRunningSoundEffect()
    {
        if (playRunningSound)
        {
            audioSource.Stop();
            audioSource2.Stop();

            playRunningSound = false;
        }
    }

    private void PantingSoundEffect()
    {
        if (!panting)
        {
            audioSource2.clip = pantingSound;
            audioSource2.loop = true;
            audioSource2.volume = 0.5f;
            audioSource2.Play();
            panting = true;
        }
       
    }

    private void StopPantingSoundEffect()
    {
        if (panting)
        {
            audioSource2.Stop();
            audioSource2.volume = 0.05f;
            panting = false;
        }
    }

}