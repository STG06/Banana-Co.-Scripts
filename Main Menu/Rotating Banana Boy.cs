using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBananaBoy : MonoBehaviour
{
    private Transform bananaBoy;

    private void Awake()
    {
        bananaBoy = GetComponent<Transform>();
    }

    private void Update()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            bananaBoy.transform.eulerAngles = new Vector3(0f, 180f, 67f);
            yield return new WaitForSeconds(0.75f);
            bananaBoy.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

    }
}

