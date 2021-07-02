using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //TODO: CANBIAR TODOS LOS ENUM POR PLAYERPREF
    enum CurrentPanel
    {
        MAIN_MENU,
        PLAY,
        DIFFICULTY,
        OPTIONS,
        CREDITS
    }

    public enum GameMode
    {
        RUSH,
        STANDARD
    }

    public enum GameDifficulty
    {
        EASY,
        NORMAL,
        HARD,
        EIGHT_WAY
    }
    
    private CurrentPanel _currentPanel = CurrentPanel.MAIN_MENU;
    private int[] _directionCounter = new int[4];
    
    
    public GameMode currentGameMode;
    public GameDifficulty currentGameDifficulty;
    public GameObject[] panels = new GameObject[5];


    /*
     * 0 = Left
     * 1 = Right
     * 2 = Up
     * 3 = Down
     */
    
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
       CheckInput();

       for (int i = 0; i < _directionCounter.Length; i++)
       {
           if (_directionCounter[i] >= 175)
           {
               SelectOption(i);
               _directionCounter[i] = 0;
           }
       }
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _directionCounter[0]++;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _directionCounter[1]++;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _directionCounter[2]++;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _directionCounter[3]++;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backslash))
        {
            SelectOption(4);
        }
                
    }

    void SelectOption(int option)
    {
        switch (_currentPanel)
        {
            case CurrentPanel.MAIN_MENU:
                switch (option)
                {
                    case 0:
                        panels[0].SetActive(false);
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
                    case 4:
                        Application.Quit(); //Exits application when presed BackSpace or Esc on Main Menu
                        break;
                }
                break;
            
            case CurrentPanel.OPTIONS:
                switch (option)
                {
                    case 4:
                        SwitchPanels(0);
                        _currentPanel = CurrentPanel.MAIN_MENU; 
                        break;
                }
                break;
            
            case CurrentPanel.CREDITS:
                switch (option)
                {
                    case 4:
                        SwitchPanels(0);
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
                        break;
                    case 1:
                        SwitchPanels(2);
                        _currentPanel = CurrentPanel.DIFFICULTY;
                        currentGameMode = GameMode.RUSH;
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
                        currentGameDifficulty = GameDifficulty.HARD;
                        break;
                    case 2:
                        currentGameDifficulty = GameDifficulty.NORMAL;
                        break;
                    case 3:
                        currentGameDifficulty = GameDifficulty.EIGHT_WAY;
                        break;
                    case 4:
                        SwitchPanels(1);
                        _currentPanel = CurrentPanel.PLAY;
                        break;
                }

                break;
        }
    }

    void SwitchPanels(int panel)
    {
        panels[(int)_currentPanel].SetActive(false);
        panels[panel].SetActive(true);
    }
}
