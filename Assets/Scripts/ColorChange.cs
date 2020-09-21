using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public bool isImpostor;
    public ImpostorStyle style;
    public Vector3 size;
    public float speedTime;
    private float speedLeft;
    public List<Color> colorList;

    private int color;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        speedLeft = speedTime;
        if (style == ImpostorStyle.Size)
        {
            GetComponent<Transform>().localScale = size;
        }
        color = Random.Range(0, colorList.Capacity);
        material = GetComponent<MeshRenderer>().material;
        material.color = colorList[color];
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateTimer())
        {
            SwitchColor();
        }
    }

    private void SwitchColor()
    {
        if (colorList.Capacity-1 == color)
        {
            color = 0;
        }
        else
        {
            color++;
        }
        material.color = colorList[color];
    }

    private bool UpdateTimer()
    {
        speedLeft -= Time.deltaTime;
        if (speedLeft <= 0)
        {
            speedLeft = speedTime;
            return true;
        }
        return false;
    }
}

public enum ImpostorStyle
{
    Speed,
    Color,
    Size
}
