using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBoySpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject bananaBoy;
    // Start is called before the first frame update

    private void Awake()
    {
        ShuffleArray(spawnPoints);
        int position = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[position];
        bananaBoy.transform.position = spawnPoint.position;
    }

    private void ShuffleArray<T>(T[] array) //Fisher-Yates shuffle algorithm
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}

