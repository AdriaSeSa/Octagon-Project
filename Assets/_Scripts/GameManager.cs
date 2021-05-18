using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private  List<List<int>> _enemyPatterns = new List<List<int>>();

    private bool isGameOver;
    
    public IEnumerator waveSpawning;

    public Canvas gameOverCanvas;
    private void Start()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();

        List<int> pattern1 = new List<int> {0, 1, 0, 1};
        List<int> pattern2 = new List<int> {2, 3, 2, 3};
        List<int> pattern3 = new List<int> {0, 1, 2, 3};

        _enemyPatterns.Add(pattern1);
        _enemyPatterns.Add(pattern2);
        _enemyPatterns.Add(pattern3);
    }

    
    private void Update()
    {
        if (!isGameOver)
        {
            CheckEnemiesSpawn();
            if (waveSpawning == null)
            {
                waveSpawning = WaveSpawning();
                StartCoroutine(WaveSpawning());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    //Reload current Scene
            } 
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                //TODO: Charge Main Menu Scene
            }
        }
    }

    private IEnumerator WaveSpawning()
    {
        int temp = Random.Range(0, _enemyPatterns.Count);
        _enemySpawner.SpawnEnemyWave(_enemyPatterns[temp], 1.0f);
        Debug.Log(temp);
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

    public void PlayerDies()
    {
        _enemySpawner.StopEnemies();
        gameOverCanvas.gameObject.SetActive(true);
        isGameOver = true;
    }
}
