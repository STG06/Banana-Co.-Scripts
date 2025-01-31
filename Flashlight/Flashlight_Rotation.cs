using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_Rotation : MonoBehaviour
{

    //Referencing our camera
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = -cam.transform.forward;
    }
}
