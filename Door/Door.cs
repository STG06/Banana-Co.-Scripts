using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    private float openAngle = 90f;
    public float doorSpeed = 2f;

    public bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private AudioSource audioSource;
    public AudioClip doorOpeningSound;
    public AudioClip doorClosingSound;

    public AudioMixerGroup main;


    private void Awake()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = main;
        audioSource.maxDistance = 20f;
        audioSource.minDistance = 19f;
        audioSource.spatialBlend = 1f;
        audioSource.volume = 0.1f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    }

    public void Open(Transform player)
    {
        StopAllCoroutines();
        StartCoroutine(OpenDoor(player));
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(CloseDoor());
    }

    private IEnumerator OpenDoor(Transform player)
    {
        audioSource.clip = doorOpeningSound;
        audioSource.Play();
        Vector3 doorToPlayer = player.position - transform.position;
        float angleToRotate = (Vector3.Dot(transform.up, doorToPlayer) > 0f) ? -openAngle : openAngle;
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + angleToRotate);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, doorSpeed * Time.deltaTime);
            float remainingRotation = Quaternion.Angle(transform.rotation, targetRotation);
            if(remainingRotation <= 1f)
            {
                break;
            } 

            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = true;
    }

    private IEnumerator CloseDoor()
    {
        audioSource.clip = doorClosingSound;
        audioSource.Play();
        targetRotation = initialRotation;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, doorSpeed * Time.deltaTime);
            float remainingRotation = Quaternion.Angle(transform.rotation, targetRotation);
            if(remainingRotation <= 1f)
            {
                break;
            } 
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = false;
    }

    /*public void Open(Transform player)
    {
         StopAllCoroutines();
         StartCoroutine(OpeningDoor(player));
    }

     public void Close()
     {
         StopAllCoroutines();
         StartCoroutine(ClosingDoor());
     }

     private IEnumerator OpeningDoor(Transform player)
     {
         Vector3 playerToDoor = transform.position - player.position;
         float dotProduct = Vector3.Dot(transform.right, playerToDoor);
         float targetAngle = (dotProduct > 0f) ? openAngle : -openAngle;

         // Convert the target angle to radians
         float targetAngleRadians = targetAngle * Mathf.Deg2Rad;

         // Calculate the new z-axis value for the target rotation
         float targetZ = Mathf.Sin(targetAngleRadians) * initialRotation.y + Mathf.Cos(targetAngleRadians) * initialRotation.x;

         // Set the target rotation
         targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, targetZ);

         while (Quaternion.Angle(transform.rotation, targetRotation) > 0f)
         {
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, doorSpeed * Time.deltaTime);
             yield return null;
         }

         transform.rotation = targetRotation;
         isOpen = true;
     }

     private IEnumerator ClosingDoor()
     {
         targetRotation = initialRotation;

         while(Quaternion.Angle(transform.rotation, targetRotation) > 0f)
         {
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, doorSpeed * Time.deltaTime);
             yield return null;
         }

         transform.rotation = targetRotation;
         isOpen = false;
     } */

    /* public void ToggleDoor(Transform player)
   {
       StopAllCoroutines();
       StartCoroutine(OpenOrCloseDoor(player));
   }

   private IEnumerator OpenOrCloseDoor(Transform player)
   {
       Vector3 doorToPlayer = player.position - transform.position;
       float angleToRotate = Vector3.SignedAngle(doorToPlayer, transform.right, Vector3.forward) - openAngle;
       targetRotation = Quaternion.Euler(0f, 0f, angleToRotate);

       while(Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
       {
           transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, doorSpeed * Time.deltaTime);
           yield return null;
       }

       transform.rotation = targetRotation;
       if (isOpen)
       {
           isOpen = false;
       }
       if (!isOpen)
       {
           isOpen = true;
       }
   } */
}