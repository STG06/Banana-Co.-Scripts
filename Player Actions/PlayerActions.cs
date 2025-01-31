using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Camera cam;

    //Door 
    [SerializeField] LayerMask doorLayer;
    [SerializeField] private TextMeshPro doorText;
    [SerializeField] private float MaxDoorUseDistance = 5f; // From how far away we can see the text

    //Banana 
    [SerializeField] private TextMeshPro bananaText;
    [SerializeField] private float MaxBananaUseDistance = 5f;

    //Battery 
    [SerializeField] private TextMeshPro batteryText;
    [SerializeField] private float MaxBatteryUseDistance = 5f;

    public Banana banana;
    public Battery_Item battery;


    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown("e")) 
        {
            UseDoor(); 
            GrabBanana();
            GrabBattery();
        }

        //Door
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, MaxDoorUseDistance, doorLayer) &&
            hit.collider.TryGetComponent<Door>(out Door door))
        {
            if (door.isOpen)
            {
                doorText.SetText("Close \"E\""); // If the door is open the "Close E" text pops up
            }
            else
            {
                doorText.SetText("Open \"E\""); // If the door is closed the "Open E" text pops up
            }
            doorText.gameObject.SetActive(true);
            doorText.transform.position = hit.point - (hit.point - cam.transform.position).normalized * 1f; // The texts position will pop up where we hit the door
            doorText.transform.rotation = Quaternion.LookRotation((hit.point - cam.transform.position).normalized); // The text will be looking towards the player

        }
        else
        {
            doorText.gameObject.SetActive(false);
        }

        //Grabbing Banana
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitBanana, MaxBananaUseDistance) && hitBanana.collider.gameObject.name == "Banana")
        {
            
            bananaText.SetText("Press \"E\" to Pick Up");
            bananaText.gameObject.SetActive(true);
            bananaText.transform.position = hitBanana.point - (hitBanana.point - cam.transform.position).normalized * 1f;
            bananaText.transform.rotation = Quaternion.LookRotation((hitBanana.point - cam.transform.position).normalized);
        }
        else
        {
            bananaText.gameObject.SetActive(false);
        }

        //Grabbing Battery
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitBattery, MaxBatteryUseDistance) && hitBattery.collider.gameObject.name == "Battery")
        {
            batteryText.SetText("Press \"E\" to Pick Up");
            batteryText.gameObject.SetActive(true);
            batteryText.transform.position = hitBattery.point - (hitBattery.point - cam.transform.position).normalized * 1f;
            batteryText.transform.rotation = Quaternion.LookRotation((hitBattery.point - cam.transform.position).normalized);
        }
        else
        {
            batteryText.gameObject.SetActive(false);
        }
    }

    public void UseDoor() // The function for closing and opening the door 
    {
        //If we are looking at a door with the door layer 
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, MaxDoorUseDistance, doorLayer))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door)) // If we make contact with a door
            {
                if (door.isOpen) // if the door is open
                {
                    door.Close();
                }
                else
                {
                    door.Open(this.transform);
                }
            }
        }
    }

    public void GrabBanana()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitBanana, MaxBananaUseDistance) && hitBanana.collider.gameObject.name == "Banana")
        {
            Destroy(hitBanana.collider.gameObject);
            banana.CollectBanana();
           
        }
    }

    public void GrabBattery()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitBattery, MaxBatteryUseDistance) && hitBattery.collider.gameObject.name == "Battery")
        {
            Destroy(hitBattery.collider.gameObject);
            battery.CollectBattery();
        }
    }
}
