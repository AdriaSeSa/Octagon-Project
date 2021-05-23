using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    /* DOWN= 0,
     UP = 1, 
     RIGHT = 2,
     LEFT= 3,
     DOWN_RIGHT= 4,
     DOWN_LEFT = 5,
     UP_RIGHT = 6,
     UP_LEFT = 7*/

    private EnemySpawner _enemySpawner;

    private Timer _timer;

    private List<List<int>> _enemyPatterns = new List<List<int>>();

    private IEnumerator waveSpawning;
    public IEnumerator changingSpeed;
    
    private bool isGameOver; 
    [SerializeField, UnityEngine.Range(0,2)]
    private int gameDifiiculty;
    
    private readonly int[] speeds = {8, 12, 4};
    private readonly float[] spawnRatios = {1.0f, 1.5f, 0.6f};

    public int enemiesSpeed;
    public float enemiesSpawnRatios;

    public TextMeshProUGUI changeSpeedText;

    
    

    public Canvas gameOverPanel;

    private void Start()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _timer = FindObjectOfType<Timer>();

        enemiesSpeed = 8;
        enemiesSpawnRatios = 1.0f;

        SetDifficulty(gameDifiiculty);
    }


    private void Update()
    {
        if (!isGameOver)
        {
            
            if (changingSpeed == null && waveSpawning == null)
            {
                Debug.Log("SpawnWave");
                waveSpawning = WaveSpawning();
                StartCoroutine(WaveSpawning());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload current Scene
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                //TODO: Charge Main Menu Scene
            }
        }
    }

    private IEnumerator WaveSpawning()
    {
        int tempEnemyPattern = Random.Range(0, _enemyPatterns.Count);

        _enemySpawner.SpawnEnemyWave(_enemyPatterns[tempEnemyPattern], enemiesSpawnRatios);   

        yield return new WaitUntil(CheckEnemiesSpawn);
        waveSpawning = null;
    }

    private bool CheckEnemiesSpawn()
    {
        if (_enemySpawner._spawnEnemies == null)
        {
            return true;
        }

        return false;
    }

    public void GameOver()
    {
        _enemySpawner.StopEnemies();
        gameOverPanel.gameObject.SetActive(true);
        isGameOver = true;
        _timer.StopTimer();
    }
    
    public void ChangeEnemiesSpeed()
    {
        //    Pause game 0.5 seconds and show text
        if (changingSpeed == null)
        {
            changingSpeed = ChangeSpeed();
            StartCoroutine(ChangeSpeed());
        }
    
        //    Randomize enemy speed and spawn ratio 
        int randomNum = 0;
        
        do
        {
            randomNum = Random.Range(0, 3);
        } 
        while (speeds[randomNum] == enemiesSpeed);
        
        enemiesSpeed = speeds[randomNum];
        enemiesSpawnRatios = spawnRatios[randomNum];
        
        //    Destroy current enemies
        _enemySpawner.DestroyEnemies();
    }

    private IEnumerator ChangeSpeed()
    {
        changeSpeedText.gameObject.SetActive(true);
        
        changeSpeedText.text = "Changing Speed!";
        yield return new WaitForSecondsRealtime(1.5f);
        
        changeSpeedText.gameObject.SetActive(false);

        changingSpeed = null;
    }
    //TODO: Poner diferentes patrones a diferentes dificultades
    private void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                List<int> pattern1 = new List<int> {0, 1, 0, 1};
                List<int> pattern2 = new List<int> {2, 3, 2, 3};
                List<int> pattern3 = new List<int> {0, 1, 2, 3};
                List<int> pattern4 = new List<int> {4, 5, 6, 7};
                List<int> pattern5 = new List<int> {1, 5, 4, 3};
                List<int> pattern6 = new List<int> {0, 1, 0, 7};

                _enemyPatterns.Add(pattern1);
                _enemyPatterns.Add(pattern2);
                _enemyPatterns.Add(pattern3);
                _enemyPatterns.Add(pattern4);
                _enemyPatterns.Add(pattern5);
                _enemyPatterns.Add(pattern6);
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
