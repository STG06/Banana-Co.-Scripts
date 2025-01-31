using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsMenu : MonoBehaviour
{
    public Player_Camera cam;

    public TextMeshProUGUI sensXText;
    public TextMeshProUGUI sensYText;

    private void Start()
    {
        sensXText.text = (cam.sensX / 100f).ToString();
        sensYText.text = (cam.sensY / 100f).ToString();
    }
    public void ChangeSensX(float sensx)
    {
        cam.sensX = (int)sensx;
        sensXText.text = (cam.sensX / 100f).ToString();
    }

    public void ChangeSensY(float sensY)
    {
        cam.sensY = (int)sensY;
        sensYText.text = (cam.sensY / 100f).ToString();
    }
}
