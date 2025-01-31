using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySoundSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] scarySounds;
    // Start is called before the first frame update
    void Start()
    {
        ShuffleArray(spawnPoints);
        SpawnSounds(scarySounds, spawnPoints);
    }

    private void SpawnSounds(GameObject[] sounds, Transform[] spawnpoints)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            sounds[i].transform.position = spawnPoints[i].transform.position;
        }
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
