using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Steamworks;
public class LevelManager : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI levelText;
    public Banana banana;
    public BananaBoy bananaBoy;
    public Battery battery;
    public float level = 1;

    private AudioSource audioSource;
    public AudioClip levelUpSound;

    public AudioClip victory;

    public Victory_Gameover vg;

    private bool victorySoundPlaying = false;

    void Awake()
    {
       levelText = GetComponent<TextMeshProUGUI>();
       levelText.enabled = false;
       audioSource = GetComponent<AudioSource>();

        if (!SteamManager.Initialized)
        {
            SteamAPI.Init();
        }
    }

    public void Update()
    {
        if(level == 20  && !victorySoundPlaying)
        {
            Victory();
            StartCoroutine(victorySound());
            victorySoundPlaying = true;
        }
        else if(level == 20)
        {
            Victory();
        }
    }

    private void Victory()
    {
        vg.Victory();

    }

    private IEnumerator victorySound()
    {
            audioSource.clip = victory;
            audioSource.loop = false;
            audioSource.Play();
            yield return new WaitForSeconds(5f);
            audioSource.Stop();
    }

    public IEnumerator LevelUpText()
    {
        audioSource.clip = levelUpSound;
        audioSource.Play();
        level++;
        battery.flashlightBattery = 100.0f;
        string achievement = "NEW_ACHIEVEMENT_" + (level - 1).ToString();
        UnlockAchievement(achievement);
        levelText.text = "Level " + level.ToString();
        levelText.enabled = true;
        bananaBoy.normalSpeed = (level / 10f) + bananaBoy.normalSpeed;
        bananaBoy.attackSpeed = bananaBoy.normalSpeed + 1;
        yield return new WaitForSeconds(4.5f);
        levelText.enabled = false;
        

    }

    private void UnlockAchievement(string id)
    {
        SteamUserStats.SetAchievement(id);
        SteamUserStats.StoreStats();
        Debug.Log(id + "is achieved");

    }
    
}
