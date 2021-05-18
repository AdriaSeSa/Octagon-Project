using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBaseClass : MonoBehaviour
{
    [SerializeField]
    public int speed;
    [SerializeField]
    private int xBounds = 8;
    [SerializeField]
    private int yBounds = 8;

    private Vector3 _enemyDirection;

    // Update is called once per frame
    void Update()
    {
        transform.position += _enemyDirection * (speed * Time.deltaTime);
        
       if (transform.position.x >= xBounds || transform.position.x <= -xBounds)
        {
            gameObject.SetActive(false);
        }
        if (transform.position.y >= yBounds || transform.position.y <= -yBounds)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDirection(Vector3 dir)
    {
        _enemyDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shield" || other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
