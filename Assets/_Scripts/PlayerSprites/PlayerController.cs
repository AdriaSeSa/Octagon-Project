using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float _horizontalInput, _verticalInput;
    private float _knifeForce;
    private SpriteRenderer _spriteRenderer;
    private int _currentSprite = 0;
    private IEnumerator _invulnerabilityTime;
    private int playerLifes = 3;
    private GameManager _gameManager;
    private AudioController _audioController;
    
    public SpriteRenderer shieldSpriteRenderer;
    public Sprite[] shieldSprites = new Sprite[3];
    public Sprite[] spriteArray = new Sprite[3];
    public int currentDirection;
    public bool isTutorialOn = false;

    public int GETPlayerLifes()
    {
        return playerLifes;
    }
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = FindObjectOfType<GameManager>();
        _audioController = FindObjectOfType<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager == null)
        {
            if (isTutorialOn)
            {
                if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, -135);
                    return;
                }

                if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 135);
                    return;
                }

                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 315);
                    return;
                }

                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 45);
                    return;
                }
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                currentDirection = 0;
                return;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                currentDirection = 1;
                return;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                currentDirection = 2;
                return;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                currentDirection = 3;
                return;
            }

            return;
        }
        
            if (!_gameManager.isGameOver)
            {
                #region Rotate Movement

                if (PlayerPrefs.GetInt("difficulty") == 3) //If 8-way is Enabled
                {
                    if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
                    {
                        transform.rotation = Quaternion.Euler(0, 0, -135);
                        return;
                    }

                    if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 135);
                        return;
                    }

                    if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 315);
                        return;
                    }

                    if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 45);
                        return;
                    }
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    return;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    return;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    return;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    return;
                }

                #endregion
            }
            
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CheckEnemyInteraction();
        }
    }

    private void UpdateSprite()
    {
        _currentSprite++;
        
        _spriteRenderer.sprite = spriteArray[_currentSprite];
        shieldSpriteRenderer.sprite = shieldSprites[_currentSprite];

    }

    private void CheckEnemyInteraction()
    {
        if (_invulnerabilityTime == null)
        {
            if (playerLifes != 1)
            {
                _audioController.PlayPlayerHit();
                _invulnerabilityTime = InvulnerabilityTime();
                StartCoroutine(InvulnerabilityTime());
                UpdateSprite();
                playerLifes--;
            }
            else Die();
        }
    }

    private void Die()
    {
        //TODO: Show bullet that killed you pausing the game
        _audioController.PlayPlayerLoss();
        _gameManager.GameOver();
    }

    IEnumerator InvulnerabilityTime()
    {
        for (int i = 0; i < 15; ++i)
        {
            _spriteRenderer.enabled = (!_spriteRenderer.enabled);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        _spriteRenderer.enabled = true;
        _invulnerabilityTime = null;
    }
}
