using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
  [SerializeField]
  private Vector3 spawnDir;

  private EnemySpawner _enemySpawner;
  public TubesManager _tubesManager;

  private void Start()
  {
    _enemySpawner = FindObjectOfType<EnemySpawner>();
  
  }

  public void SpawnEnemy()
  {
    foreach (var enemy in _enemySpawner.GetEnemiesPool())
    {
      if (enemy.isActiveAndEnabled == false)
      {
        enemy.transform.position = this.transform.position;
        enemy.gameObject.SetActive(true);
        enemy.SetDirection(spawnDir);
        
        _tubesManager.ShootEnemyTube();
        
        break;
      }
    }
  }
}
