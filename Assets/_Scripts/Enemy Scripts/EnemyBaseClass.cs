using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Serialization;

public class EnemyBaseClass : MonoBehaviour
{

    private Light2D light2D;
    private PlayerController _player;
    private ParticleManager _particleManager;
    private AudioController _audioController;
    
    private GameManager _gameManager;
    private Vector3 _enemyDirection;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _particleManager = FindObjectOfType<ParticleManager>();
        _player = FindObjectOfType<PlayerController>();
        light2D = gameObject.GetComponentInChildren<Light2D>();
        _audioController = FindObjectOfType<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _enemyDirection * (_gameManager.enemiesSpeed * Time.deltaTime);

        Vector2 distanceFromPlayer = _player.transform.position - transform.position;

        if (distanceFromPlayer.magnitude > 3)
        {
            light2D.color = Color.yellow;
            light2D.intensity = 0.5f;
        }
        else
        {
            light2D.color = Color.red;
            light2D.intensity = 1.5f;
        }
        
    }

    public void SetDirection(Vector3 dir)
    {
        _enemyDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Shield") || other.gameObject.CompareTag("Player"))
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        _audioController.PlayEnemyDeath();
        _particleManager.SpawnEnemyDeathParticle(transform.position);
        gameObject.SetActive(false);
    }
}
