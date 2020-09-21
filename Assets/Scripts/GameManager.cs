using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float timerSwitch = 3;
    private float timeLeft;
    public List<Color> colorList;
    [HideInInspector] public ColorChange[] objectChange;


    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timerSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateTimer())
        {
            SwitchColor();
        }
    }


    private bool UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = timerSwitch;
            return true;
        }
        return false;
    }

    private void SwitchColor()
    {
        objectChange = FindObjectsOfType<ColorChange>();
        //changeColor.ChangeColorSaleConnard();
    }
}
