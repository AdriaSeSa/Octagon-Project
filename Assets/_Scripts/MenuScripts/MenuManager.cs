using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    //TODO: CANBIAR TODOS LOS ENUM POR PLAYERPREF
    public enum CurrentPanel
    {
        MAIN_MENU,
        PLAY,
        DIFFICULTY,
        OPTIONS,
        CREDITS
    }
    
    public CurrentPanel _currentPanel = CurrentPanel.MAIN_MENU;
    private MenuOption _menuOption;
    
    public int[] directionCounter = new int[4];
    
    public GameObject[] panels = new GameObject[5];


    private void Start()
    {
        _menuOption = FindObjectOfType<MenuOption>();
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
       CheckInput();

       for (int i = 0; i < directionCounter.Length; i++)
       {
           if (directionCounter[i] >= 140)
           {
               SelectOption(i);
               directionCounter[i] = 0;
           }
       }
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionCounter[0]++;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            directionCounter[1]++;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            directionCounter[2]++;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            directionCounter[3]++;
        }
        else
        {
            for (int i = 0; i < directionCounter.Length; i++)
            {
                directionCounter[i] = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
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
                        _menuOption.SetOptions();
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
                        PlayerPrefs.SetInt("gameMode", 1);
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
