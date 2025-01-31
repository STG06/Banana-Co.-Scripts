using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
   public int sensX;
   public int sensY;

   public Transform player;

   float cameraVerticalRotation = 0f;

    private void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;

    }

    public void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X")  * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y")  * sensY * Time.deltaTime;


        //Moving camera around its local X axis
        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        //Rotating the Player Object and the Camera around its Y axis
        player.Rotate(Vector3.up * mouseX);

    }

}
