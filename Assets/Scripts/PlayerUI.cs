using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    public GameObject pauseObject;

    public Image timerBar;
    public Image limitBar;

    public Image panelScoreJ1;
    public Image panelScoreJ2;

    public Text lauchLevelText;
    public Image FondLauchLevel;

    public PostProcessVolume postProcess;

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
        GameManager.instance.postProcess = postProcess;
        FondLauchLevel.gameObject.SetActive(true);
        lauchLevelText.gameObject.SetActive(true);
        lauchLevelText.gameObject.GetComponent<Animator>().SetInteger("LevelStartTextInt", 0);
    }
}
