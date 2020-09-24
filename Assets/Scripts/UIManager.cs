using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject mainMenu;
    public GameObject optionsMenu;
    [SerializeField] private Toggle[] toggleList;

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
        toggleList = FindObjectsOfType<Toggle>();
        foreach (Toggle toggle in toggleList)
        {
            toggle.isOn = false;
        }
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
        Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode) numberToggle;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}