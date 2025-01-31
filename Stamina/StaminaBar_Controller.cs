using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar_Controller : MonoBehaviour
{
    [Header("stamina Main Parameters")]
    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [HideInInspector] public bool hasRegenerated = true;
    [HideInInspector] public bool weAreSprinting = false;

    [Header("Stamina Regen Parameters")]
    [SerializeField] private float staminaDrain;
    [SerializeField] private float staminaRegen;

    [Header("Stamina Speed Parameters")]
    public float walkSpeed;
    public float RunSpeed;

    [Header("Stamina uI Elements")]
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    private PlayerMovement playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerMovement>();
        
    }

    private void Update()
    {
        if (!weAreSprinting) // If we are not sprinting 
        {
            if(playerStamina <= maxStamina) // And if our player stamina is not full yet 
            {
                playerStamina += staminaRegen * Time.deltaTime; // We regen our stamina 
                UpdateStamina(); // Showing the UI and updating our stamina bar 
                playerController.SetRunSpeed(walkSpeed); // Setting our speed to walk speed
            }
            
            //If our player stamina is equal to our max stamina we set our run speed to our walk speed and hide the ui
            if (playerStamina >= maxStamina)
            {
                playerController.SetRunSpeed(walkSpeed);
                sliderCanvasGroup.alpha = 0; // Hiding the UI
                hasRegenerated = true; // Setting we have regenerated to true, so we are now able to sprint  
            }
        }
    }

    public void Sprinting() // If we input shift this function is activated (used in my player movement script)
    {
        if (hasRegenerated) // If we have regenerated
        {
            weAreSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime; // Draining Stamina 
            UpdateStamina(); // Showing the UI and updating the stamina bar 
            playerController.SetRunSpeed(RunSpeed); // Setting our speed to run speed 

            if(playerStamina <= 0) // If our player stamina equals 0
            {
                weAreSprinting = false; // We set the bool to false
                hasRegenerated = false; // We set has regenerated to false
                playerController.SetRunSpeed(walkSpeed); // And we set our speed to walk speed
                sliderCanvasGroup.alpha = 0; //Hiding the UI
            }
        }
    }
    void UpdateStamina() // Void used for updating the stamina bar
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina; // Updating our stamina bar 
        sliderCanvasGroup.alpha = 1; // Showing our UI
    }
}
