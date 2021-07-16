using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{

    private GameManager _gameManager;
    private UIManager _uiManager;
    private EnemySpawner _enemySpawner;
    private Timer _timer;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) && !_gameManager.isGameOver)
        {
            _uiManager.ToggleUI(false);
            _gameManager.GameOver();
        }

      
    }
}
