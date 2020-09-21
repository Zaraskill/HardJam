using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float timerSwitch = 3;
    private float timeLeft;
    public List<Color> colorList;
    [HideInInspector] public ColorChange[] objectChange;

    public Transform[] propsSpawnPointsArray;
    public GameObject propsToInstantiate;

    public List<Transform> emptySpawnPoints;
    public List<Transform> occupiedSpawnPoints;
    public int maxPropsInLevel = 5;


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

        SpawnProps();
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

    private void SpawnProps()
    {
        int i = 0;
        while (i < maxPropsInLevel)
        {
            int randomIndex = Random.Range(1, maxPropsInLevel);
            if (!occupiedSpawnPoints.Contains(emptySpawnPoints[randomIndex]))
            {
                occupiedSpawnPoints.Add(emptySpawnPoints[randomIndex]);
                Instantiate(propsToInstantiate, emptySpawnPoints[randomIndex].transform.position, Quaternion.identity);
                i++;
            }
        }
    }
}
