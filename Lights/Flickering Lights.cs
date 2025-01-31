using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlickeringLights : MonoBehaviour
{
    private bool isFlickering = false;
    private float timeDelay;
    public AudioMixerGroup main;
    public GameObject lightTube;
    private AudioSource audioSource;
    public AudioClip flickeringSounds;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = main;
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 19f;
        audioSource.maxDistance = 20f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.clip = flickeringSounds;
        audioSource.loop = true;
        audioSource.volume = 0.3f;
        audioSource.Play();

    }
    void Update()
    {
        if (!isFlickering)
        {
            StartCoroutine(Flickering());
        }
        
    }

    private IEnumerator Flickering()
    {
        isFlickering = true;
        gameObject.GetComponent<Light>().enabled = true;
        lightTube.GetComponent<Renderer>().enabled = true;
        timeDelay = Random.Range(5f, 10f);
        yield return new WaitForSeconds(timeDelay);

        lightTube.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(0.1f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;


    }

}
