using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    public GameObject pauseObject;

    public Image timerBar;
    public Image limitBar;

    public Text lauchLevelText;
    public Image FondLauchLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        lauchLevelText.gameObject.SetActive(false);
    }
}
