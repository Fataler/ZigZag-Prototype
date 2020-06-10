using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Vector3 initSpawnPoint = new Vector3(4.5f, -0.5f, 4.5f);
    public GameObject coinToSpwan;
    public Vector3 curSpawnPoint;
    public GameObject cubeToSpawn;
    public bool GameOver = false;
    public GameObject randomSpawnObj;
    public int curScore = 0;
    public int hiScore;
    public Text scoreText;
    public Text coinsText;
    public Text hiScoreText;
    public int totalCoins;

    private enum Directions
    {
        x,
        z
    }

    private void Start()
    {
        Init();
        SceneManager.sceneLoaded += Initialize;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Initialize;
    }

    private void Initialize(Scene arg0, LoadSceneMode arg1)
    {
        Init();
    }

    private void Init()
    {
        CancelInvoke("SpawnSequence");
        GameOver = false;
        curSpawnPoint = initSpawnPoint;
        for (int i = 0; i < 20; i++)
        {
            SpawnSequence();
        }
        InvokeRepeating("SpawnSequence", 2f, 0.1f);
        curScore = 0;
        hiScore = PlayerPrefs.GetInt("score", 0);
        totalCoins = PlayerPrefs.GetInt("coins", 0);
        SetupUI();
    }

    private void SetupUI()
    {
        coinsText.text = "COINS: \n" + totalCoins.ToString();
        hiScoreText.text = "HISCORE: \n" + hiScore.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameOver)
        {
            CancelInvoke("SpawnSequence");
            if (curScore > hiScore)
            {
                hiScore = curScore;
                PlayerPrefs.SetInt("score", hiScore);
            }
            PlayerPrefs.SetInt("coins", totalCoins);
        }
        else
        {
            scoreText.text = curScore.ToString();
        }
    }

    private void SpawnSequence()
    {
        int cubesDir = UnityEngine.Random.Range(0, 2);
        int coin = UnityEngine.Random.Range(0, 10);
        if (cubesDir == 0)
        {
            //x
            Spawn(Directions.x);
        }
        else
        {
            //z
            Spawn(Directions.z);
        }

        if (coin == 1)
        {
            Instantiate(coinToSpwan, curSpawnPoint + Vector3.up * 1.5f, Quaternion.identity);
        }
    }

    private void Spawn(Directions dir)
    {
        if (dir == Directions.x)
        {
            curSpawnPoint += new Vector3(1, 0, 0);
        }
        else
        {
            curSpawnPoint += new Vector3(0, 0, 1);
        }
        Instantiate(cubeToSpawn, curSpawnPoint, Quaternion.identity);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RiseScore()
    {
        curScore++;
    }

    public void RiseCoin()
    {
        totalCoins++;
    }

    public void GameStarted()
    {
        GameOver = false;
        var UI = GameObject.Find("UI");
        var animator = UI.GetComponent<Animator>();
        animator.SetTrigger("GameStart");
    }
}