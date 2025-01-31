using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Banana : MonoBehaviour
{
    public GameObject bananaPrefab; 
    public Transform[] spawnPoints;
    public int bananasPerSet;
    private int bananasNeededToCollect = 10;
    public int bananasCollected = 0;
    public TextMeshProUGUI bananaCounter;

    public BananaBoy bananaBoy;
    public LevelManager levelManager;

    private AudioSource audioSource;
    public AudioClip ding;

    private List<Transform> usedSpawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnBananas();
        bananaCounter.enabled = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;
    }
    private void Update()
    {
        if (bananasCollected == bananasNeededToCollect)
        {
            levelManager.StartCoroutine(levelManager.LevelUpText());
            bananasCollected = 0;
            bananaCounter.enabled = false;
            DestroyBananas();
            SpawnBananas();
        }
    }
    private void SpawnBananas()
    {
        ShuffleArray(spawnPoints);
        usedSpawnPoints.Clear();

        for (int i = 0; i < bananasPerSet; i++)
        {
            Transform randomSpawnPoint = GetRandomSpawnPoint();
            if (randomSpawnPoint == null)
            {
                Debug.LogWarning("No available spawn points.");
                return; 
            }

            while (usedSpawnPoints.Contains(randomSpawnPoint))
            {
                randomSpawnPoint = GetRandomSpawnPoint();
            }

            usedSpawnPoints.Add(randomSpawnPoint);

            GameObject instantiatedBanana = Instantiate(bananaPrefab, randomSpawnPoint.position, Quaternion.identity);
            BoxCollider collider = instantiatedBanana.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.05f, 0.05f, 0.5f);
            collider.center = new Vector3(-2.095476e-09f, 0.004281823f, -0.0031387f);
            instantiatedBanana.name = "Banana";
            instantiatedBanana.tag = "Banana";

        }
    }

    private Transform GetRandomSpawnPoint()
    { 
        Transform randomSpawnPoint = null;
        randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return randomSpawnPoint;
    }

    public void CollectBanana()
    {
        audioSource.clip = ding;
        audioSource.Play();
        bananasCollected++;
        bananaBoy.normalSpeed += 0.0125f;
        bananaCounter.enabled = true;
        bananaCounter.text = "Bananas Collected: " +  bananasCollected.ToString() + " / " + bananasNeededToCollect.ToString();
        
    }
    private void DestroyBananas()
    {
        GameObject[] bannas = GameObject.FindGameObjectsWithTag("Banana");
        foreach(GameObject banana in bannas)
        {
            Destroy(banana);
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