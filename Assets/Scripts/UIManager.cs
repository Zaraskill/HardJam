using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    public Slider sliderVolume;
    public Slider sliderSfx;
    public GameObject firstMenu;
    public GameObject firstOption;
    private IList<Player> listPlayers;
    public GameObject playButton;
    public GameObject optButton;
    public GameObject quitButton;
    public GameObject tutorial;
    public GameObject tutoTwo;
    public GameObject button;
    public float timerWait;
    private float timer;
    private int typeBlindness;
    public List<Toggle> togglesBlindness;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        togglesBlindness[PlayerPrefs.GetInt("ColorBlindness", 0)].isOn = true;
        listPlayers = ReInput.players.AllPlayers;
        timer = timerWait;
        sliderVolume.value = PlayerPrefs.GetFloat("Music", 75);
        sliderSfx.value = PlayerPrefs.GetFloat("SFX", 75);
        AkSoundEngine.SetRTPCValue("Music", PlayerPrefs.GetFloat("Music", 75));
        AkSoundEngine.SetRTPCValue("SFX", PlayerPrefs.GetFloat("SFX", 75));
    }

    private void Update()
    {
        if (SceneManager.GetSceneByBuildIndex(0) == SceneManager.GetActiveScene())
        {
            if (optionsMenu.activeInHierarchy)
            {
                foreach (Player player in listPlayers)
                {
                    if (player.GetButtonDown("UICancel"))
                    {
                        mainMenu.SetActive(true);
                        optionsMenu.SetActive(false);
                        EventSystem.current.SetSelectedGameObject(firstMenu);
                    }
                }
            }
            else if (mainMenu.activeInHierarchy)
            {
                if (EventSystem.current.currentSelectedGameObject == playButton)
                {
                    playButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1, 1);
                    optButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);
                    quitButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);
                }
                else if (EventSystem.current.currentSelectedGameObject == optButton)
                {
                    optButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1, 1);
                    playButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);
                    quitButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);
                }
                else if (EventSystem.current.currentSelectedGameObject == quitButton)
                {
                    quitButton.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1, 1);
                    optButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);
                    playButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1, 1);
                }
            }
            else if (tutorial.activeInHierarchy)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    button.SetActive(true);
                    foreach (Player player in listPlayers)
                    {
                        if (player.GetButtonDown("UISubmit"))
                        {
                            if (tutoTwo.activeInHierarchy)
                            {
                                SoundManager.instance.StopMusic();
                                SceneManager.LoadScene(1);
                            }
                            else
                            {
                                tutoTwo.SetActive(true);
                                button.SetActive(false);
                                timer = timerWait;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode)typeBlindness;
        }
        
    }

    public void OnClickPlay()
    {
        mainMenu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void OnClickOptions(bool sideChange)
    {
        if (sideChange)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstOption);
        }
    }

    public void ToggleScript(int numberToggle)
    {
        if (mainMenu.activeInHierarchy || EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
        {
            typeBlindness = numberToggle;
            Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode)numberToggle;
            PlayerPrefs.SetInt("ColorBlindness", numberToggle);
        }
        else
        {            
            Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode)0;
            PlayerPrefs.SetInt("ColorBlindness", 0);
            typeBlindness = 0;
        }
        
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void SlideVolume()
    {
        float valueVolume = sliderVolume.value;
        PlayerPrefs.SetFloat("Music", valueVolume);
        AkSoundEngine.SetRTPCValue("Music", valueVolume);
    }

    public void SlideSfx()
    {
        float valueSfx = sliderSfx.value;
        PlayerPrefs.SetFloat("SFX", valueSfx);
        AkSoundEngine.SetRTPCValue("SFX", valueSfx);
    }

    public void OnCancel()
    {
        if (optionsMenu.activeInHierarchy)
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            FindObjectOfType<EventSystem>().firstSelectedGameObject = firstOption;
        }
        
    }
}