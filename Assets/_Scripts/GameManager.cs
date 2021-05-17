using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum EnemySpawns
    {
        DOWN= 0,
        UP = 1, 
        RIGHT = 2,
        LEFT= 3,
        DOWN_RIGHT= 4,
        DOWN_LEFT = 5,
        UP_RIGHT = 6,
        UP_LEFT = 7
    }

    private EnemySpawner _enemySpawner;

    [SerializeField] private  List<List<int>> _enemyPatterns = new List<List<int>>();
    
    private IEnumerator waveSpawning;
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
        if (waveSpawning == null)
        {
            waveSpawning = WaveSpawning();
            StartCoroutine(WaveSpawning());
        }
    }

    private IEnumerator WaveSpawning()
    {

        _enemySpawner.SpawnEnemyWave(_enemyPatterns[Random.Range(0,_enemyPatterns.Count)], 1.0f);
        
        yield return new WaitForSecondsRealtime(7.0f);
        waveSpawning = null;
    }
}
