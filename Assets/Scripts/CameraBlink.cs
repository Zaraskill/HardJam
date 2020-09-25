using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class CameraBlink : MonoBehaviour
{
    public List<Color> backgroundColorList;
    public float timerSwitch = 1;
    private float _timeLeft;

    Camera cam;
    private int colorIndex;

    [Header("Camera Shaker")]
    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;

    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = timerSwitch;
        cam = gameObject.GetComponent<Camera>();
        cam.backgroundColor = backgroundColorList[0];
        Camera.main.GetComponent<ColorBlindFilter>().mode = (ColorBlindMode)UIManager.instance.typeBlindness;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetSceneByBuildIndex(0) == SceneManager.GetActiveScene())
        {
            if (UpdateBackgroundTimer())
            {
                SwitchBackgroundColor();
            }
        }
        else if (GameManager.instance.state == EnumStateScene.Level)
        {
            if (UpdateBackgroundTimer())
            {
                SwitchBackgroundColor();
            }
        }
    }

    void SwitchBackgroundColor()
    {
        if(cam.backgroundColor == backgroundColorList[0])
        {
            cam.backgroundColor = backgroundColorList[1];
        }
        else
        {
            cam.backgroundColor = backgroundColorList[0];
        }
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
    }

    private bool UpdateBackgroundTimer()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft <= 0)
        {
            _timeLeft = timerSwitch;
            return true;
        }
        return false;
    }
}
