using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Battery_Item : MonoBehaviour
{
    public GameObject batteryPrefab;
    public Transform[] spawnPoints;
    public int batteryPerSet = 2;
    private int batterysCollected = 0;

    public Battery flashlight;

    private AudioSource audioSource;
    public AudioClip batterySound;

    private List<Transform> usedSpawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnBatterys();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }

    private void Update()
    {
        if (batterysCollected >= batteryPerSet)
        {
            batterysCollected = 0;
            DestroyBatteries();
            SpawnBatterys();
        }
    }
    public void SpawnBatterys()
    {
        ShuffleArray(spawnPoints);

        for(int i = 0; i < batteryPerSet; i++)
        {
            Transform randomSpawnPoint = GetRandomSpawnPoint();

            while (usedSpawnPoints.Contains(randomSpawnPoint))
            {
                randomSpawnPoint = GetRandomSpawnPoint();
            }

            usedSpawnPoints.Add(randomSpawnPoint);

            GameObject instantiatedBattery = Instantiate(batteryPrefab, randomSpawnPoint.position, Quaternion.identity);
            BoxCollider collider = instantiatedBattery.AddComponent<BoxCollider>();
            collider.center = new Vector3(5.960464e-08f, 0.4519436f, 2.248268f);
            collider.size = new Vector3(20f, 20f, 20f);
            instantiatedBattery.name = "Battery";
            instantiatedBattery.tag = "Battery";
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        Transform randomSpawnPoint = null;
        randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return randomSpawnPoint;
    }

    public void CollectBattery()
    {
        batterysCollected++;

        audioSource.clip = batterySound;
        audioSource.Play();

        if(flashlight.flashlightBattery <= flashlight.maxBattery)
        {
            if(flashlight.maxBattery - flashlight.flashlightBattery < flashlight.batteryRegen)
            {
                flashlight.flashlightBattery = flashlight.maxBattery;
            }
            else
            {
                flashlight.flashlightBattery += flashlight.batteryRegen;
            }
        }

    }
  
    private void DestroyBatteries()
    {
        GameObject[] batteries = GameObject.FindGameObjectsWithTag("Battery");
        
        foreach(GameObject battery in batteries)
        {
            Destroy(battery);
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
