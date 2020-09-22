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

    [Header("Timer")]
    public float roundTime = 15f;
    [SerializeField] private float _roundTimeLeft;
    public float gameTime = 90f;
    [SerializeField] private float _gameTimeLeft;

    [Header("Score")]
    private float _scoreJ1 = 0;
    private float _scoreJ2 = 0;

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
        _roundTimeLeft = roundTime;
        _gameTimeLeft = gameTime;
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        //SpawnProps();
        if (RoundTimer())
        {
            NextRound();
        }
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
            if (_scoreJ1 > _scoreJ2)
            {
                // J1 gagne
                // Display des scores
                // Retour au menu
            }
            else if (_scoreJ1 < _scoreJ2)
            {
                // J2 gagne
                // Display des scores
                // Retour au menu
            }
            else
            {
                // Mort subite, on refait un round
                NextRound();
            }
        }
    }

    private void NextRound()
    {

    }
}


