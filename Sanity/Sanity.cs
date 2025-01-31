using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using TMPro;


public class Sanity : MonoBehaviour
{
    public float sanity = 100f;
    private float maxSanity = 100f;
    private float fadeDuration = 2.0f;

    public Flashlight flashlight;
    public BananaBoy bananaBoy;

    public Color Blue;
    public Color Yellow;
    public Color whitishRed;
    public Color Red;

    public Volume volume;
    private MotionBlur motionBlur;

    public AudioSource audioSource;
    public AudioClip impacts;
    public AudioClip stressImpacts;
    public AudioClip whispering;
    public AudioClip drama;

    private bool playingImpacts = false;
    private bool playingStressImpacts = false;
    private bool playingWhispering = false;
    private bool gameOverSound = false;
    private bool decreaseSanity = false;

    public Victory_Gameover vg;


    [Header("Health UI elements")]
    [SerializeField] private Image sanityProgressUI = null;
    [SerializeField] private CanvasGroup sanityCanvasGroup = null;

    private void Awake()
    {
        sanityProgressUI.color = Blue;
        sanityCanvasGroup.alpha = 1;

        volume.profile.TryGet(out motionBlur);
        motionBlur.active = false;
    }

    private void Update()
    {
        sanityEffects();

        GameObject[] pointLights = GameObject.FindGameObjectsWithTag("Point Light");

        SanityControl();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Point Light"))
        {
            decreaseSanity = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Point Light"))
        {
            decreaseSanity = true;
        }
    }

    private void SanityControl()
    {
        if (!decreaseSanity)
        {
            if(sanity < 100.1f)
            {
                sanity += 0.5f * Time.deltaTime;
                updateSanity();
            }
          
        }
        else if (flashlight.on)
        {
            if (sanity < 100.1f)
            {
                sanity += 0.5f * Time.deltaTime;
                updateSanity();
            }
        }
        else if (decreaseSanity && !flashlight.on)
        {
            if(sanity > -0.1f)
            {
                sanity -= 0.5f * Time.deltaTime;
                updateSanity();
            }
        }
    }

    public void updateSanity()
    {
        sanityProgressUI.fillAmount = sanity / maxSanity;
    }

  
    private void sanityEffects()
    {

        if (sanity <= 0 && !gameOverSound)
        {
            //Game Over
            bananaBoy.jumpScared = true;
            vg.GameOver();
            StartCoroutine(GameOver());
            gameOverSound = true;
            
        }
        else if(sanity <= 0)
        {
            vg.GameOver();
        }

        if (sanity <= 50f && sanity > 25f)
        {
            stopStressImpacts();
            playImpacts();
            motionBlur.intensity.Override(25f);
            motionBlur.active = true;
            sanityProgressUI.color = Yellow;
        }
        if (sanity <= 25f && sanity > 10f)
        {
            stopImpacts();
            stopWhispering();
            playStressImpacts();
            motionBlur.intensity.Override(50f);
            sanityProgressUI.color = whitishRed;
        }
        if (sanity <= 10f && sanity > 0f)
        {
            stopStressImpacts();
            playWhispering();
            motionBlur.intensity.Override(100f);
            sanityProgressUI.color = Red;
        }
        else if (sanity > 50f)
        {
            audioSource.Stop();
            sanityProgressUI.color = Blue;
            motionBlur.intensity.Override(0f);
            motionBlur.active = false;
        }
    }

    private IEnumerator GameOver()
    {
            audioSource.Stop();
            audioSource.clip = drama;
            audioSource.loop = false;
            audioSource.Play();
            yield return new WaitForSeconds(2f);
            audioSource.Stop();
       
    }

    private void playImpacts()
    {
        if (!playingImpacts)
        {
            audioSource.clip = impacts;
            audioSource.loop = true;
            audioSource.Play();
            playingImpacts = true;
        }

    }

    private void stopImpacts()
    {
        if (playingImpacts)
        { 
            playingImpacts = false;
        }
    }

    private void playStressImpacts()
    {
        if (!playingStressImpacts)
        {
            audioSource.clip = stressImpacts;
            audioSource.loop = true;
            audioSource.Play();
            playingStressImpacts = true;
        }

    }

    private void stopStressImpacts()
    {
        if (playingStressImpacts)
        {
            playingStressImpacts = false;
        }
    }

    private void playWhispering()
    {
        if (!playingWhispering)
        {
            audioSource.clip = whispering;
            audioSource.loop = true;
            audioSource.Play();
            playingWhispering = true;

        }

    }

    private void stopWhispering()
    {
        if (playingWhispering)
        {
            playingWhispering = false;
        }
    }

}
