using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TMPro;
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


   
    private int startCountDown;
    private int _currentHighScoreS, _currentHighScoreR, _currentHighScore;

    public bool isGameOver; 

    
    private readonly int[] speeds = {8, 12, 3};

    public int enemiesSpeed;
    public float enemiesSpawnRatios;

    public TextMeshProUGUI changeSpeedText;

    public GameObject[] tubes = new GameObject[4];
    public bool isEightWay;

    public Canvas gameOverPanel;

    private UIManager _uiManager;
    private bool _isHighScore;
    private AudioController _audioController;
    



    private void Start()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
        _timer = FindObjectOfType<Timer>();
      
        _uiManager = FindObjectOfType<UIManager>();

        _audioController = FindObjectOfType<AudioController>();

     
        SetGameMode(PlayerPrefs.GetInt("gameMode"));
        SetDifficulty(PlayerPrefs.GetInt("difficulty"));
        SetTubes();
    }


    private void Update()
    {
        if (startCountDown < 200)
        {
            startCountDown++;
            return;
        }
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
                SceneManager.LoadScene(0);
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
        _enemySpawner.ToggleStopEnemies(true);
        _uiManager.ToggleUI(false);
        isGameOver = true;
        _timer.ToggleTimer(true);
        _audioController.musicSource.Stop();
        
        SetHighScore();
        _uiManager.SetGameOverUI(PlayerPrefs.GetInt("gameMode"), PlayerPrefs.GetInt("difficulty"), _timer._currentSecond, _isHighScore);

        
    }

    private void SetHighScore()
    {
        switch (PlayerPrefs.GetInt("difficulty"))
        {
            case 0:
                if (PlayerPrefs.GetInt("gameMode") == 0 && _timer._currentSecond > PlayerPrefs.GetInt("highScore00"))
                {
                    PlayerPrefs.SetInt("highScore00", _timer._currentSecond);
                    _isHighScore = true;
                }
                else if (PlayerPrefs.GetInt("gameMode") == 1 &&_timer._currentSecond > PlayerPrefs.GetInt("highScore10"))
                {
                    PlayerPrefs.SetInt("highScore10", _timer._currentSecond);
                    _isHighScore = true;
                }
                break;
            case 1:
                if (PlayerPrefs.GetInt("gameMode") == 0 && _timer._currentSecond > PlayerPrefs.GetInt("highScore01"))
                {
                    PlayerPrefs.SetInt("highScore01", _timer._currentSecond);
                    _isHighScore = true;
                }
                else if (PlayerPrefs.GetInt("gameMode") == 1 && _timer._currentSecond > PlayerPrefs.GetInt("highScore11"))
                {
                    PlayerPrefs.SetInt("highScore11", _timer._currentSecond);
                    _isHighScore = true;
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("gameMode") == 0 && _timer._currentSecond > PlayerPrefs.GetInt("highScore02"))
                {
                    PlayerPrefs.SetInt("highScore02", _timer._currentSecond);
                    _isHighScore = true;
                }
                else if (PlayerPrefs.GetInt("gameMode") == 1 && _timer._currentSecond > PlayerPrefs.GetInt("highScore12"))
                {
                    PlayerPrefs.SetInt("highScore12", _timer._currentSecond);
                    _isHighScore = true;
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("gameMode") == 0 && _timer._currentSecond > PlayerPrefs.GetInt("highScore03"))
                {
                    PlayerPrefs.SetInt("highScore03", _timer._currentSecond);
                    _isHighScore = true;
                }
                else if (PlayerPrefs.GetInt("gameMode") == 1 && _timer._currentSecond > PlayerPrefs.GetInt("highScore13"))
                {
                    PlayerPrefs.SetInt("highScore13", _timer._currentSecond);
                    _isHighScore = true;
                }
                break;
        }
    }
    
    
    //TODO: Poner diferentes patrones a diferentes dificultades
    /// <summary>
    /// Set patterns and spawn ratio for different difficulties 
    /// </summary>
    /// <param name="difficulty">0=easy; 1=normal; 2=hard; 3=eight-way</param>
    private void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                List<int> pattern1 = new List<int> {0, 1, 0, 1};
                List<int> pattern2 = new List<int> {2, 3, 2, 3};
                List<int> pattern3 = new List<int> {0, 1, 2, 3};
                List<int> pattern4 = new List<int> {0, 1, 0, 3};
                List<int> pattern5 = new List<int> {1, 1, 1, 2, 2, 0, 1, 1};
                List<int> pattern6 = new List<int> {3,3,2,2,1,1,3,2,1};

                _enemyPatterns.Add(pattern1);
                _enemyPatterns.Add(pattern2);
                _enemyPatterns.Add(pattern3);
                _enemyPatterns.Add(pattern4);
                _enemyPatterns.Add(pattern5);
                _enemyPatterns.Add(pattern6);
                
                enemiesSpawnRatios = PlayerPrefs.GetInt("gameMode") == 0 ? 0.9f : 1.8f;
                
                break;
            case 1:
                List<int> pattern12 = new List<int> {0, 1, 0, 1};
                List<int> pattern22 = new List<int> {2, 3, 2, 3};
                List<int> pattern32 = new List<int> {0, 1, 2, 3};
                List<int> pattern42 = new List<int> {0, 1, 0, 3};
                List<int> pattern52 = new List<int> {1, 1, 1, 2, 2, 0, 1, 1};
                List<int> pattern62 = new List<int> {3,3,2,2,1,1,3,2,1};

                _enemyPatterns.Add(pattern12);
                _enemyPatterns.Add(pattern22);
                _enemyPatterns.Add(pattern32);
                _enemyPatterns.Add(pattern42);
                _enemyPatterns.Add(pattern52);
                _enemyPatterns.Add(pattern62);

                enemiesSpawnRatios = PlayerPrefs.GetInt("gameMode") == 0 ? 0.7f : 1.3f;
                
                break;
            case 2:
                List<int> pattern13 = new List<int> {0, 1, 0, 1};
                List<int> pattern23 = new List<int> {2, 3, 2, 3};
                List<int> pattern33 = new List<int> {0, 1, 2, 3};
                List<int> pattern43 = new List<int> {0, 1, 0, 3};
                List<int> pattern53 = new List<int> {1, 1, 1, 2, 2, 0, 1, 1};
                List<int> pattern63 = new List<int> {3,3,2,2,1,1,3,2,1};

                _enemyPatterns.Add(pattern13);
                _enemyPatterns.Add(pattern23);
                _enemyPatterns.Add(pattern33);
                _enemyPatterns.Add(pattern43);
                _enemyPatterns.Add(pattern53);
                _enemyPatterns.Add(pattern63);
                
                enemiesSpawnRatios = PlayerPrefs.GetInt("gameMode") == 0 ? 0.35f : 1f;
                break;
            case 3:
                List<int> pattern14 = new List<int> {0, 1, 0, 1};
                List<int> pattern24 = new List<int> {2, 3, 2, 3};
                List<int> pattern34 = new List<int> {0, 1, 2, 3};
                List<int> pattern44 = new List<int> {4, 5, 6, 7};
                List<int> pattern54 = new List<int> {1, 5, 4, 3};
                List<int> pattern64 = new List<int> {0, 1, 0, 7};
                List<int> pattern74 = new List<int> {7, 6, 5, 4};
                List<int> pattern84 = new List<int> {6, 6, 5, 5, 4, 4, 7, 7};

                _enemyPatterns.Add(pattern14);
                _enemyPatterns.Add(pattern24);
                _enemyPatterns.Add(pattern34);
                _enemyPatterns.Add(pattern44);
                _enemyPatterns.Add(pattern54);
                _enemyPatterns.Add(pattern64);
                _enemyPatterns.Add(pattern74);
                _enemyPatterns.Add(pattern84);

                enemiesSpawnRatios = PlayerPrefs.GetInt("gameMode") == 0 ? 0.5f : 1f;
                isEightWay = true;
                break;
        }
    }

    private void SetGameMode(int gameMode)
    {
        switch (gameMode)
        {
            case 0:
                enemiesSpeed = speeds[2];
                enemiesSpawnRatios = 1.0f;
                break;
            case 1:
                enemiesSpeed = speeds[1];
                enemiesSpawnRatios = 2.0f;
                break;
        }
    }

    private void SetTubes()
    {
        for (int i = 0; i < tubes.Length; i++)
        {
            tubes[i].SetActive(isEightWay);
        }
    }
    
    /* public void ChangeEnemiesSpeed()
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
    }*/
}
