using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    //Still need to add a Battery increase system on this script


    [Header("Battery Main Parameters")]
    public float flashlightBattery = 100.0f;
    public float maxBattery = 100.0f;


    //Here put collecting a battery item increasing the battery life

    [Header("Battery Regen/Drain Parameters")]
    public float batteryDrain = 1.5f;
    public float batteryRegen = 10f;

    [Header("Battery UI Elements")]
    [SerializeField] private Image batteryProgressUI = null;
    [SerializeField] private CanvasGroup batteryCanvasGroup = null;

    //Accessing flashlight
    private Flashlight flashlight;

    public void Start()
    {
        flashlight = GetComponent<Flashlight>();
    }

    public void Update()
    {

       
        if(flashlight.on == true)
        {
            //Battery is Draining by the batteryDrain amount
            flashlightBattery -= batteryDrain * Time.deltaTime;
            //Updating Battery amount
            UpdateBattery();
            //Player is able to see the battery bar
            batteryCanvasGroup.alpha = 1;
           
        }
        else if(flashlight.on == false)
        {
            //Making the player unable to see the Battery 
            batteryCanvasGroup.alpha = 0;
        }
        
    }
   
    public void UpdateBattery()
    {
        //Updating how much Battery is left
        batteryProgressUI.fillAmount = flashlightBattery / maxBattery;
    }
}
