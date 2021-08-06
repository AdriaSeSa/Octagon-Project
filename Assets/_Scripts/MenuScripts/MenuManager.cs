using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public enum CurrentPanel
    {
        MAIN_MENU,
        PLAY,
        DIFFICULTY,
        OPTIONS,
        CREDITS,
        HOWTOPLAY
    }

    public CurrentPanel _currentPanel = CurrentPanel.MAIN_MENU;
    private MenuOption _menuOption;
    private PlayerController _playerController;
    private TutorialSpawner _tutorialSpawner;
    private int[] _highScores = new int[8];
    private Color32[] _scoreColors = {Color.cyan, Color.red, Color.yellow, Color.magenta};
    private SettingsController _settingsController;

    public int[] directionCounter = new int[4];
    
    public GameObject[] panels = new GameObject[6];

    public GameObject highScoreUI;
    public TextMeshProUGUI highScoreText;


    private void Start()
    {
        _menuOption = FindObjectOfType<MenuOption>();
        _playerController = FindObjectOfType<PlayerController>();
        _tutorialSpawner = FindObjectOfType<TutorialSpawner>();
        _settingsController = FindObjectOfType<SettingsController>();
        
        _highScores[0] = PlayerPrefs.GetInt("highScore00");
        _highScores[1] = PlayerPrefs.GetInt("highScore02");
        _highScores[2] = PlayerPrefs.GetInt("highScore01");
        _highScores[3] = PlayerPrefs.GetInt("highScore03");
        _highScores[4] = PlayerPrefs.GetInt("highScore10");
        _highScores[5] = PlayerPrefs.GetInt("highScore12");
        _highScores[6] = PlayerPrefs.GetInt("highScore11");
        _highScores[7] = PlayerPrefs.GetInt("highScore13");
    }

    /*
     * 0 = Left
     * 1 = Right
     * 2 = Up
     * 3 = Down
     */

    // Update is called once per frame
    void Update()
    {
        if (_menuOption.triggerAnim != null)
        {
            CheckInput();
        }
        
        for (int i = 0; i < directionCounter.Length; i++)
       {
           if (directionCounter[i] >= 200)
           {
               SelectOption(i);
               directionCounter[i] = 0;
           }
       }

       if (_currentPanel == CurrentPanel.DIFFICULTY)
       {
           int tempAdd = 0;
           
           if (PlayerPrefs.GetInt("gameMode") == 1)
           {
               tempAdd = 4;
           }
           
           highScoreText.text = _highScores[_playerController.currentDirection + tempAdd].ToString();
           highScoreText.color = _scoreColors[_playerController.currentDirection];
       }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption(4);
        }

        if (_currentPanel == CurrentPanel.CREDITS || _currentPanel == CurrentPanel.HOWTOPLAY) return;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionCounter[0]+=2;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            directionCounter[1]+=2;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            directionCounter[2]+=2;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            directionCounter[3]+=2;
        }
        else
        {
            for (int i = 0; i < directionCounter.Length; i++)
            {
                directionCounter[i] = 0;
            }
        }

    }

    
    //TODO: Fix Credits options Animation and Do HOW TO PLAY
    void SelectOption(int option)
    {
        if (_menuOption.triggerAnim == null)
        {
            SelectOption(option);
        }
        switch (_currentPanel)
        {
            case CurrentPanel.MAIN_MENU:
                switch (option)
                {
                    case 0:
                        SwitchPanels(4);
                        _currentPanel = CurrentPanel.CREDITS;
                        break;
                    case 1:
                        SwitchPanels(3);
                        _currentPanel = CurrentPanel.OPTIONS;
                        break;
                    case 2:
                        SwitchPanels(1);
                        _currentPanel = CurrentPanel.PLAY;
                        break;
                    case 3:
                        SwitchPanels(5);
                        _playerController.isTutorialOn = true;
                        _currentPanel = CurrentPanel.HOWTOPLAY;
                        break;
                    case 4:
                        Application.Quit(); //Exits application when presed BackSpace or Esc on Main Menu
                        break;
                }
                break;
            
            case CurrentPanel.OPTIONS:
                switch (option)
                {
                    case 0:
                        _settingsController.ToggleFullScreen();
                        break;
                    case 1:
                        _settingsController.ToggleMusic();
                        break;
                    case 2:
                        _settingsController.ToggleSFX();
                        break;
                    case 4:
                        SwitchPanels(0);
                        _currentPanel = CurrentPanel.MAIN_MENU; 
                        break;
                }
                break;
            
            case CurrentPanel.CREDITS:
            case CurrentPanel.HOWTOPLAY:
                switch (option)
                {
                    case 4:
                        SwitchPanels(0);
                        _playerController.isTutorialOn = false;
                        _currentPanel = CurrentPanel.MAIN_MENU; 
                        break;
                }
                break;
            
            case CurrentPanel.PLAY:
                switch (option)
                {
                    case 0:
                        SwitchPanels(2);
                        _currentPanel = CurrentPanel.DIFFICULTY;
                        PlayerPrefs.SetInt("gameMode", 0);
                        
                        highScoreUI.SetActive(true);
                        break;
                    case 1:
                        SwitchPanels(2);
                        _currentPanel = CurrentPanel.DIFFICULTY;
                        PlayerPrefs.SetInt("gameMode", 1);
                        
                        highScoreUI.SetActive(true);
                        break;
                    case 4:
                        SwitchPanels(0);
                        _currentPanel = CurrentPanel.MAIN_MENU;
                        break;
                }
                break;
            
            case CurrentPanel.DIFFICULTY:
                switch (option)
                {
                    case 0:
                        PlayerPrefs.SetInt("difficulty", 0);
                        SceneManager.LoadScene(1);
                        break;
                    case 1:
                        PlayerPrefs.SetInt("difficulty", 2);
                        SceneManager.LoadScene(1);
                        break;
                    case 2:
                        PlayerPrefs.SetInt("difficulty", 1);
                        SceneManager.LoadScene(1);
                        break;
                    case 3:
                        PlayerPrefs.SetInt("difficulty", 3);
                        SceneManager.LoadScene(1);
                        break;
                    case 4:
                        SwitchPanels(1);
                        _currentPanel = CurrentPanel.PLAY;
                        
                        highScoreUI.SetActive(false);
                        break;
                }

                break;
        }
        
        _menuOption.SetOptions();
    }

    void SwitchPanels(int panel)
    {
        panels[(int)_currentPanel].SetActive(false);
        panels[panel].SetActive(true);
    }
}
