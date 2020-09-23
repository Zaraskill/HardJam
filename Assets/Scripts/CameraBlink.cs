using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlink : MonoBehaviour
{
    public List<Color> backgroundColorList;
    public float timerSwitch = 1;
    private float _timeLeft;

    Camera cam;
    private int colorIndex;

    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = timerSwitch;
        cam = gameObject.GetComponent<Camera>();
        cam.backgroundColor = backgroundColorList[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(UpdateBackgroundTimer())
        {
            SwitchBackgroundColor();
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
