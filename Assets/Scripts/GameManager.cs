using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform[] propsSpawnPointsArray;
    public GameObject propsToInstantiate;
    public List<int> occupiedSpawnPoints;
    public int maxPropsInLevel = 5;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Different Props")]
    public List<Props> differentProps;
    public List<GameObject> instanceLevels;

    [Header("Patterns Colors")]
    public List<Colors> patterns;
    private int randomPattern;

    public GameObject plateauTournant;
    private GameObject instanceLevel;

    [Header("Timer")]
    public float roundTime = 15f;
    [SerializeField] private float _roundTimeLeft;
    public float gameTime = 90f;
    [SerializeField] private float _gameTimeLeft;

    //Test
    [Header("Score")]
    public int score1 = 0;
    public int score2 = 0;
    public Text scoreTextJ1;
    public Text scoreTextJ2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        _roundTimeLeft = roundTime;
        _gameTimeLeft = gameTime;
        SpawnProps();
        instanceLevel = Instantiate(instanceLevels[0], plateauTournant.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnProps();
        if (RoundTimer())
        {
            NextRound();
        }

        //scoreTextJ1.text = score1.ToString();
        //scoreTextJ2.text = score2.ToString();
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

    private bool RoundTimer()
    {
        _roundTimeLeft -= Time.deltaTime;
        if(_roundTimeLeft <= 0)
        {
            _roundTimeLeft = roundTime;
            return true;
        }
        return false;
    }

    private void GameTimer()
    {
        _gameTimeLeft -= Time.deltaTime;
        if(_gameTimeLeft <= 0)
        {
            if (score1 > score2)
            {

            }
            else if (score1 < score2)
            {

            }
            else
            {

            }
        }
    }

    private void NextRound()
    {
        GameObject objToInstantiate = instanceLevels[Random.Range(0, instanceLevels.Capacity)];
        randomPattern = Random.Range(0, patterns.Capacity);
        Destroy(instanceLevel.gameObject);
        instanceLevel = objToInstantiate;
        Instantiate(instanceLevel, plateauTournant.transform);
    }

    public int GetRandomPattern()
    {
        return randomPattern;
    }
}


