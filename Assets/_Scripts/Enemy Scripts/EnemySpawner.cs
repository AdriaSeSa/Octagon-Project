using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemyBaseClass> enemiesPool;

    private GameManager _gameManager;
    
    public IEnumerator _spawnEnemies;
    
    [SerializeField]
    public EnemySpawnPoint[] spawnPoints = new EnemySpawnPoint[8];

    public bool isSpawningDone;


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }


    /// <summary>
    /// Spawns an Enemy Wave on the List of spawnPoints passed with a time interval
    /// </summary>
    /// <param name="spawnPoint">List of SpawnPoints</param>
    /// <param name="time">Time between each spawn</param>
   public void SpawnEnemyWave(List<int> spawnPoint, float time)
    {
        if (_spawnEnemies == null)
        {
            isSpawningDone = false;
            _spawnEnemies = SpawnWave(spawnPoint, time);
            StartCoroutine(SpawnWave(spawnPoint, time));
        }
    }

    public List<EnemyBaseClass> GetEnemiesPool()
    {
        return enemiesPool;
    }
    
    IEnumerator SpawnWave(List<int> spawnPoint, float time)
    {
       
        for (int i = 0; i < spawnPoint.Count; i++)
        {
            spawnPoints[spawnPoint[i]].SpawnEnemy();
            yield return new WaitForSecondsRealtime(time);
        }

        isSpawningDone = true;
        _spawnEnemies = null;

    }

    public void StopEnemies()
    {
        foreach (var enemy in enemiesPool)
        {
             _gameManager.enemiesSpeed = 0;
        }
    }

    public void DestroyEnemies()
    {
        foreach (var enemy in enemiesPool)
        {
            enemy.Destroy();
        }
    }
}
