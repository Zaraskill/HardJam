using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRotation : MonoBehaviour
{
    public float rotationSpeed;

    private void Awake()
    {
        GameManager.instance.plateauTournant = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.state == EnumStateScene.Level)
            transform.Rotate(0, rotationSpeed, 0);
    }
}
