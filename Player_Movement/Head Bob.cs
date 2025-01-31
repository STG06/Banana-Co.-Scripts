using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{ 
    
    private Quaternion initialRotation;
    private float targetRotationAngle = 0f;
    private float rotationDirection = 1f;
    private bool isRotatingRight = true;
    public PlayerMovement player;

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        initialRotation = transform.localRotation;
    }

   public void Bobbing(float bobbingSpeed, float bobbingAmount)
   {
        timer += Time.deltaTime * bobbingSpeed;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
   }

    public void CameraRotation(float rotationAmount, float rotationSpeed)
    {
        targetRotationAngle = Mathf.Lerp(-rotationAmount, rotationAmount, Mathf.PingPong(Time.time * rotationSpeed, 1f));

        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + targetRotationAngle);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
