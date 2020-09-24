using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
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
    public GameObject eventSystem;
    public GameObject firstMenu;
    public GameObject firstOption;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickOptions(bool sideChange)
    {
        if (sideChange)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    public void ToggleScript(int numberToggle)
    {
        
        if (!EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
        {
            Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode) 0;
        }
        else
        {
            Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode)numberToggle;
        }
        
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void SlideVolume()
    {
        float valueVolume = sliderVolume.value;
    }

    public void SlideSfx()
    {
        float valueSfx = sliderSfx.value;
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