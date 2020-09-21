using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private int color;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        color = Random.Range(0, GameManager.gameManager.colorList.Capacity);
        material = GetComponent<MeshRenderer>().materials[0];
        material.color = GameManager.gameManager.colorList[color];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchColor()
    {
        if (GameManager.gameManager.colorList.Capacity-1 == color)
        {
            color = 0;
        }
        else
        {
            color++;
        }
        material.color = GameManager.gameManager.colorList[color];
    }

}
