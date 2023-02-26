using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static int curScore = 0;
    public static int curEnemyCount = 0;
    public static int curLife = 0;

    static public bool isStart = true;

    public float curEnemySpwanDelay = -2f;
    public float nextEnemySpwanDelay = 2f;
    public float clearScore = 1000f;

    public GameObject[] enemyPrefabs;
    public GameObject[] lifeImages;
    public GameObject[] gameTitles;
    public GameObject[] buttons;

    public Transform[] spawnPoints;

    public GameObject lifeText;
    public GameObject player;
    public GameObject bombImage;

    public Text scoreText;
    public Text bombText;


    int totalScore = 0;
    int totalEnemyCount = 0;
    int totalLife = 0;
    int totalBomb = 1;

    Image imageCache;
    Color colorCache;
    Vector3 startPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        // 초기화는 간단히 할 수 있다.
        //SceneManager.LoadScene();

        UIInitialize();
        ResetField();

    }

    private void ResetPlayerStatus()
    {
        player.SetActive(true);

        PlayerCtrl playerCtrl = FindObjectOfType<PlayerCtrl>();
        playerCtrl.PlayerStatusReset();
        playerCtrl.curBombDealy = playerCtrl.maxBombDealy;
        totalBomb = (int)playerCtrl.bomb;

        curScore = 0;
        totalScore = 0;


        totalBomb = 1;

        curEnemyCount = 0;
        totalEnemyCount = 0;

        scoreText.text = "SCORE\n" + string.Format("{0:D6}", totalScore);
        bombText.text = "x " + totalBomb;

    }

    public void ResetField()
    {
        totalScore = 0;

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            Destroy(enemys[i]);
        }

        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }

        GameObject[] PlayerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        for (int i = 0; i < PlayerBullets.Length; i++)
        {
            Destroy(PlayerBullets[i]);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("ItemMaker");
        for (int i = 0; i < items.Length; i++)
        {
            Destroy(items[i]);

        }

        GameObject[] bombEffect = GameObject.FindGameObjectsWithTag("BoomImage");
        for (int i = 0; i < bombEffect.Length; i++)
        {
            Destroy(bombEffect[i]);
        }

        player.transform.position = startPos;

    }

    public void UIInitialize()
    {
        startPos = Vector3.down * 4.2f;

        lifeText.SetActive(false);


        gameTitles[0].SetActive(false);
        gameTitles[1].SetActive(false);

        int WhileCounter = 0;
        while (WhileCounter < lifeImages.Length)
        {
            lifeImages[WhileCounter].SetActive(true);

            imageCache = lifeImages[WhileCounter].GetComponent<Image>();
            colorCache = imageCache.color;
            colorCache.a = 0.5f;
            imageCache.color = colorCache;

            WhileCounter++;

            ButtonReset();

        }

        ResetField();
        ResetPlayerStatus();

        Time.timeScale = 0f;

    }
    private void ButtonReset()
    {
        if (!buttons[0])
            buttons[1].SetActive(true);

    }

    void Update()
    {
        EnemySpwan();
        ScoreUpdate();
        LifeStatus();
        BombStatus();
        GameClear();

    }
    void EnemySpwan()
    {
        curEnemySpwanDelay += Time.deltaTime;

        if (curEnemySpwanDelay > nextEnemySpwanDelay)
        {
            SpwanEnemy();

            nextEnemySpwanDelay = Random.Range(0.5f, 2.0f);
            curEnemySpwanDelay = 0;

        }
    }
    void ScoreUpdate()
    {
        if (curScore == totalScore)
            return;

        totalScore = curScore;
        totalEnemyCount = curEnemyCount;

        scoreText.text = "Score\n" + string.Format("{0:D6}", totalScore);

    }

    void LifeStatus()
    {
        PlayerCtrl playerCtrl = player.GetComponent<PlayerCtrl>();
        curLife = (int)playerCtrl.life;

        if (curLife == totalLife)
            return;

        totalLife = curLife;

        if (totalLife < 4)
        {
            if (totalLife >= 0 && totalLife < 3)
            {
                imageCache = lifeImages[totalLife].GetComponent<Image>();
                colorCache = imageCache.color;
                colorCache.a = 0.5f;
                imageCache.color = colorCache;

            }
            for (int i = 0; i < totalLife; i++)
            {
                imageCache = lifeImages[i].GetComponent<Image>();
                colorCache = imageCache.color;
                colorCache.a = 1f;
                imageCache.color = colorCache;

            }
        }
        else if (totalLife > 4)
        {
            for (int i = 1; i < lifeImages.Length; i++)
            {
                imageCache = lifeImages[i].GetComponent<Image>();
                colorCache = imageCache.color;
                colorCache.a = 0f;
                imageCache.color = colorCache;

            }

            imageCache = lifeImages[0].GetComponent<Image>();
            colorCache = imageCache.color;
            colorCache.a = 1f;
            imageCache.color = colorCache;

            lifeText.SetActive(true);
            Text text = lifeText.GetComponent<Text>();
            text.text = "x " + totalLife;

        }


    }
    void BombStatus()
    {
        PlayerCtrl playerCtrl = player.GetComponent<PlayerCtrl>();


        if (playerCtrl.bomb == totalBomb)
            return;

        totalBomb = (int)playerCtrl.bomb;


        bombText.text = "x " + totalBomb;
    }

    private void SpwanEnemy()
    {
        //int ranEnemy = Random.Range(0, enemyPrefabs.Length);
        int ranPoint = Random.Range(0, spawnPoints.Length);
        int randN = Random.Range(0, 20);

        int enemyCode = 0;

        if (randN <= 8)
            enemyCode = 0;
        else if (randN > 8 && randN <= 14)
            enemyCode = 1;
        else if (randN > 14 && randN <= 18)
            enemyCode = 2;
        else
            enemyCode = 3;

        GameObject goEnemy = Instantiate(enemyPrefabs[enemyCode], spawnPoints[ranPoint].position, Quaternion.identity);

        EnemyCtrl enemyLogic = goEnemy.GetComponent<EnemyCtrl>();
        enemyLogic.Move(ranPoint);

    }

    public void GameClear()
    {
        if (totalScore < clearScore)
            return;


        gameTitles[0].SetActive(true);
        buttons[1].SetActive(true);

        Time.timeScale = 0;
    }
    public void GameOver()
    {
        Invoke("GameOverDeady", 1f);

    }
    void GameOverDeady()
    {
        gameTitles[1].SetActive(true);
        buttons[1].SetActive(true);

        Time.timeScale = 0;

    }

    public void RespawnPlayer()
    {
        Invoke("AlivePlayer", 1.0f);

    }

    public void AlivePlayer()
    {
        GameObject[] enembyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < enembyBullets.Length; i++)
        {
            Destroy(enembyBullets[i]);

        }
        player.transform.position = Vector3.down * 4.2f;
        player.SetActive(true);

    }


}

