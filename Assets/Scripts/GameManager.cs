using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] propsSpawnPointsArray;
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
    private float _roundTimeLeft;
    public float gameTime = 90f;
    private float _gameTimeLeft;
    public Image timerBar;
    public Image limitBar;

    //Test
    [Header("Score")]
    public int score1 = 0;
    public int score2 = 0;
    public Text scoreTextJ1;
    public Text scoreTextJ2;

    [Header("Background Color")]
    public List<BackgroundColors> bgColorsList;
    private int colorIndex = 0;

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
        Init();
    }

    void Init()
    {
        _roundTimeLeft = roundTime;
        _gameTimeLeft = gameTime;
        instanceLevel = Instantiate(instanceLevels[Random.Range(0, instanceLevels.Capacity)], plateauTournant.transform);
        propsSpawnPointsArray = GameObject.FindGameObjectsWithTag("Spawnpoint");
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
                instance.transform.Rotate(new Vector3(instance.transform.rotation.x, Random.Range(0f,361f), instance.transform.rotation.z));
                i++;
            }
        }
    }

    private bool RoundTimer()
    {
        _roundTimeLeft -= Time.deltaTime;
        timerBar.fillAmount = _roundTimeLeft / roundTime;
        timerBar.color = Color.Lerp(Color.red, Color.green, timerBar.fillAmount);
        limitBar.rectTransform.anchorMin = new Vector2(timerBar.fillAmount, limitBar.rectTransform.anchorMin.y);
        limitBar.rectTransform.anchorMax = new Vector2(timerBar.fillAmount, limitBar.rectTransform.anchorMax.y);
        limitBar.rectTransform.anchoredPosition = Vector2.zero;
        if (_roundTimeLeft <= 0)
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

    public void NextRound()
    {
        StartCoroutine(_CoroutineNextRound());
    }

    IEnumerator _CoroutineNextRound()
    {
        GameObject objToInstantiate = instanceLevels[Random.Range(0, instanceLevels.Capacity)];
        _roundTimeLeft = roundTime;
        randomPattern = Random.Range(0, patterns.Capacity);
        propsSpawnPointsArray = Array.Empty<GameObject>();
        Destroy(instanceLevel.gameObject);        
        yield return new WaitForEndOfFrame();

        instanceLevel = Instantiate(objToInstantiate, plateauTournant.transform);

        DestroyProps();
        propsSpawnPointsArray = GameObject.FindGameObjectsWithTag("Spawnpoint");


        SpawnProps();




        colorIndex++;
        if (colorIndex >= bgColorsList.Capacity)
        {
            colorIndex = 0;
        }
        Camera.main.backgroundColor = bgColorsList[colorIndex].backgroundColors[0];
        Camera.main.GetComponent<CameraBlink>().backgroundColorList = bgColorsList[colorIndex].backgroundColors;
    }

    private void DestroyProps()
    {
        ColorChange[] props = FindObjectsOfType<ColorChange>();
        foreach(ColorChange prop in props)
        {
            Destroy(prop.gameObject);
        }
        occupiedSpawnPoints.Clear();
    }

    public int GetRandomPattern()
    {
        return randomPattern;
    }
}


