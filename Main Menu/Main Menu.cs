using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public CanvasGroup mainMenu;
    public CanvasGroup settings;
    public CanvasGroup controls;
    public CanvasGroup loadingScrene;
    public CanvasGroup lobbySearch;
    public CanvasGroup createLobby;
    public CanvasGroup createdLobby;


    public Slider loadingScreneSlider;

    public GameObject music;

    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        resolutions = Screen.resolutions;

       resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }
    public void PlaySingularPlayer()
    {
        mainMenu.alpha = 0;
        music.SetActive(false);
        loadingScrene.alpha = 1;
        StartCoroutine(loadingBar(1));
    }

    private IEnumerator loadingBar(int level) 
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingScreneSlider.value = progressValue;
            yield return null;
        }
    }


    public void ChangeSettings()
    {
        mainMenu.alpha = 0;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;
        settings.alpha = 1;
        settings.interactable = true;
        settings.blocksRaycasts = true;
    }

    public void Multiplayer()
    { 
        mainMenu.alpha = 0;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;
        lobbySearch.alpha = 1;
        lobbySearch.interactable = true;
        lobbySearch.blocksRaycasts = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    //Controls
    public void ChangeControls()
    {
        settings.alpha = 0;
        settings.interactable = false;
        settings.blocksRaycasts = false;
        controls.alpha = 1;
        controls.interactable = true;
        controls.blocksRaycasts = true;
    }

  
    //Setting Functions
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void ChangeQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void Back()
    {
        settings.alpha = 0;
        settings.interactable = false;
        settings.blocksRaycasts = false;
        controls.alpha = 0;
        controls.interactable = false;
        controls.blocksRaycasts = false;
        lobbySearch.alpha = 0;
        lobbySearch.interactable = false;
        lobbySearch.blocksRaycasts = false;
        createLobby.alpha = 0;
        createLobby.interactable = false;
        createLobby.blocksRaycasts = false;
        createdLobby.alpha = 0;
        createdLobby.interactable = false;
        createdLobby.blocksRaycasts = false;
        mainMenu.alpha = 1;
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void CreateLobbyScrene()
    {
        lobbySearch.alpha = 0;
        lobbySearch.interactable = false;
        lobbySearch.blocksRaycasts = false;
        createLobby.alpha = 1;
        createLobby.interactable = true;
        createLobby.blocksRaycasts = true;
    }

    public void CreatedLobbyScrene()
    {
        createLobby.alpha = 0;
        createLobby.interactable = false;
        createLobby.blocksRaycasts = false;
        lobbySearch.alpha = 0;
        lobbySearch.interactable = false;
        lobbySearch.blocksRaycasts = false;
        createdLobby.alpha = 1;
        createdLobby.interactable = true;
        createdLobby.blocksRaycasts = true;
    }

    public void Leave()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

       
    }


 
}
