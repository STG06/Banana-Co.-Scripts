using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Flashlight : MonoBehaviour
{
    //spotlight
    public GameObject flashlight;

    //Sound will go here
    private AudioSource audioSource;
    public AudioClip turnOn_Off;

    //bools for if flashlight is on or off
    public bool on;

    //Referencing battery
    private Battery battery;

    private void Start()
    {
        on = false;
        flashlight.SetActive(false);
        //Getting battery component
        battery = GetComponent<Battery>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (on == false && Input.GetKeyDown("f") && battery.flashlightBattery > 0.1)
        {
            //Turn on sound here
            Activate();

            flashlight.SetActive(true);

            on = true;
           

        }
        else if(on == true && Input.GetKeyDown("f"))
        {
            //turn off sound here 
            Activate();

            on = false;

            flashlight.SetActive(false);

        }
        //If battery level reaches 0 we turn off the flashlight 
       else if ( on && battery.flashlightBattery < 0.01f)
        {
            on = false;
            flashlight.SetActive(false);
        }



    }

    private void Activate()
    {
        audioSource.clip = turnOn_Off;
        audioSource.Play();
    }
}
