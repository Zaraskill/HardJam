using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] propsSpawnPointsArray;
    public List<int> occupiedSpawnPoints;
    public int maxPropsInLevel = 5;

    [Header("Enum State")]
    [HideInInspector] public EnumStateScene state;

    [Header("Different Props")]
    public List<Props> differentProps;
    public List<GameObject> instanceLevels;

    [Header("Patterns Colors")]
    public List<Colors> patterns;
    private int randomPattern;

    [HideInInspector] public GameObject plateauTournant;
    private GameObject instanceLevel;

    [Header("Timer")]
    public float roundTime = 15f;
    private float _roundTimeLeft;
    public float gameTime = 90f;
    private float _gameTimeLeft;

    private bool pause;

    [HideInInspector] public PostProcessVolume postProcess;
    DepthOfField depthOfField;
    Vignette vignette;

    //Test
    //[Header("Score")]
    //public int score1 = 0;
    //public int score2 = 0;
    //public Text scoreTextJ1;
    //public Text scoreTextJ2;

    [Header("Background Color")]
    public List<BackgroundColors> bgColorsList;
    private int colorIndex = 0;

    private bool is30SecDone = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
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

    public void Init()
    {
        postProcess.profile.TryGetSettings(out depthOfField);
        depthOfField.active = false;
        postProcess.profile.TryGetSettings(out vignette);
        vignette.active = false;

        state = EnumStateScene.StartLevel;
        _roundTimeLeft = roundTime;
        _gameTimeLeft = gameTime;
        instanceLevel = Instantiate(instanceLevels[Random.Range(0, instanceLevels.Capacity)], plateauTournant.transform);
        propsSpawnPointsArray = GameObject.FindGameObjectsWithTag("Spawnpoint");
        SpawnProps();
        StopCoroutine(StartLevelText());
        StartCoroutine(StartLevelText());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnumStateScene.Level)
        {
            //SpawnProps();
            if (RoundTimer())
            {
                NextRound();
            }
            GameTimer();
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
        var P_i = PlayerUI.instance;

        _roundTimeLeft -= Time.deltaTime;
        P_i.timerBar.fillAmount = _roundTimeLeft / roundTime;
        P_i.timerBar.color = Color.Lerp(Color.red, Color.green, P_i.timerBar.fillAmount);
        P_i.limitBar.rectTransform.anchorMin = new Vector2(P_i.timerBar.fillAmount, P_i.limitBar.rectTransform.anchorMin.y);
        P_i.limitBar.rectTransform.anchorMax = new Vector2(P_i.timerBar.fillAmount, P_i.limitBar.rectTransform.anchorMax.y);
        P_i.limitBar.rectTransform.anchoredPosition = Vector2.zero;
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
        if(Mathf.FloorToInt(_gameTimeLeft) == 30 && !is30SecDone)
        {
            SoundManager.instance.Play30sec();
            vignette.active = true;
            is30SecDone = true;
        }
        if(_gameTimeLeft <= 0)
        {
            FinalScore.instance.StopGameplayAndShowFinalScore();
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

        depthOfField.active = true;
        yield return new WaitForEndOfFrame();
       
   
        instanceLevel = Instantiate(objToInstantiate, plateauTournant.transform);

        DestroyProps();
        propsSpawnPointsArray = GameObject.FindGameObjectsWithTag("Spawnpoint");
        SpawnProps();

        yield return new WaitForSeconds(0.2f);
        depthOfField.active = false;



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

    public void Pause()
    {
        pause = !pause;
        if (pause)
        {
            state = EnumStateScene.Pause;
            Time.timeScale = 0;
            PlayerUI.instance.pauseObject.SetActive(pause);
        }
        else
        {
            state = EnumStateScene.Level;
            Time.timeScale = 1;
            PlayerUI.instance.pauseObject.SetActive(pause);
        }
    }

    IEnumerator StartLevelText()
    {
        var P_anim = PlayerUI.instance.lauchLevelText.gameObject.GetComponent<Animator>();
        P_anim.SetInteger("LevelStartTextInt", 0);
        yield return new WaitForSeconds(1.1f);
        PlayerUI.instance.lauchLevelText.text = "2";
        P_anim.SetInteger("LevelStartTextInt", 1);
        yield return new WaitForSeconds(1.1f);
        PlayerUI.instance.lauchLevelText.text = "1";
        P_anim.SetInteger("LevelStartTextInt", 2);
        yield return new WaitForSeconds(1.1f);
        PlayerUI.instance.lauchLevelText.text = "GO";
        P_anim.SetInteger("LevelStartTextInt", 3);
        yield return new WaitForSeconds(0.9f);
        PlayerUI.instance.lauchLevelText.gameObject.SetActive(false);
        PlayerUI.instance.FondLauchLevel.gameObject.SetActive(false);
        state = EnumStateScene.Level;
    }
}


