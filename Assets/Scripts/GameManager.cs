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

    public List<int> occupiedSpawnPoints;
    public int maxPropsInLevel = 5;


    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timerSwitch;
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        //if (UpdateTimer())
        //{
        //    SwitchColor();
        //}

        //SpawnProps();
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
            int randomIndex = Random.Range(1, propsSpawnPointsArray.Length);
            if (!occupiedSpawnPoints.Contains(randomIndex))
            {
                occupiedSpawnPoints.Add(randomIndex);
                Instantiate(propsToInstantiate, propsSpawnPointsArray[randomIndex].transform.position, Quaternion.identity);
                i++;
            }
        }
    }
}
