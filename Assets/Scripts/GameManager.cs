using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public Transform[] propsSpawnPointsArray;
    public GameObject propsToInstantiate;

    public List<int> occupiedSpawnPoints;
    public int maxPropsInLevel = 5;

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }        
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {

        //SpawnProps();
    }

    private void SpawnProps()
    {
        int i = 0;
        while (i < maxPropsInLevel)
        {
            int randomIndex = Random.Range(0, propsSpawnPointsArray.Length);
            if (!occupiedSpawnPoints.Contains(randomIndex))
            {
                occupiedSpawnPoints.Add(randomIndex);
                Instantiate(propsToInstantiate, propsSpawnPointsArray[randomIndex].transform.position, Quaternion.identity);
                i++;
            }
        }
    }
}
