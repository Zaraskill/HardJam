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
    private List<Color> colorList;

    private int color;
    public List<MeshRenderer> materials;
    // Start is called before the first frame update
    void Start()
    {
        if (isImpostor)
        {
            colorList = GameManager.instance.patterns[GameManager.instance.GetRandomPattern()].impostorColors;
        }
        else
        {
            colorList = GameManager.instance.patterns[GameManager.instance.GetRandomPattern()].normalColors;
        }

        speedLeft = speedTime;
        if (style == ImpostorStyle.Size)
        {
            GetComponent<Transform>().localScale = size;
        }
        color = Random.Range(0, colorList.Capacity);
        foreach(MeshRenderer meshRender in materials)
        {
            foreach(Material mat in meshRender.materials)
            {
                mat.color = colorList[color];
            }            
        }
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
        foreach (MeshRenderer meshRender in materials)
        {
            foreach (Material mat in meshRender.materials)
            {
                mat.color = colorList[color];
            }
        }
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
