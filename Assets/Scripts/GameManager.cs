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

    [Header("Different Props")]
    public List<Props> differentProps;

    public GameObject plateauTournant;

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
        int family = Random.Range(0, differentProps.Capacity);
        int randomIndex = Random.Range(0, propsSpawnPointsArray.Length);
        occupiedSpawnPoints.Add(randomIndex);
        GameObject instance = Instantiate(differentProps[family].impostorProps[Random.Range(0, differentProps[family].impostorProps.Capacity)], propsSpawnPointsArray[randomIndex].transform.position, Quaternion.identity);
        instance.transform.SetParent(plateauTournant.transform);

        int i = 1;
        while (i < maxPropsInLevel)
        {
            randomIndex = Random.Range(0, propsSpawnPointsArray.Length);
            if (!occupiedSpawnPoints.Contains(randomIndex))
            {
                occupiedSpawnPoints.Add(randomIndex);
                instance = Instantiate(differentProps[family].normalProps[Random.Range(0, differentProps[family].normalProps.Capacity)] , propsSpawnPointsArray[randomIndex].transform.position, Quaternion.identity);
                instance.transform.SetParent(plateauTournant.transform);
                i++;
            }
        }
    }
}


